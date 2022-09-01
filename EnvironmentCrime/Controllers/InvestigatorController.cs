using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.FileProviders;

namespace EnvironmentCrime.Controllers
{
    [Authorize(Roles = "Investigator")]
    public class InvestigatorController : Controller
    {
        private IWebHostEnvironment environment;

        private IRepository repository;

        public InvestigatorController(IRepository repo, IWebHostEnvironment env)
        {
            repository = repo;
            environment = env;


        }




        /// <summary>
        /// Displays an CrimeInvestigator page.
        /// </summary>
        /// <returns>An CrimeInvestigator page.</returns>
        public ViewResult CrimeInvestigator(int id)
        {
            @ViewBag.Title = "Handläggare - Detaljer";
            ViewBag.ID = id;
            ViewBag.ListOfStatuses = repository.ErrandStatuses;

            TempData["ID"] = id;
            return View();
        }
        /// <summary>
        /// Displays an StartInvestigator page.
        /// </summary>
        /// <returns>An StartInvestigator page.</returns>
        public ViewResult StartInvestigator()
        {
            @ViewBag.Title = "Handläggare - Start";
            return View(repository);
        }
        // Method to add information from form to database in Investigator
        public async Task <IActionResult> UpdateInvestigator(string statusID, string events, string information, IFormFile loadImage, IFormFile loadSample)
        {

            int someID = int.Parse(TempData["ID"].ToString());



            repository.InvestigatorUpdateStatus(someID, statusID, events, information);
            if (loadImage != null)
            {
               await addImage(loadImage, someID);

            }
            if (loadSample != null)
            {
               await addSample(loadSample, someID);
            }


            return RedirectToAction("CrimeInvestigator", new { id = someID });
        }

        // Method being used in “UpdateInvestigator” to add an sample
        public async Task addSample ( IFormFile loadSample, int someID)
        {
            var tempPath = Path.GetTempFileName();
            if (loadSample.Length > 0)
            {
                using (var stream = new FileStream(tempPath, FileMode.Create))
                {
                    await loadSample.CopyToAsync(stream);
                }


                var path = Path.Combine(environment.WebRootPath, "Samples", loadSample.FileName);

                System.IO.File.Move(tempPath, path);


                string filename = loadSample.FileName;

                repository.AddSample(someID, filename);




            }
        }
        // Method being used in “UpdateInvestigator” to add an image
        public async Task addImage(IFormFile loadImage, int someID)
        {


            var tempPath = Path.GetTempFileName();
            if (loadImage.Length > 0)
            {
                using (var stream = new FileStream(tempPath, FileMode.Create))
                {
                   await loadImage.CopyToAsync(stream);
                }


                var path = Path.Combine(environment.WebRootPath, "Pictures", loadImage.FileName);

                System.IO.File.Move(tempPath, path);


                string filename = loadImage.FileName;

                ViewBag.Path = path;

                repository.AddPicture(someID, filename);

            }


        }
        }



    }

