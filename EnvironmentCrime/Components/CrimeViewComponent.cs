using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvironmentCrime.Components
{
    public class CrimeViewComponent: ViewComponent 
    {
        private IRepository repository;

        public CrimeViewComponent(IRepository repo)
        {
            repository = repo;

        }

       public async Task <IViewComponentResult> InvokeAsync(int id)
        {
            var detail = await repository.GetDetail(id);

            return View(detail);
        }
        
        


    }
}
