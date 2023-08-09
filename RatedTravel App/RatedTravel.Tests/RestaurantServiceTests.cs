using Microsoft.EntityFrameworkCore;
using RatedTravel.Core.Interfaces;
using RatedTravel.Core.Services;
using RatedTravel.Data;
using RatedTravel.Web.ViewModels.Restaurant;
using static RatedTravel.Tests.DatabaseSeeder;

namespace RatedTravel.Tests
{
    public class RestaurantServiceTests
    {
        private DbContextOptions<RatedTravelDbContext> dbOptions;
        private RatedTravelDbContext dbContext;

        private ICityService cityService;
        private IRestaurantService restaurantService;

        private readonly IEmployeeService employeeService;

        [SetUp]
        public void Setup()
        {
            this.dbOptions = new DbContextOptionsBuilder<RatedTravelDbContext>()
                .UseInMemoryDatabase("RatedTravelInMemoryDb" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RatedTravelDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.cityService = new CityService(this.dbContext);
            this.restaurantService = new RestaurantService(this.dbContext, this.cityService, employeeService);

        }




        [Test]
        public async Task GetOverallScoreOfRestaurant_Should_CalculateAverageScoreCorrectly()
        {
            // Arrange
            var restaurantId = "1";

            // Act
            var overallScore = await restaurantService.GetOverallScoreOfRestaurant(restaurantId);

            // Assert
            Assert.AreEqual(4.0, overallScore);
        }

        [Test]
        public async Task DoesRestaurantExistsByIdAsync_Should_ReturnTrueForExistingRestaurantId()
        {
            // Arrange
            var restaurantId = "1"; 

            // Act
            var result = await restaurantService.DoesRestaurantExistsByIdAsync(restaurantId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DoesRestaurantExistsByIdAsync_Should_ReturnFalseForNonExistingRestaurantId()
        {
            // Arrange
            var restaurantId = "999"; 

            // Act
            var result = await restaurantService.DoesRestaurantExistsByIdAsync(restaurantId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DoesRestaurantExistsByName_Should_ReturnTrueForExistingRestaurantName()
        {
            // Arrange
            var restaurantName = "The Bistro"; 

            // Act
            var result = await restaurantService.DoesRestaurantExistsByName(restaurantName);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DoesRestaurantExistsByName_Should_ReturnFalseForNonExistingRestaurantName()
        {
            // Arrange
            var restaurantName = "NonExistingRestaurant"; 

            // Act
            var result = await restaurantService.DoesRestaurantExistsByName(restaurantName);

            // Assert
            Assert.IsFalse(result);
        }


        [Test]
        public async Task AllRestaurantsAsync_Should_ReturnAllActiveRestaurants()
        {
            // Act
            var result = await restaurantService.AllRestaurantsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count()); 
        }

        [Test]
        public async Task GetRestaurantForEditAsync_Should_ReturnCorrectRestaurantForEdit()
        {
            // Arrange
            var restaurantId = "1"; 

            // Act
            var result = await restaurantService.GetRestaurantForEditAsync(restaurantId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("London", result.CityName);

        }




        [Test]
        public async Task DetailsAsync_Should_ThrowExceptionForInvalidCityId()
        {
            // Arrange
            var cityId = "999"; 
            var restaurantId = "1"; 

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await restaurantService.DetailsAsync(cityId, restaurantId);
            }, "City not found.");
        }

        [Test]
        public async Task DetailsAsync_Should_ThrowExceptionForInvalidRestaurantId()
        {
            // Arrange
            var cityId = "1"; 
            var restaurantId = "999"; 

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await restaurantService.DetailsAsync(cityId, restaurantId);
            }, "Restaurant not found.");
        }



        [Test]
        public async Task SendReviewAsync_Should_ThrowExceptionForNonExistingRestaurant()
        {
            // Arrange
            var restaurantId = "999"; 
            var model = new RestaurantRateAndReviewModel
            {
                ReviewText = "Test review",
                FoodRate = 4,
                LocationRate = 5,
                PriceRate = 3,
                ServiceRate = 4
            };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await restaurantService.SendReviewAsync(restaurantId, model);
            }, "Restaurant not found.");
        }

