using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvironmentCrime.Models
{
   public interface IRepository
    {
        IQueryable<Errand> Errands { get; }
        IQueryable<ErrandStatus> ErrandStatuses { get; }
        IQueryable<Department> Departments { get; }
        IQueryable<Employee> Employees { get; }

        IQueryable<Sample> Samples { get; }

        IQueryable<Picture> Pictures { get; }

        




        Task <Errand> GetDetail(int id);

        public Errand AddErrand(Errand errand);

        public void CoordinatorUpdate(int errandId, string departmentId);

        public void ManagerUpdate(int errandId, string employeeId);

        public void ManagerNoAction(int ErrandID, string text);
        public void InvestigatorUpdateStatus(int errandId, string statusId, string events, string information);

        public void AddPicture(int ErrandID, string filename);

        public void AddSample(int ErrandID, string sampleName);

        public IQueryable <MyErrand> CoordinatorInfo();

        public IQueryable<MyErrand> ManagerInfo();

        public IQueryable<MyErrand> InvestigatorInfo();

        public IQueryable<Employee> EmployeeList();
    }
}
