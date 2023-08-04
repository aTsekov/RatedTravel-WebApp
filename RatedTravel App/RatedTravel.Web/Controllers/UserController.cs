using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RatedTravel.Data.DataModels;
using RatedTravel.Web.ViewModels.User;

namespace RatedTravel.App.Web.Controllers
{
	
        public class UserController : Controller
        {
            private readonly SignInManager<ApplicationUser> signInManager;
            private readonly UserManager<ApplicationUser> userManager;
            private readonly IMemoryCache memoryCache;

            public UserController(SignInManager<ApplicationUser> signInManager,
                                  UserManager<ApplicationUser> userManager,
                                  IMemoryCache memoryCache)
            {
                this.signInManager = signInManager;
                this.userManager = userManager;

                this.memoryCache = memoryCache;
            }

            [HttpGet]
            public IActionResult Register()
            {
                return View();
            }

            [HttpPost]
            //[ValidateRecaptcha(Action = nameof(Register),
            // ValidationFailedAction = ValidationFailedAction.ContinueRequest)]
             public async Task<IActionResult> Register(RegisterFormModel model)
             {
                  if (!ModelState.IsValid)
                  {
                     return View(model);
                  }

                 ApplicationUser user = new ApplicationUser()
                 {
                    FirstName = model.FirstName,
                    LastName = model.LastName
                 };

                await userManager.SetEmailAsync(user, model.Email);
                await userManager.SetUserNameAsync(user, model.Email);

                IdentityResult result =
                await userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View(model);
                }

                await signInManager.SignInAsync(user, false);
                //this.memoryCache.Remove(UsersCacheKey);

                return RedirectToAction("Index", "Home");
             }




    }
}

