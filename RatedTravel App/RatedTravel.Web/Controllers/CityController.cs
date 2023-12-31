﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using RatedTravel.Common;
using RatedTravel.Core.Interfaces;
using RatedTravel.Web.ViewModels.City;
using static RatedTravel.Common.GeneralApplicationConstants;


 

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

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> SelectCity(string cityId)
		{
			try
			{
				var currentCity = await cityService.SelectCityAsync(cityId);

				if (currentCity == null)
				{
					ModelState.AddModelError(nameof(cityId), "This city doesn't exist! Try another one.");
					this.TempData[NotificationMessagesConstants.ErrorMessage] = "This city doesn't exist! Try another one.";
					return RedirectToAction("SelectCity", "City");
				}

				string cityName = currentCity.Name;

				bool doesCityExists = await this.cityService.DoesCityExistsAsync(cityName);

				if (!doesCityExists)
				{
					ModelState.AddModelError(nameof(cityName), "This city doesn't exist! Try another one.");
					this.TempData[NotificationMessagesConstants.ErrorMessage] = "This city doesn't exist! Try another one.";
					return RedirectToAction("SelectCity", "City");
				}

				return View(currentCity);
			}
			catch (Exception)
			{
				

				this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Please try again later or contact us";
				return RedirectToAction("Error", "Home");
			}
		}


		[HttpGet]
        //Checking if the user is allowed to add a city or not. 
        public async Task<IActionResult> AddCity()
        {
			string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			bool isEmployee =
		        await this.employeeService.EmployeeExistsByUserIdAsync(userId);


	        if (!isEmployee)
	        {
		        this.TempData[NotificationMessagesConstants.ErrorMessage] =
			        "You have to be part of the team in order to be able to add cities";
		        return RedirectToAction("Index", "Home");
	        }

	        return View();
        }

        [HttpPost]

        public async Task<IActionResult> AddCity(CityFormModel model)
        {
			string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			bool isEmployee = await this.employeeService.EmployeeExistsByUserIdAsync(userId);

	        if (!isEmployee)
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
				
				var employee = await employeeService.EmployeeByUserIdAsync(userId);
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

			return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> EditCity(string id)
        {
            bool cityExists = await cityService.DoesCityExistsByIdAsync(id);

            if (!cityExists)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "This city does not exists!";

                return RedirectToAction("Index", "Home");
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isEmployee =
                await this.employeeService.EmployeeExistsByUserIdAsync(userId);

            if (!isEmployee)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "You must part of our team in order to edit the city!";

                return this.RedirectToAction("JoinUs", "Employee");
            }

            try
            {
                CityFormModel formModel = await this.cityService
                    .EditAsync(id);
               

                return this.View(formModel);
            }
            catch (Exception)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Please try again later or contact us";
                return this.RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditCity(string id, CityFormModel model)
        {


            bool cityExists = await cityService.DoesCityExistsByIdAsync(id);

            if (!cityExists)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "This city does not exists!";

                return RedirectToAction("Index", "Home");
            }
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isEmployee =
                await this.employeeService.EmployeeExistsByUserIdAsync(userId);

            if (!isEmployee)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "You must part of our team in order to edit the city!";

                return this.RedirectToAction("JoinUs", "Employee");
            }

            try
            {
                await this.cityService.EditCityByIdAndFormModelAsync(id, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to update the City. Please try again later or contact administrator!");
                
                return View(model);
            }

            this.TempData[NotificationMessagesConstants.SuccessMessage] = "The city was edited successfully!";
             return RedirectToAction("Index", "Home", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCity(string id)
        {
            bool cityExists = await cityService.DoesCityExistsByIdAsync(id);

            if (!cityExists)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "You cannot delete a city that doesn't exist!";

                return RedirectToAction("Index", "Home");
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isEmployee =
                await this.employeeService.EmployeeExistsByUserIdAsync(userId);

            

            if (!this.User.IsInRole(AdminRoleName))
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "You must be Admin in order to delete a city!";

                return this.RedirectToAction("Login", "User");
            }

            try
            {
                CityDeleteModel deleteModel = await this.cityService
                    .GetCityForDelete(id);


                return this.View(deleteModel);
            }
            catch (Exception)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Please try again later or contact us";
                return this.RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCity(string id, CityDeleteModel model)
        {


            bool cityExists = await cityService.DoesCityExistsByIdAsync(id);

            if (!cityExists)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "This city does not exists!";

                return RedirectToAction("Index", "Home");
            }
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isEmployee =
                await this.employeeService.EmployeeExistsByUserIdAsync(userId);

            if (!this.User.IsInRole(AdminRoleName))
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "You must be Admin in order to delete a city!";

                return this.RedirectToAction("Login", "User");
            }

            try
            {
                await this.cityService.DeleteCityByIdAsync(id);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to update the City. Please try again later or contact administrator!");

                return View(model);
            }

            this.TempData[NotificationMessagesConstants.SuccessMessage] = "The city was deleted successfully!";
            return RedirectToAction("Index", "Home", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> AllCities()
        {
            List<CityAllModel> allCities = new List<CityAllModel>();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isUserEmployee = await this.employeeService.EmployeeExistsByIdAsync(userId);

            try
            {
                allCities.AddRange(await cityService.AllCitiesAsync());

                return View(allCities);
            }
            catch (Exception)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Please try again later or contact us";
                return this.RedirectToAction("Index", "Home");
            }
        }


    }
}
