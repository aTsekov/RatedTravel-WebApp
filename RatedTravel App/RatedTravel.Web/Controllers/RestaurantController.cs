using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RatedTravel.Core.Interfaces;
using RatedTravel.Data.DataModels;
using RatedTravel.Web.ViewModels.Restaurant;
using RatedTravel.Common;
using System.Security.Claims;

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
            bool doesCityExists = await this.cityService.DoesCityExistsAsync(model.CityName);

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
        [AllowAnonymous]
        public async Task<IActionResult> AllRestaurantsInACity(string cityId)
        {
            List<RestaurantAllModel> allRestaurantsInACity = new List<RestaurantAllModel>();

            try
            {
                allRestaurantsInACity.AddRange(await restaurantService.AllRestaurantsInACityAsync(cityId));

                if (allRestaurantsInACity.Count == 0)
                {
                    ModelState.AddModelError("", "No restaurants found in the selected city.");
                }

                if (!ModelState.IsValid)
                {
                    this.TempData[NotificationMessagesConstants.ErrorMessage] = "We couldn't find restaurants in this city.";

                    // Get the previous URL from Referer header
                    string previousUrl = Request.Headers["Referer"].ToString();
                    return Redirect(previousUrl);
                }

                return View(allRestaurantsInACity);
            }
            catch (Exception ex)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Please try again later or contact us";

                // Get the previous URL from Referer header
                string previousUrl = Request.Headers["Referer"].ToString();
                return Redirect(previousUrl);
            }
        }



        [HttpGet]
		[AllowAnonymous]

        public async Task<IActionResult> RestaurantDetails(string cityId, string restaurantId)
        {
            try
            {
                var restaurant = await restaurantService.DetailsAsync(cityId, restaurantId);

                if (restaurant == null)
                {
                    ModelState.AddModelError("", "Restaurant not found");
                    this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Please try again later or contact us";
                    return this.RedirectToAction("Index", "Home");
                }

                return View(restaurant);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Oops, something went wrong :( Please try again later or contact us");
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Please try again later or contact us";
                return this.RedirectToAction("Index", "Home");
            }
        }




        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> SubmitReview(string restaurantId, RestaurantRateAndReviewModel model)
        {
            bool doesRestaurantExistsByName = await restaurantService.DoesRestaurantExistsByIdAsync(model.RestaurantId);

            if (!doesRestaurantExistsByName)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] =
                    "You cannot write a review for this restaurant - please contact the administrators.";
                string previousUrl = Request.Headers["Referer"].ToString();
                return Redirect(previousUrl);
            }

            try
            {
                await restaurantService.SendReviewAsync(restaurantId, model);
                this.TempData[NotificationMessagesConstants.SuccessMessage] = "Thank you for your review!";
                string previousUrl = Request.Headers["Referer"].ToString();
                return Redirect(previousUrl);
            }
            catch (Exception)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :(";
                string previousUrl = Request.Headers["Referer"].ToString();
                return Redirect(previousUrl);
            }



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReview(string? reviewId)
        {
            if (string.IsNullOrEmpty(reviewId))
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "Invalid reviewId.";
                string previousUrl = Request.Headers["Referer"].ToString();
                return Redirect(previousUrl);
            }

            try
            {
                await restaurantService.DeleteReviewByIdAsync(reviewId);
                this.TempData[NotificationMessagesConstants.SuccessMessage] = "Review deleted successfully!";


                string previousUrl = Request.Headers["Referer"].ToString();

                // Get the URL of the page before the previous one
                string pageBeforePreviousUrl = Request.Headers["Referer"];

                // Redirect to the page before the previous one
                if (!string.IsNullOrEmpty(pageBeforePreviousUrl))
                {
                    return Redirect(pageBeforePreviousUrl);
                }
                else
                {
                    // If there's no page before the previous one, redirect to a default URL
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :(";
                string previousUrl = Request.Headers["Referer"].ToString();
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRestaurant(string restaurantId)
        {
            if (string.IsNullOrEmpty(restaurantId))
            {
                TempData[NotificationMessagesConstants.ErrorMessage] = "Invalid restaurantId.";
                string previousUrl = Request.Headers["Referer"].ToString();
                return Redirect(previousUrl);
            }

            try
            {
                RestaurantDeleteModel deleteModel = await restaurantService.GetRestaurantForDeleteAsync(restaurantId);

                return View(deleteModel);
            }
            catch (Exception)
            {
                TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :(";
                return RedirectToAction("Index", "Home");
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> DeleteRestaurant(RestaurantDeleteModel model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                TempData[NotificationMessagesConstants.ErrorMessage] = "Invalid restaurantId.";
                string previousUrl = Request.Headers["Referer"].ToString();
                return Redirect(previousUrl);
            }

            try
            {
                await restaurantService.DeleteRestaurantByIdAsync(model.Id);
                TempData[NotificationMessagesConstants.SuccessMessage] = "Restaurant deleted successfully!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :(";
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditRestaurant(string cityId, string restaurantId)
        {
            ViewData["cityId"] = cityId;
            bool cityExists = await cityService.DoesCityExistsByIdAsync(cityId);

            if (!cityExists)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "This city does not exist!";
                return RedirectToAction("Index", "Home");
            }

            bool restaurantExists = await restaurantService.DoesRestaurantExistsByIdAsync(restaurantId);

            if (!restaurantExists)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "This restaurant does not exist!";
                return RedirectToAction("AllRestaurantsInACity", "Restaurant", new { cityId });
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isEmployee = await this.employeeService.EmployeeExistsByUserIdAsync(userId);

            if (!isEmployee)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "You must be part of our team in order to edit a restaurant!";
                return this.RedirectToAction("JoinUs", "Employee");
            }

            try
            {
                RestaurantFormModel formModel = await this.restaurantService.GetRestaurantForEditAsync(restaurantId);

                return this.View(formModel);
            }
            catch (Exception)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Please try again later or contact us";
                return this.RedirectToAction("AllRestaurantsInACity", "Restaurant", new { cityId });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditRestaurant(string cityId, string restaurantId, RestaurantFormModel model)
        {
            bool cityExists = await cityService.DoesCityExistsByIdAsync(cityId);

            if (!cityExists)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "This city does not exist!";
                return RedirectToAction("Index", "Home");
            }

            bool restaurantExists = await restaurantService.DoesRestaurantExistsByIdAsync(restaurantId);

            if (!restaurantExists)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "This restaurant does not exist!";
                return RedirectToAction("AllRestaurantsInACity", "Restaurant", new { cityId });
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isEmployee = await this.employeeService.EmployeeExistsByUserIdAsync(userId);

            if (!isEmployee)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "You must be part of our team in order to edit a restaurant!";
                return this.RedirectToAction("JoinUs", "Employee");
            }

            try
            {
                await this.restaurantService.EditRestaurantByIdAndFormModelAsync(int.Parse(restaurantId), model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to update the Restaurant. Please try again later or contact the administrator!");
                return View(model);
            }

            this.TempData[NotificationMessagesConstants.SuccessMessage] = "The restaurant was edited successfully!";
            return RedirectToAction("AllRestaurantsInACity", "Restaurant", new { cityId });
        }

        [HttpGet]
        
        public async Task<IActionResult> AllRestaurants(string cityId)
        {
            List<RestaurantAllModel> allRestaurants = new List<RestaurantAllModel>();

            try
            {
                allRestaurants.AddRange(await restaurantService.AllRestaurantsAsync());

                if (allRestaurants.Count == 0)
                {
                    ModelState.AddModelError("", "No restaurants found.");
                }


                return View(allRestaurants);
            }
            catch (Exception ex)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Please try again later or contact us";

                // Get the previous URL from Referer header
                string previousUrl = Request.Headers["Referer"].ToString();
                return Redirect(previousUrl);
            }
        }



    }

    



    
}
