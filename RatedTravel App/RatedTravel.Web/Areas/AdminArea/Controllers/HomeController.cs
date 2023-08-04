using Microsoft.AspNetCore.Mvc;

namespace RatedTravel.App.Web.Areas.AdminArea.Controllers
{
	public class HomeController : BaseAdminController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
