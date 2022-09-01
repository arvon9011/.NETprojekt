using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Infrastructure;


namespace EnvironmentCrime.Controllers
{

    public class CitizenController : Controller
    {
        private IRepository repository;

        public CitizenController(IRepository repo)
        {
            repository = repo;

        }

        /// <summary>
        /// Displays an Context page.
        /// </summary>
        /// <returns>An Context page.</returns
        public ViewResult Contact()
        {
            @ViewBag.Title = "Småstads kommun - Kontakt";
            return View();
        }

        /// <summary>
        /// Displays an Faq page.
        /// </summary>
        /// <returns>An Faq page.</returns>
        public ViewResult Fag ()
        {
            @ViewBag.Title = "Småstads kommun - Frågor";
            return View();
        }

        /// <summary>
        /// Displays an Services page.
        /// </summary>
        /// <returns>An Services page.</returns>
        public ViewResult Services()
        {
            @ViewBag.Title = "Småstads kommun - Tjänster";
            return View();
        }

        /// <summary>
        /// Displays an Thanks page.
        /// </summary>
        /// <returns>An Thanks page.</returns>
        public ViewResult Thanks()
        {
            @ViewBag.Title = "Småstads kommun - Tack för din anmälan";
            

            return View();
        }

        /// <summary>
        /// Displays an Validate page.
        /// </summary>
        /// <returns>An Validate page with an instans of a Errand object</returns>
        public ViewResult Validate(Errand errand)
        {
            @ViewBag.Title = "Småstads kommun - Validera";
            HttpContext.Session.SetJson("Errand", errand);
          
            return View(errand);
        }


    }
}
