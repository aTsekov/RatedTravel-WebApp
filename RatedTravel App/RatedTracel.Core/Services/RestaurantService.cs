using Microsoft.EntityFrameworkCore;
using RatedTravel.Core.Interfaces;
using RatedTravel.Data;
using RatedTravel.Web.ViewModels.Restaurant;
using City = RatedTravel.Data.DataModels.City;
using Restaurant = RatedTravel.Data.DataModels.Restaurant;
using RestaurantReviewAndRate = RatedTravel.Data.DataModels.RestaurantReviewAndRate;

namespace RatedTravel.Core.Services
{

    public class RestaurantService : IRestaurantService
    {

        private readonly RatedTravelDbContext dbContext;
        private readonly ICityService cityService;
        private readonly IEmployeeService employeeService;

        public RestaurantService(RatedTravelDbContext dbContext, ICityService cityService, IEmployeeService employeeService)
        {
            this.dbContext = dbContext;
            this.cityService = cityService;
            this.employeeService = employeeService;
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
                .Where(r => r.City.Id.ToString() == cityId && r.IsActive)
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
            if (string.IsNullOrEmpty(cityId) || string.IsNullOrEmpty(restaurantId))
            {
                throw new ArgumentException("CityId and RestaurantId cannot be null or empty.");
            }

            var city = await dbContext.Cities
                .Where(c => c.Id.ToString() == cityId && c.IsActive)
                .FirstOrDefaultAsync();

            if (city == null)
            {
                throw new ArgumentException("City not found.");
            }

            var restaurant = await dbContext.Restaurants
                .Include(r => r.Reviews.Where(review => review.IsActive == true))
                .Where(r => r.City.Id.ToString() == cityId && r.Id.ToString() == restaurantId && r.IsActive == true)
                .FirstOrDefaultAsync();

            if (restaurant == null)
            {
                throw new ArgumentException("Restaurant not found.");
            }

            double totalScore = await GetOverallScoreOfRestaurant(restaurant.Id.ToString());

            var model = new RestaurantDetailsView
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                CityName = city.Name,
                Image = restaurant.ImageUrl,
                Address = restaurant.Address,
                Description = restaurant.Description,
                OverallScore = totalScore,
                UserId = restaurant.UserId.ToString(),
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
                .Where(s => s.IsActive && s.RestaurantId.ToString() == restaurantId)
                .Select(s => s.FoodRate)
                .ToListAsync();



            List<int> locationRates = await dbContext.RestaurantReviewsAndRates
                .Where(s => s.IsActive && s.RestaurantId.ToString() == restaurantId)
                .Select(s => s.LocationRate)
                .ToListAsync();

            List<int> priceRates = await dbContext.RestaurantReviewsAndRates
                .Where(s => s.IsActive && s.RestaurantId.ToString() == restaurantId)
                .Select(s => s.PriceRate)
                .ToListAsync();

            List<int> serviceRates = await dbContext.RestaurantReviewsAndRates
                .Where(s => s.IsActive && s.RestaurantId.ToString() == restaurantId)
                .Select(s => s.ServiceRate)
                .ToListAsync();

            // Check if any rates are available
            if (foodRates.Count == 0 || locationRates.Count == 0 || priceRates.Count == 0 || serviceRates.Count == 0)
            {

                return 0;
            }

            double totalScore = (foodRates.Average() + locationRates.Average() + priceRates.Average() +
                                 serviceRates.Average()) / 4.0; // Convert to double by adding ".0"
            double roundedScore = Math.Round(totalScore, 1);

            return roundedScore;
        }

        public async Task SendReviewAsync(string restaurantId, RestaurantRateAndReviewModel model)
        {
            var restaurant = await dbContext.Restaurants
                .FirstOrDefaultAsync(r => r.Id.ToString() == restaurantId);

            if (restaurant == null)
            {
                throw new ArgumentException("Restaurant not found.");
            }

            RestaurantReviewAndRate review = new RestaurantReviewAndRate
            {
                ReviewText = model.ReviewText,
                FoodRate = model.FoodRate,
                LocationRate = model.LocationRate,
                PriceRate = model.PriceRate,
                ServiceRate = model.ServiceRate,
                RestaurantId = restaurant.Id
            };

            await dbContext.RestaurantReviewsAndRates.AddAsync(review);
            await dbContext.SaveChangesAsync();
        }




