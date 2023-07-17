using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RatedTravel.Core.Interfaces;
using RatedTravel.Data;
using RatedTravel.Data.DataModels;
using RatedTravel.Web.ViewModels.Restaurant;
using static RaterTravel.Common.EntityValidationConstants;
using City = RatedTravel.Data.DataModels.City;
using Restaurant = RatedTravel.Data.DataModels.Restaurant;

namespace RatedTravel.Core.Services
{

    public class RestaurantService : IRestaurantService
    {

        private readonly RatedTravelDbContext dbContext;
        private readonly ICityService cityService;

        public RestaurantService(RatedTravelDbContext dbContext, ICityService cityService)
        {
            this.dbContext = dbContext;
            this.cityService = cityService;
        }


        public async Task CreateRestaurantAsync(string userId, RestaurantFormModel formModel)
        {

            var cityIdOfResto = await cityService.GetCityIdByName(formModel.CityName);


            Restaurant newResto = new Restaurant
            {
                Name = formModel.Name,
                Address = formModel.Address,
                Description = formModel.Description,
                OverallScore = formModel.OverallScore,
                UserId = Guid.Parse(userId),
                CityId = Guid.Parse(cityIdOfResto),
            };

            // Handle the uploaded image file
            if (formModel.ImageFile != null && formModel.ImageFile.Length > 0)
            {
                // Generate the filename for the image using the city name
                var extension = Path.GetExtension(formModel.ImageFile.FileName);
                var fileName = formModel.Name + extension;
                var filePath = Path.Combine("wwwroot", "images", fileName);

                // Save the image file to the specified path
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formModel.ImageFile.CopyToAsync(fileStream);
                }

                // Update the city entity with the image path
                newResto.ImageUrl = fileName;
            }
            else
            {
                // If the image file is not uploaded, set the image URL to null
                newResto.ImageUrl = null;
            }

            await this.dbContext.Restaurants.AddAsync(newResto);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> DoesRestaurantExistsByIdAsync(string restaurantId)
        {
            return await dbContext.Restaurants.Where(r => r.IsActive == true).AnyAsync(c => c.Id.ToString() == restaurantId);
        }
    }
}
