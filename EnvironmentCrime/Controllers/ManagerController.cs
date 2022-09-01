using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {

        private IRepository repository;

        public ManagerController(IRepository repo)
        {
            repository = repo;

        }

        /// <summary>
        /// Displays an StartManager page.
        /// </summary>
        /// <returns>An StartManager page with repository to connect to the database</returns>
        public ViewResult StartManager()
        {
            @ViewBag.Title = "Avdelningschef - Detaljer";
            return View(repository);
        }

        /// <summary>
        /// Displays an CrimeManager page.
        /// </summary>
        /// <returns>An CrimeManager page.</returns>
        public ViewResult CrimeManager(int id)
        {
            @ViewBag.Title = "Avdelningchef - Detaljer";
            ViewBag.ID = id;
            ViewBag.ListOfEmployees = repository.EmployeeList();

            TempData["ID"] = id;
            return View();
        }


        public IActionResult UpdateEmployee(string EmployeeId, bool noAction, string reason)
        {

            int someID = int.Parse(TempData["ID"].ToString());

            if (noAction == true)
            { repository.ManagerNoAction(someID, reason);}
            else
            { repository.ManagerUpdate(someID, EmployeeId); }

            return RedirectToAction("CrimeManager", new { id = someID });
        }
    }
}
