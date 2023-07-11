using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RatedTravel.Core.Interfaces;
using RatedTravel.Web.ViewModels.Employee;
//using static RaterTravel.Common.NotificationMessagesConstants;
using RaterTravel.Common;

namespace RatedTravel.App.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }


        [HttpGet]
        public async Task<IActionResult> JoinUs()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isValidForEmployee = await this.employeeService.EmployeeExistsByIdAndHasMoreThanThreeCreatedItemsAsync(userId);
            if (!isValidForEmployee)
            {
	            this.TempData[NotificationMessagesConstants.ErrorMessage] = "Sorry but you don't meet the requirements! You are either already part of the team or you need to add at least 3 places. ";

	            return this.RedirectToAction("Index", "Home");
            }
            return this.View();
		}

        [HttpPost]

        public async Task<IActionResult> JoinUs(BecomeEmployeeFormModel model)
        {
	        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

	        bool isValidForEmployee = await this.employeeService.EmployeeExistsByIdAndHasMoreThanThreeCreatedItemsAsync(userId);
	        if (!isValidForEmployee)
	        {
		        this.TempData[NotificationMessagesConstants.ErrorMessage] = "Sorry but you don't meet the requirements! You are either already part of the team or you need to add at least 3 places. ";

		        return this.RedirectToAction("Index", "Home");
	        }

	        bool isPhoneNumberAlreadyUser = await this.employeeService.EmployeeExistsByPhoneNumberAsync(model.PhoneNumber);

	        if (isPhoneNumberAlreadyUser)
	        {
		        this.ModelState.AddModelError(nameof(model.PhoneNumber), "A person with the provided phone number already exists!");

	        }
	        if (!this.ModelState.IsValid)
	        {
		        return this.View(model);
	        }

	        try
	        {
		        await this.employeeService.CreateEmployeeAsync(userId, model);
			}
	        catch (Exception)
	        {
		        this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Please try again later or contact us";
		        return this.RedirectToAction("Index", "Home");
	        }

	        return this.View("JoinUsWelcome");


        }





    }
}
