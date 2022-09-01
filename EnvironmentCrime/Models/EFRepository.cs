using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EnvironmentCrime.Models
{
    public class EFRepository : IRepository
    {
        private IHttpContextAccessor contextAcc;

        private ApplicationDBContext context;
        public EFRepository(ApplicationDBContext ctx, IHttpContextAccessor cont)
        {
            context = ctx;

            contextAcc = cont;

        }

        public IQueryable<Errand> Errands =>
        context.Errands.Include(e => e.Samples).Include(e => e.Pictures);


        public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;
        public IQueryable<Department> Departments => context.Departments;
        public IQueryable<Employee> Employees => context.Employees;

        public IQueryable<Sample> Samples => context.Samples;

        public IQueryable<Picture> Pictures => context.Pictures;





        // Gets the details of one single Errand
        public Task<Errand> GetDetail(int id)
        {
            return Task.Run(() =>
            {
                var details = Errands.Where(ed => ed.ErrandID == id).First();
                return details;

            });



        }
        /// <summary>
        /// Adds one Errand to the database
        /// </summary>
        /// <returns> A instance of an Errand object</returns>
       
        public Errand AddErrand(Errand errand)
        {
            if (errand.ErrandID == 0)
            {
                Sequence dbEntry = context.Sequences.FirstOrDefault(s => s.Id == 1);
                errand.RefNumber = "2020-45-" + dbEntry.CurrentValue;
                errand.StatusId = "S_A";
                context.Errands.Add(errand);
                dbEntry.CurrentValue++;

                context.SaveChanges();

            }
            return errand;
        }

        /// <summary>
        /// Updates the department responsible for a Errand
        /// </summary>
        public void CoordinatorUpdate(int ErrandID, string departmentID)
        {
            Errand dbEntry = context.Errands.FirstOrDefault(s => s.ErrandID == ErrandID);

            if (dbEntry != null & departmentID != "Välj Avdelning" & departmentID != "D00")
            {
                dbEntry.DepartmentId = departmentID;
                context.SaveChanges();
            }
           
        }
        /// <summary>
        ///Updates the Employee responsible for a Errand
        /// </summary>
        public void ManagerUpdate(int ErrandID, string EmployeeId)
        {
            Errand dbEntry = context.Errands.FirstOrDefault(s => s.ErrandID == ErrandID);

            if (dbEntry != null & EmployeeId != "Välj handläggare")
            {
                dbEntry.EmployeeId = EmployeeId;
                context.SaveChanges();
            }
            
        }
        ///<summary>
        /// If a checkbox  on the startManager is  View  "true"  this method will be run and will updates the page.
       /// </summary>
        public void ManagerNoAction(int ErrandID, string reason)
        {
            Errand dbEntry = context.Errands.FirstOrDefault(s => s.ErrandID == ErrandID);

            if (dbEntry != null)
            {
                
                dbEntry.InvestigatorInfo = reason;
                dbEntry.StatusId = "S_B";
                context.SaveChanges();
            }
            
        }

        //Updates the status of an Errand.
        public void InvestigatorUpdateStatus(int ErrandID, string StatusId, string events, string information)
        {
            
            Errand dbEntry = context.Errands.FirstOrDefault(s => s.ErrandID == ErrandID);

            //Checks that the user 
            if (dbEntry != null & StatusId != "Välj" & StatusId != "S_B" & StatusId != "S_A")
            {
                dbEntry.StatusId = StatusId;
                if (events != null) { dbEntry.InvestigatorAction += " " + events; }

                if (information != null) { dbEntry.InvestigatorInfo += " " + information; }

                context.SaveChanges();
            }
            
        }

        /// <summary>
        /// Adds a picture to the database
        /// </summary>
        /// <param name="ErrandID"></param>
        /// <param name="pictureName"></param>
        
        public void AddPicture(int ErrandID, string pictureName)

        {
            //Makes sure picturename isnt null
            if ( pictureName != null)
            {
                Picture p = new Picture
                {
                    PictureName = pictureName,
                    ErrandId = ErrandID
                };

                context.Pictures.Add(p);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Adds a sample to the database
        /// </summary>
        /// <param name="ErrandID"></param>
        /// <param name="sampleName"></param>

        public void AddSample(int ErrandID, string sampleName)

        {
            if (sampleName != null)
            {
                Sample newSample = new Sample
                {
                    SampleName = sampleName,
                    ErrandId = ErrandID

                };

                context.Samples.Add(newSample);

                context.SaveChanges();

            }
        }

        /// <summary>
        /// Gets all the relevant Information about a Errand 
        /// </summary>
        /// <returns>the variable "errandList"</returns>
        public IQueryable <MyErrand> CoordinatorInfo()
            {
                var errandList = from err in Errands
                                 join stat in ErrandStatuses on err.StatusId equals stat.StatusID
                                 join dep in Departments on err.DepartmentId equals dep.DepartmentID
                                 into departmentErrand
                                 from deptE in departmentErrand.DefaultIfEmpty()
                                 join em in Employees on err.EmployeeId equals em.EmployeeID
                                 into employeeErrand
                                 from empE in employeeErrand.DefaultIfEmpty()
                                 orderby err.RefNumber descending
                                 //Adds the information to a poco-class named “MyErrand”
                                 select new MyErrand
                                 {
                                     DateOfObservation = err.DateOfObservation,
                                     ErrandId = err.ErrandID,
                                     RefNumber = err.RefNumber,
                                     TypeOfCrime = err.TypeOfCrime,
                                     StatusName = stat.StatusName,
                                     DepartmentName = (err.DepartmentId == null ? "ej tillsatt" : deptE.DepartmentName),
                                     EmployeeName = (err.EmployeeId == null ? "ej tillsatt" : empE.EmployeeName)
                                 };

           
            return errandList;
            }



        /// <summary>
        /// Gets all the relevant Information about a Errand depending on where the manager works 
        /// </summary>
        /// <returns></returns>
        public IQueryable<MyErrand> ManagerInfo()
        {
            var userName = contextAcc.HttpContext.User.Identity.Name;
            var depid = context.Employees.FirstOrDefault(usr => usr.EmployeeID == userName).DepartmentId;

            var errandList = from err in Errands 
                             join stat in ErrandStatuses on err.StatusId equals stat.StatusID

                             join dep in Departments on err.DepartmentId equals dep.DepartmentID
                             into departmentErrand
                             from deptE in departmentErrand.DefaultIfEmpty()

                             join em in Employees on err.EmployeeId equals em.EmployeeID
                             into employeeErrand
                             from empE in employeeErrand.DefaultIfEmpty()
                            where err.DepartmentId == depid
                             orderby err.RefNumber descending
                             select new MyErrand
                             {
                                 DateOfObservation = err.DateOfObservation,
                                 ErrandId = err.ErrandID,
                                 RefNumber = err.RefNumber,
                                 TypeOfCrime = err.TypeOfCrime,
                                 StatusName = stat.StatusName,
                                 DepartmentName =  deptE.DepartmentName,
                                 EmployeeName = (err.EmployeeId == null ? "ej tillsatt" : empE.EmployeeName)
                             };

            return errandList;
        }

        /// <summary>
        /// Gets the name of all the Employees that work at a specific department
        /// </summary>
        /// <returns></returns>
        public IQueryable<Employee> EmployeeList()
        {
            var userName = contextAcc.HttpContext.User.Identity.Name;
        var depid = context.Employees.FirstOrDefault(usr => usr.EmployeeID == userName).DepartmentId;

        var employeeList = from em in Employees

                         where em.DepartmentId == depid && em.EmployeeID != userName
                         orderby em.EmployeeID descending
                         select new Employee
                         {                         
                             EmployeeName = em.EmployeeName,
                             EmployeeID = em.EmployeeID
                         };

            return employeeList;
        }


        /// <summary>
        /// Gets all the relevant Information about the Errands that the investigator has been assigned  
        /// </summary>
        /// <returns></returns>
        public IQueryable<MyErrand> InvestigatorInfo()
        {
            var userName = contextAcc.HttpContext.User.Identity.Name;

            var errandList = from err in Errands
                             join stat in ErrandStatuses on err.StatusId equals stat.StatusID
                             join dep in Departments on err.DepartmentId equals dep.DepartmentID
                             into departmentErrand
                             from deptE in departmentErrand.DefaultIfEmpty()
                             join em in Employees on err.EmployeeId equals em.EmployeeID
                             into employeeErrand
                             from empE in employeeErrand.DefaultIfEmpty()
                             where (err.EmployeeId).Contains(userName)
                             orderby err.RefNumber descending
                             select new MyErrand
                             {
                                 DateOfObservation = err.DateOfObservation,
                                 ErrandId = err.ErrandID,
                                 RefNumber = err.RefNumber,
                                 TypeOfCrime = err.TypeOfCrime,
                                 StatusName = stat.StatusName,
                                 DepartmentName = deptE.DepartmentName,
                                 EmployeeName =  empE.EmployeeName
                             };

            return errandList;
        }

    }
}


               

        
            
            

        




    

