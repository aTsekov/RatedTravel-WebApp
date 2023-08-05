using Microsoft.AspNetCore.Mvc;
using RatedTravel.Core.Interfaces;
using RatedTravel.Web.ViewModels.Home;
using static RatedTravel.Common.GeneralApplicationConstants;

namespace RatedTravel.App.Web.Controllers
{
	public class HomeController : Controller
    {
       
        private readonly ICityService cityService;
        public HomeController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        public async Task<IActionResult> Index()
        {
	        if (this.User.IsInRole(AdminRoleName))
	        {
				return this.RedirectToAction("Index", "Home", new { Area = AdminAreaName });
			}
            IEnumerable<IndexViewModel> viewModel = await this.cityService.OurCitiesAsync();
            return View(viewModel);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
	        if (statusCode == 404 || statusCode == 400)
	        {
                return this.View("Error404");
	        }
            return View();
        }
    }
}