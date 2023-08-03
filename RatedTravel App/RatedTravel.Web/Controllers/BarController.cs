using Microsoft.AspNetCore.Mvc;
using RatedTravel.Core.Interfaces;
using RatedTravel.Core.Services;
using RaterTravel.Common;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RatedTravel.Web.ViewModels.Bar;
using RatedTravel.Data.DataModels;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Collections.Generic;

namespace RatedTravel.App.Web.Controllers
{
    [Authorize]
    public class BarController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly ICityService cityService;
        private readonly IBarService barService;
        private readonly UserManager<ApplicationUser> userManager;

        public BarController(IEmployeeService employeeService, ICityService cityService, IBarService barService, UserManager<ApplicationUser> userManager)
        {
            this.cityService = cityService;
            this.employeeService = employeeService;
            this.barService = barService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> AddBar()
        {
            var user = await userManager.GetUserAsync(User);
            bool isUser = user != null;

            if (!isUser)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] =
                    "You have to be logged in to add a bar!";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBar(BarFormModel model)
        {
            var user = await userManager.GetUserAsync(User);
            bool isUser = user != null;

            bool doesBarExistByName = await barService.DoesBarExistsByName(model.Name);

            if (doesBarExistByName)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] =
                    "The bar already exists. To add more bars of the same chain, edit the existing bar and add the address of the new locations.";
                return RedirectToAction("Index", "Home");
            }

