using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EnvironmentCrime.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private IRepository repository;

        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        

        public HomeController(IRepository repo, UserManager<IdentityUser> userMgr,
        SignInManager<IdentityUser> signInMgr) 
        {
            repository = repo;

            userManager = userMgr;
            signInManager = signInMgr;

        }

        /// <summary>
        /// Displays an index page.
        /// </summary>
        /// <returns>An index page.</returns
        [AllowAnonymous]
        public ViewResult  Index()
        {
            
            @ViewBag.Title = "Småstads Kommun - Start";
            var myErrand = HttpContext.Session.GetJson<Errand>("Errand");
            if (myErrand == null)
            {
                return View();
            }
            else
            {
                return View(myErrand);
            }
        }
        /// <summary>
        /// Displays an Login page.
        /// </summary>
        /// <returns>An returnUrl.</returns
        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            @ViewBag.Title = "Småstads Kommun - Login";
            return View(new LoginModel
            {
                ReturnUrl = returnUrl
            });
        }

        /// <summary>
        /// A help method for Login
        /// </summary>
        /// <returns>An instans of the LoginModel object</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(loginModel.UserName);
                
                    if (user != null)
                    {
                        await signInManager.SignOutAsync();
                        if ((await signInManager.PasswordSignInAsync(user,
                        loginModel.Password, false, false)).Succeeded)
                        {

                        if (await userManager.IsInRoleAsync(user, "Coordinator"))
                        {
                            return Redirect("/Coordinator/StartCoordinator");
                        }

                        if (await userManager.IsInRoleAsync(user, "Investigator"))
                        {
                            return Redirect("/Investigator/StartInvestigator");
                        }

                        if (await userManager.IsInRoleAsync(user, "Manager"))
                        {
                            return Redirect("/Manager/StartManager");
                        }
                    }

                    
                }

               
            }
            ModelState.AddModelError("", "Felaktigt användarnamn eller lösenord");
            return View(loginModel);

        }

        /// <summary>
        /// Signs out the user
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns> a RedirectResult named with the value "/"</returns>
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }



        [AllowAnonymous]
        public ViewResult AccessDenied() 
        { 
        return View();
        }

    }


}
