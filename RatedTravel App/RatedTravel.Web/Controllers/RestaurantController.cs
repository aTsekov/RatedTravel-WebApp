using Microsoft.AspNetCore.Mvc;

namespace RatedTravel.App.Web.Controllers
{
    public class RestaurantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