        [Test]
        public async Task GetRestaurantForEditAsync_Should_ThrowExceptionForNonExistingRestaurant()
        {
            // Arrange
            var restaurantId = "999"; 

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await restaurantService.GetRestaurantForEditAsync(restaurantId);
            }, "Invalid restaurant ID.");
        }



        [Test]
        public async Task EditRestaurantByIdAndFormModelAsync_Should_ThrowExceptionForInvalidRestaurantId()
        {
            // Arrange
            var restaurantId = 999; 
            var restaurantFormModel = new RestaurantFormModel
            {
                Id = 1,
                Name = "Updated Restaurant",
                CityName = "City 2", 
                Address = "Updated Address",
                Description = "Updated Description",
                OverallScore = 4
            };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await restaurantService.EditRestaurantByIdAndFormModelAsync(restaurantId, restaurantFormModel);
            }, "Invalid restaurant ID.");
        }

        [Test]
        public async Task AllRestaurantsInACityAsync_Should_ReturnAllActiveRestaurantsInCity()
        {
            // Arrange
            var cityId = "7e980128-41f1-4351-b11f-2e9ac6d0cade"; 

            // Act
            var result = await restaurantService.AllRestaurantsInACityAsync(cityId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count()); 
        }

        [Test]
        public async Task AllRestaurantsInACityAsync_Should_ReturnEmptyListForNonExistingCity()
        {
            // Arrange
            var cityId = "999"; 

            // Act
            var result = await restaurantService.AllRestaurantsInACityAsync(cityId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }



        [Test]
        public async Task DeleteReviewByIdAsync_Should_ThrowExceptionForInvalidReviewId()
        {
            // Arrange
            var reviewId = "999"; 

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await restaurantService.DeleteReviewByIdAsync(reviewId);
            }, $"Review with ID '{reviewId}' not found.");
        }

     

        [Test]
        public async Task DetailsAsync_Should_ReturnCorrectRestaurantDetails()
        {
            // Arrange
            var cityId = City1.Id.ToString();
            var restaurantId = Restaurant1.Id.ToString();

            // Act
            var result = await restaurantService.DetailsAsync(cityId, restaurantId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(Restaurant1.Id, result.Id);
            Assert.AreEqual(Restaurant1.Name, result.Name);
            Assert.AreEqual(City1.Name, result.CityName);
            Assert.AreEqual(Restaurant1.ImageUrl, result.Image);
            Assert.AreEqual(Restaurant1.Description, result.Description);
            Assert.AreEqual(Restaurant1.Address, result.Address);
            Assert.AreEqual(Restaurant1.UserId.ToString(), result.UserId);
            Assert.AreEqual(Restaurant1.City.Id.ToString(), result.CityId);
            Assert.AreEqual(Restaurant1.Reviews.Count(r => r.IsActive), result.Reviews.Count());
        }


        [Test]
        public async Task DeleteReviewByIdAsync_Should_ThrowException_WhenInvalidReviewId()
        {
            // Arrange
            var reviewId = "999"; 

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await restaurantService.DeleteReviewByIdAsync(reviewId);
            }, $"Review with ID '{reviewId}' not found.");
        }


        [Test]
        public async Task DoesRestaurantExistsByIdAsync_Should_ReturnTrueForExistingRestaurant()
        {
            // Arrange
            var restaurantId = Restaurant1.Id.ToString();

            // Act
            var result = await restaurantService.DoesRestaurantExistsByIdAsync(restaurantId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DoesRestaurantExistsByIdAsync_Should_ReturnFalseForNonExistingRestaurant()
        {
            // Arrange
            var restaurantId = "999"; 

            // Act
            var result = await restaurantService.DoesRestaurantExistsByIdAsync(restaurantId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DoesRestaurantExistsByName_Should_ReturnTrueForExistingRestaurant()
        {
            // Arrange
            var restaurantName = Restaurant1.Name;

            // Act
            var result = await restaurantService.DoesRestaurantExistsByName(restaurantName);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DoesRestaurantExistsByName_Should_ReturnFalseForNonExistingRestaurant()
        {
            // Arrange
            var restaurantName = "NonExistingRestaurant";

            // Act
            var result = await restaurantService.DoesRestaurantExistsByName(restaurantName);

            // Assert
            Assert.IsFalse(result);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}