            if (!isUser)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] =
                    "You have to be logged in to add a bar!";
                return RedirectToAction("Index", "Home");
            }

            bool doesCityExists = await this.cityService.DoesCityExistsAsync(model.CityName);

            if (!doesCityExists)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "This city does not exist! Try another one.";
            }

            try
            {
                // Handle the uploaded image file
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    // Generate the filename for the image using the bar name
                    var fileName = model.Name + "_" + model.ImageFile.FileName;

                    // Define the file path within the wwwroot folder where the image will be saved
                    var filePath = Path.Combine("wwwroot", "images", fileName);

                    // Save the image file to the specified path
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(fileStream);
                    }

                    await barService.CreateBarAsync(user.Id.ToString(), model);
                }
            }
            catch (Exception)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Are you sure you are logged in?";
                return this.RedirectToAction("Index", "Home");
            }

            //TODO: to return to ALL bars.
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AllBarsInACity(string cityId)
        {
            List<BarAllModel> allBarsInACity = new List<BarAllModel>();

            try
            {
                allBarsInACity.AddRange(await barService.AllBarsInACityAsync(cityId));

                if (allBarsInACity.Count == 0)
                {
                    ModelState.AddModelError("", "No bars found in the selected city.");
                }

                if (!ModelState.IsValid)
                {
                    this.TempData[NotificationMessagesConstants.ErrorMessage] = "We couldn't find bars in this city.";

                    // Get the previous URL from Referer header
                    string previousUrl = Request.Headers["Referer"].ToString();
                    return Redirect(previousUrl);
                }

                return View(allBarsInACity);
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting
                // You can use a logger service, for example: logger.LogError(ex, "Error in AllBarsInACity action");

                this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Please try again later or contact us";

                // Get the previous URL from Referer header
                string previousUrl = Request.Headers["Referer"].ToString();
                return Redirect(previousUrl);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> BarDetails(string cityId, string barId)
        {
            try
            {
                var bar = await barService.DetailsAsync(cityId, barId);

                if (bar == null)
                {
                    ModelState.AddModelError("", "Bar not found");
                    this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Please try again later or contact us";
                    return this.RedirectToAction("Index", "Home");
                }

                return View(bar);
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting
                // You can use a logger service, for example: logger.LogError(ex, "Error in BarDetails action");

                ModelState.AddModelError("", "Oops, something went wrong :( Please try again later or contact us");
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Please try again later or contact us";
                return this.RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SubmitReview(string barId, BarRateAndReviewModel model)
        {
            bool doesBarExistsByName = await barService.DoesBarExistsByIdAsync(model.BarId);

            if (!doesBarExistsByName)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] =
                    "You cannot write a review for this bar - please contact the administrators.";
                string previousUrl = Request.Headers["Referer"].ToString();
                return Redirect(previousUrl);
            }

            try
            {
                await barService.SendReviewAsync(barId, model);
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
                await barService.DeleteReviewByIdAsync(reviewId);
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
        public async Task<IActionResult> DeleteBar(string barId)
        {
            if (string.IsNullOrEmpty(barId))
            {
                TempData[NotificationMessagesConstants.ErrorMessage] = "Invalid barId.";
                string previousUrl = Request.Headers["Referer"].ToString();
                return Redirect(previousUrl);
            }

            try
            {
                BarDeleteModel deleteModel = await barService.GetBarForDeleteAsync(barId);

                return View(deleteModel);
            }
            catch (Exception)
            {
                TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :(";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBar(BarDeleteModel model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                TempData[NotificationMessagesConstants.ErrorMessage] = "Invalid barId.";
                string previousUrl = Request.Headers["Referer"].ToString();
                return Redirect(previousUrl);
            }

            try
            {
                await barService.DeleteBarByIdAsync(model.Id);
                TempData[NotificationMessagesConstants.SuccessMessage] = "Bar deleted successfully!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :(";
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditBar(string cityId, string barId)
        {
            ViewData["cityId"] = cityId;
            bool cityExists = await cityService.DoesCityExistsByIdAsync(cityId);

            if (!cityExists)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "This city does not exist!";
                return RedirectToAction("Index", "Home");
            }

            bool barExists = await barService.DoesBarExistsByIdAsync(barId);

            if (!barExists)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "This bar does not exist!";
                return RedirectToAction("AllBarsInACity", "Bar", new { cityId });
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isEmployee = await this.employeeService.EmployeeExistsByUserIdAsync(userId);

            if (!isEmployee)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "You must be part of our team in order to edit a bar!";
                return this.RedirectToAction("JoinUs", "Employee");
            }

            try
            {
                BarFormModel formModel = await this.barService.GetBarForEditAsync(barId);

                return this.View(formModel);
            }
            catch (Exception)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Please try again later or contact us";
                return this.RedirectToAction("AllBarsInACity", "Bar", new { cityId });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditBar(string cityId, string barId, BarFormModel model)
        {
            bool cityExists = await cityService.DoesCityExistsByIdAsync(cityId);

            if (!cityExists)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "This city does not exist!";
                return RedirectToAction("Index", "Home");
            }

            bool barExists = await barService.DoesBarExistsByIdAsync(barId);

            if (!barExists)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "This bar does not exist!";
                return RedirectToAction("AllBarsInACity", "Bar", new { cityId });
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isEmployee = await this.employeeService.EmployeeExistsByUserIdAsync(userId);

            if (!isEmployee)
            {
                this.TempData[NotificationMessagesConstants.ErrorMessage] = "You must be part of our team in order to edit a bar!";
                return this.RedirectToAction("JoinUs", "Employee");
            }

            try
            {
                await this.barService.EditBarByIdAndFormModelAsync(int.Parse(barId), model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to update the Bar. Please try again later or contact the administrator!");
                return View(model);
            }

            this.TempData[NotificationMessagesConstants.SuccessMessage] = "The bar was edited successfully!";
            return RedirectToAction("AllBarsInACity", "Bar", new { cityId });
        }

        [HttpGet]
        public async Task<IActionResult> AllBars(string cityId)
        {
            List<BarAllModel> allBars = new List<BarAllModel>();

            try
            {
                allBars.AddRange(await barService.AllBarsAsync());

                if (allBars.Count == 0)
                {
                    ModelState.AddModelError("", "No bars found.");
                }

                return View(allBars);
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting
                // You can use a logger service, for example: logger.LogError(ex, "Error in AllBars action");

                this.TempData[NotificationMessagesConstants.ErrorMessage] = "Oops, something went wrong :( Please try again later or contact us";

                // Get the previous URL from Referer header
                string previousUrl = Request.Headers["Referer"].ToString();
                return Redirect(previousUrl);
            }
        }
    }
}
