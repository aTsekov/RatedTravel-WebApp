using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RatedTravel.App.Web.Controllers
{
    [Authorize]
    public class CityController : Controller
    {
        //[AllowAnonymous]
        //public Task<IActionResult>SelectCity()
        //{
        //    return View();
        //}
    }
}
