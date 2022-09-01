using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnvironmentCrime.Infrastructure;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Components
{
    public class ThanksViewComponent : ViewComponent
    {

        private IRepository repository;

        public ThanksViewComponent(IRepository repo)
        {
            repository = repo;
        }

            public IViewComponentResult Invoke()
             {

            var myErrand = HttpContext.Session.GetJson<Errand>("Errand");
            repository.AddErrand(myErrand);
            HttpContext.Session.Remove("Errand");        
            return View(myErrand);
               }
    }
}