        public async Task<RestaurantFormModel> GetRestaurantForEditAsync(string restaurantId)
        {
            Restaurant? restaurantToEdit = await this.dbContext.Restaurants
                .Where(r => r.IsActive == true && r.Id == int.Parse(restaurantId))
                .FirstOrDefaultAsync();

            if (restaurantToEdit == null)
            {
                // Handle the case where the restaurant with the provided ID does not exist.
                throw new ArgumentException("Invalid restaurant ID.");
            }

            City? city = await this.dbContext.Cities
                .Where(c => c.IsActive == true && c.Id == restaurantToEdit.CityId)
                .FirstOrDefaultAsync();

            if (city == null)
            {
                throw new ArgumentException("Invalid city ID.");
            }

            return new RestaurantFormModel
            {
                Id = restaurantToEdit.Id,
                Name = restaurantToEdit.Name,
                CityName = city.Name,
                Address = restaurantToEdit.Address,
                Description = restaurantToEdit.Description,
                OverallScore = restaurantToEdit.OverallScore,

            };
        }



        public async Task EditRestaurantByIdAndFormModelAsync(int restaurantId, RestaurantFormModel restaurantFormModel)
        {
            Restaurant? restaurant = await this.dbContext.Restaurants
                .Where(r => r.IsActive == true && r.Id == restaurantId)
                .FirstOrDefaultAsync();

            City? city = await this.dbContext.Cities
                .Where(c => c.IsActive == true && c.Id.ToString() == restaurantFormModel.CityId)
                .FirstOrDefaultAsync();

            if (city == null)
            {
                throw new ArgumentException("Invalid city ID.");
            }

            if (restaurant == null)
            {
                throw new ArgumentException("Invalid restaurant ID.");
            }

            restaurant.Name = restaurantFormModel.Name;
            restaurant.City.Name = city.Name;
            restaurant.Address = restaurantFormModel.Address;
            restaurant.Description = restaurantFormModel.Description;
            restaurant.OverallScore = restaurantFormModel.OverallScore;

            // Handle the uploaded image file
            if (restaurantFormModel.ImageFile != null && restaurantFormModel.ImageFile.Length > 0)
            {
                // Generate the filename for the image using the restaurant name
                var extension = Path.GetExtension(restaurantFormModel.ImageFile.FileName);
                var fileName = restaurantFormModel.Name + extension;
                var filePath = Path.Combine("wwwroot", "images", fileName);

                // Save the image file to the specified path
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await restaurantFormModel.ImageFile.CopyToAsync(fileStream);
                }

                // Update the restaurant entity with the image path
                restaurant.ImageUrl = fileName;
            }

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<RestaurantAllModel>> AllRestaurantsAsync()
        {
            List<RestaurantAllModel> restaurants = await dbContext.Restaurants
                .Where(r => r.IsActive)
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


        public async Task<RestaurantDeleteModel> GetRestaurantForDeleteAsync(string restaurantId)
        {
            Restaurant? restaurantToDelete = await dbContext.Restaurants
                .Include(r => r.City)
                .Where(r => r.IsActive)
                .FirstOrDefaultAsync(r => r.Id.ToString() == restaurantId);

            return new RestaurantDeleteModel
            {
                Id = restaurantToDelete.Id.ToString(),
                Name = restaurantToDelete.Name,
                City = restaurantToDelete.City.Name,
                ImageUrl = restaurantToDelete.ImageUrl
            };
        }

        public async Task DeleteReviewByIdAsync(string reviewId)
        {
            var reviewToDelete = await dbContext.RestaurantReviewsAndRates.Where(r => r.IsActive)
                .FirstOrDefaultAsync(r => r.Id.ToString() == reviewId);

            if (reviewToDelete == null)
            {
                throw new ArgumentException($"Review with ID '{reviewId}' not found.");
            }

            reviewToDelete.IsActive = false;

            await dbContext.SaveChangesAsync();
        }


        public async Task DeleteRestaurantByIdAsync(string restaurantId)
        {
            Restaurant? restaurantToDelete = await dbContext.Restaurants
                .Where(r => r.IsActive)
                .FirstOrDefaultAsync(r => r.Id.ToString() == restaurantId);

            restaurantToDelete.IsActive = false;
            await dbContext.SaveChangesAsync();
        }


    }


}