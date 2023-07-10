using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RatedTravel.Core.Interfaces;
using static RaterTravel.Common.NotificationMessagesConstants;

namespace RatedTravel.App.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmploeeService employeeService;

        public EmployeeController(IEmploeeService employeeService)
        {
            this.employeeService = employeeService;
        }


        [HttpGet]
        public async Task<IActionResult> JoinUs()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isValidForEmployee = await this.employeeService.AgentExistsByIdAndHasMoreThanThreeCreatedItems(userId);
            if (!isValidForEmployee)
            {
	            TempData[ErrorMessage] = "Sorry but you don't meet the requirements!";

	            return this.RedirectToAction("Index", "Home");
            }
            return this.View();
		}



           
        
    }
}
