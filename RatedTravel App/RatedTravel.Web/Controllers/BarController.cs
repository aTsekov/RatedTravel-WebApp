using Microsoft.AspNetCore.Mvc;

namespace RatedTravel.App.Web.Controllers
{
    public class BarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
