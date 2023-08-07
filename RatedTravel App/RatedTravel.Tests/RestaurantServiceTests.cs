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


        private IRestaurantService restaurantService;
        private ICityService cityService;
        private IEmployeeService emlpService;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

            this.dbOptions = new DbContextOptionsBuilder<RatedTravelDbContext>()
                .UseInMemoryDatabase("RatedTravelInMemoryDb" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RatedTravelDbContext(this.dbOptions);

            SeedDatabase(this.dbContext);

            this.dbContext.Database.EnsureCreated();


            this.restaurantService = new RestaurantService(this.dbContext, cityService,emlpService);


        }

        [SetUp]
        public void SetUp()
        {
            //Create a new instance of the context for each test method

            this.dbContext = new RatedTravelDbContext(this.dbOptions);



        }

        [TearDown]
        public void TearDown()
        {
            //Dispose of the context after each test method
            this.dbContext.Dispose();
        }
        // Test AllRestaurantsAsync method
       

        // Test GetRestaurantForEditAsync method
        [Test]
        public async Task GetRestaurantForEditAsync_Should_Return_RestaurantFormModel_With_Valid_Id()
        {
            // Arrange
            var restaurantId = "1"; // Existing restaurant ID

            // Act
            var result = await restaurantService.GetRestaurantForEditAsync(restaurantId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(restaurantId, result.Id.ToString());
            // Add more specific assertions based on the expected result.
        }

       

        // Test GetRestaurantForDeleteAsync method
        [Test]
        public async Task GetRestaurantForDeleteAsync_Should_Return_RestaurantDeleteModel_With_Valid_Id()
        {
            // Arrange
            var restaurantId = "1"; // Existing restaurant ID

            // Act
            var result = await restaurantService.GetRestaurantForDeleteAsync(restaurantId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(restaurantId, result.Id);
            // Add more specific assertions based on the expected result.
        }

       

        // Test DoesRestaurantExistsByIdAsync method
        [Test]
        public async Task DoesRestaurantExistsByIdAsync_Should_Return_True_If_Restaurant_Exists()
        {
            // Arrange
            var restaurantId = Restaurant1.Id; // Existing restaurant ID
            // Act
            var result = await restaurantService.DoesRestaurantExistsByIdAsync(restaurantId.ToString());

            // Assert
            Assert.IsTrue(result);
        }

        // Test DoesRestaurantExistsByName method
        [Test]
        public async Task DoesRestaurantExistsByName_Should_Return_True_If_Restaurant_Exists()
        {
            // Arrange
            var restaurantName = "The Bistro"; // Existing restaurant name

            // Act
            var result = await restaurantService.DoesRestaurantExistsByName(restaurantName);

            // Assert
            Assert.IsTrue(result);
        }

        // Test AllRestaurantsInACityAsync method
       


    }
}
