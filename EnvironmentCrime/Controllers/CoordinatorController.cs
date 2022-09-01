using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace EnvironmentCrime.Controllers
{
    [Authorize(Roles = "Coordinator")]
    public class CoordinatorController : Controller
    {
        
        private IRepository repository;

        
        public CoordinatorController (IRepository repo)
        {
            repository = repo;

        }
        /// <summary>
        /// Displays an CrimeCoordinator page.
        /// </summary>
        /// <returns>An CrimeCoordinator page.</returns>
        public ViewResult CrimeCoordinator(int id)
        {
            @ViewBag.Title = "Samordnare - Detaljer";
            ViewBag.ID = id;
            ViewBag.ListOfDepartments = repository.Departments;
            TempData["ID"] = id;
            return View();
        }
        /// <summary>
        /// Displays an ReportCrime page.
        /// </summary>
        /// <returns>An ReportCrime page.</returns>
        public ViewResult ReportCrime()
        {
            @ViewBag.Title = "Samordnare - Rapport";

            var myCrime = HttpContext.Session.GetJson<Errand>("Errand");
            if (myCrime == null)
            {
                return View();
            }
            else
            {
                return View(myCrime);
            }
        }

        /// <summary>
        /// Displays an StartCoordinator page.
        /// </summary>
        /// <returns>An StartCoordinator page with the repostitory in order to connect to the database</returns>
        public ViewResult StartCoordinator() 
        {
            @ViewBag.Title = "Samordnare - Start";

            return View(repository);
        
        }


        /// <summary>
        /// Displays an Validate page.
        /// </summary>
        /// <returns>An Validate page with an instans of a Errand object</returns>
        public ViewResult Validate(Errand errand)
        {
            @ViewBag.Title = "Samordnare - Validera";
            HttpContext.Session.SetJson("Errand", errand);
            return View(errand);
        }

        /// <summary>
        /// Displays an Thanks page.
        /// </summary>
        /// <returns>An Thanks page.</returns>
        public ViewResult Thanks()
        {
            @ViewBag.Title = "Samordnare - Tack för din anmälan";
            
            return View();

        }

        /// <summary>
        /// A help method that is used when by the form to make sure the information is updated.
        /// </summary>
        /// <returns> Rediraction to the CrimeCoordinator site with a new id.</returns>
        public IActionResult UpdateDepartment(string DepartmentId)
        {
            int someID = int.Parse(TempData["ID"].ToString());

            repository.CoordinatorUpdate(someID, DepartmentId);

            return RedirectToAction("CrimeCoordinator", new { id = someID });
        }



    }
}
