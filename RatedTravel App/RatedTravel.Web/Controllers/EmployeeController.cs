using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RatedTravel.App.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        //public Task<IActionResult> JoinUs()
        //{
        //    return View();
        //}
    }
}
