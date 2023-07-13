using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RatedTravel.Core.Interfaces;
using RatedTravel.Web.ViewModels.City;
using RaterTravel.Common;

 

namespace RatedTravel.App.Web.Controllers
{
    [Authorize]
    public class CityController : Controller
    {
        private readonly IEmployeeService employeeService;
		private readonly  ICityService cityService;

        public CityController(IEmployeeService employeeService, ICityService cityService)
        {
	        this.employeeService = employeeService;
			this.cityService = cityService;
        }
		//[AllowAnonymous]
		//public Task<IActionResult> SelectCity()
		//{
		//	return View();
		//}

		[HttpGet]
        //Checking if the user is allowed to add a city or not. 
        public async Task<IActionResult> AddCity()
        {
			string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			bool isAgent =
		        await this.employeeService.EmployeeExistsByIdAsync(userId);
	        if (!isAgent)
	        {
		        this.TempData[NotificationMessagesConstants.ErrorMessage] =
			        "You have to be part of the team in order to be able to add cities";
		        return RedirectToAction("JoinUs", "Employee");
	        }

	        return View();
        }

        [HttpPost]

        public async Task<IActionResult> AddCity(CityFormModel model)
        {
			string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			bool isAgent = await this.employeeService.EmployeeExistsByIdAsync(userId);

	        if (!isAgent)
	        {
		        this.TempData[NotificationMessagesConstants.ErrorMessage] =
			        "You have to be part of the team in order to be able to add cities";
		        return RedirectToAction("JoinUs", "Employee");
	        }
	        bool doesCityExists = await this.cityService.DoesCityExistsAsync(model.Name);

			if (doesCityExists)
	        {
				//Automatically the model state becomes invalid.
				ModelState.AddModelError(nameof(model.Name), "This city already exists! Try another one.");
	        }

	        if (!ModelState.IsValid)
	        {
		        this.TempData[NotificationMessagesConstants.ErrorMessage] =
					"This city already exists! Try another one.";
				return RedirectToAction("AddCity","City");
			}

	        try
	        {
				
				var employee = await employeeService.EmployeeIdAsync(userId);
				var emplId = employee.Id.ToString();

                // Handle the uploaded image file
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    // Generate the filename for the image using the city name
                    var fileName = model.Name + "_" + model.ImageFile.FileName;

                    // Define the file path within the wwwroot folder where the image will be saved
                    var filePath = Path.Combine("wwwroot", "images", fileName);

                    // Save the image file to the specified path
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(fileStream);
                    }

                    await cityService.CreateCityAsync(emplId, userId, model);
                }

            }
	        catch (Exception)
	        {
		        this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Please try again later or contact us";
		        return this.RedirectToAction("Index", "Home");
	        }

			

			//TODO: to return to a specific city once I create that action and view. 
			return RedirectToAction("Index", "Home");

        }


    }
}
