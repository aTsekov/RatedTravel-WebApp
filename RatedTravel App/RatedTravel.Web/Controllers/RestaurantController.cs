using Microsoft.AspNetCore.Mvc;
using RatedTravel.Core.Interfaces;
using RatedTravel.Core.Services;
using RaterTravel.Common;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RatedTravel.Web.ViewModels.Restaurant;
using RatedTravel.Data.DataModels;

namespace RatedTravel.App.Web.Controllers
{
    [Authorize]
    public class RestaurantController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly ICityService cityService;
        private readonly IRestaurantService restaurantService;
        private readonly UserManager<ApplicationUser> userManager;


        public RestaurantController(IEmployeeService employeeService, ICityService cityService, IRestaurantService restaurantService, UserManager<ApplicationUser> userManager)
        {
            this.cityService = cityService;
            this.employeeService = employeeService;
            this.restaurantService = restaurantService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> AddRestaurant()
        {
            var user = await userManager.GetUserAsync(User);
            bool isUser = user != null;

            if (!isUser)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] =
                    "You have to logged in in order to add a restaurant!";
                return  RedirectToAction("Index", "Home");
            }


            return View();
        }

        [HttpPost]

        public async Task<IActionResult> AddRestaurant(RestaurantFormModel model)
        {
            // I need to add a check if the restaurant doesn't exist because if it does I should not create it. 
            var user = await userManager.GetUserAsync(User);
            bool isUser = user != null;

            bool doesRestaurantExistByName = await restaurantService.DoesRestaurantExistsByName(model.Name);

            if (doesRestaurantExistByName)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] =
                    "The restaurant already exists. To add more restaurants of the same chain edit the existing restaurant and add the address of the new locations.";
                return RedirectToAction("Index", "Home");
            }

            if (!isUser)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] =
                    "You have to logged in in order to add a restaurant!";
                return RedirectToAction("Index", "Home");
            }
            bool doesCityExists = await this.cityService.DoesCityExistsAsync(model.Name);

            if (!doesCityExists)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "This city does not exists! Try another one.";
            }


            

            try
            {
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

                    await restaurantService.CreateRestaurantAsync(user.Id.ToString(), model);
                }

            }
            catch (Exception)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Are you sure you are logged in?";
                return this.RedirectToAction("Index", "Home");
            }



            //TODO: to return to ALL restaurants. 
            return RedirectToAction("Index", "Home");

        }

		[HttpGet]
		public async Task<IActionResult> AllRestaurantsInACity(string cityId)
		{
			List<RestaurantAllModel> allRestaurantsInACity = new List<RestaurantAllModel>();

			try
			{
				allRestaurantsInACity.AddRange(await restaurantService.AllRestaurantsInACityAsync(cityId));

				return View(allRestaurantsInACity);
			}
			catch (Exception)
			{
				this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Please try again later or contact us";
				return this.RedirectToAction("Index", "Home");
			}
		}

	}
}
