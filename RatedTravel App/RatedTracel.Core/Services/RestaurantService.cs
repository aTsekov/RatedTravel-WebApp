﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RatedTravel.Core.Interfaces;
using RatedTravel.Data;
using RatedTravel.Data.DataModels;
using RatedTravel.Web.ViewModels.City;
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

        public async Task<bool> DoesRestaurantExistsByName(string restaurantName)
        {
            return await dbContext.Restaurants.Where(r => r.IsActive).AnyAsync(r => r.Name == restaurantName);
        }

		public async Task<IEnumerable<RestaurantAllModel>> AllRestaurantsInACityAsync(string cityId)
		{
			List<RestaurantAllModel> restaurants = await dbContext.Restaurants
				.Where(r => r.City.Id.ToString() == cityId)
				.Select(r => new RestaurantAllModel
				{
					Id = r.Id,
					Name = r.Name,
					CityName = r.City.Name,
					Image = r.ImageUrl,
					Description = r.Description,
					Address = r.Address,
					UserId = r.UserId.ToString(),
					CityId = r.City.Id.ToString()
				})
				.ToListAsync();

			foreach (var resto in restaurants)
			{
				double totalScore = await GetOverallScoreOfRestaurant(resto.Id.ToString());
				resto.OverallScore = totalScore;
			}

			return restaurants;
		}

		public async Task<RestaurantDetailsView> DetailsAsync(string cityId, string restaurantId)
		{

			var restaurant = await dbContext.Restaurants
				.Include(r => r.Reviews)
				.Where(r => r.City.Id.ToString() == cityId && r.Id.ToString() == restaurantId)
				.FirstOrDefaultAsync();


			double totalScore = await GetOverallScoreOfRestaurant(restaurant.Id.ToString());

			var model = new RestaurantDetailsView
			{
				Id = restaurant.Id,
				Name = restaurant.Name,
				CityName = restaurant.City.Name,
				Image = restaurant.ImageUrl,
				Address = restaurant.Address,
				Description = restaurant.Description,
				OverallScore = totalScore,
				UserId = restaurant.UserId.ToString(),
				EmployeeId = restaurant.EmployeeId.ToString(),
				CityId = restaurant.City.Id.ToString(),
				Reviews = restaurant.Reviews.Select(r => new RestaurantReviewModel
				{
					ReviewId = r.Id,
					ReviewText = r.ReviewText,
					LocationRate = r.LocationRate,
					FoodRate = r.FoodRate,
					PriceRate = r.PriceRate,
					ServiceRate = r.ServiceRate
				})
			};

			return model;
		}
	


		public async Task<double> GetOverallScoreOfRestaurant(string restaurantId)
		{
			List<int> foodRates = await dbContext.RestaurantReviewsAndRates
				.Where(s => s.IsActive && s.Id.ToString() == restaurantId)
				.Select(s => s.FoodRate)
				.ToListAsync();

			List<int> locationRates = await dbContext.RestaurantReviewsAndRates
				.Where(s => s.IsActive && s.Id.ToString() == restaurantId)
				.Select(s => s.LocationRate)
				.ToListAsync();

			List<int> priceRates = await dbContext.RestaurantReviewsAndRates
				.Where(s => s.IsActive && s.Id.ToString() == restaurantId)
				.Select(s => s.PriceRate)
				.ToListAsync();

			List<int> serviceRates = await dbContext.RestaurantReviewsAndRates
				.Where(s => s.IsActive && s.Id.ToString() == restaurantId)
				.Select(s => s.ServiceRate)
				.ToListAsync();

			// Check if any rates are available
			if (foodRates.Count == 0 || locationRates.Count == 0 || priceRates.Count == 0 || serviceRates.Count == 0)
			{

				return 0;
			}

			double totalScore = (foodRates.Average() + locationRates.Average() + priceRates.Average() +
			                     serviceRates.Average()) / 4.0; // Convert to double by adding ".0"


			return totalScore;
		}
	}


}
