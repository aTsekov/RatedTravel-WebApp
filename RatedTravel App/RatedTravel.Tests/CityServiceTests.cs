using Microsoft.EntityFrameworkCore;
using RatedTravel.Core.Interfaces;
using RatedTravel.Core.Services;
using RatedTravel.Data;
using RatedTravel.Data.DataModels;
using RatedTravel.Web.ViewModels.City;
using static RatedTravel.Tests.DatabaseSeeder;

namespace RatedTravel.Tests
{
    [TestFixture]
    public class CityServiceTests
    {

        private DbContextOptions<RatedTravelDbContext> dbOptions;
        private RatedTravelDbContext dbContext;


        private ICityService cityService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

            this.dbOptions = new DbContextOptionsBuilder<RatedTravelDbContext>()
                .UseInMemoryDatabase("RatedTravelInMemoryDb" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RatedTravelDbContext(this.dbOptions);

            SeedDatabase(this.dbContext);

            this.dbContext.Database.EnsureCreated();


            this.cityService = new CityService(this.dbContext);


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

        [Test]
        public async Task OurCitiesAsync_ShouldReturnActiveCities()
        {
            // Act
            var result = await cityService.OurCitiesAsync();

            // Assert
            Assert.IsTrue(result.Count() == ActiveCities.Count);
        }

        [Test]
        public async Task AllCitiesAsync_ShouldReturnActiveCities()
        {
            // Act
            var result = await cityService.AllCitiesAsync();

            // Assert
            Assert.IsTrue(result.Count() == InactiveCities.Count);
        }

        [Test]
        public async Task DoesCityExistsAsync_ShouldReturnTrueForExistingCity()
        {

            // Arrange
            string cityName = "London";

            // Act
            var result = await cityService.DoesCityExistsAsync(cityName);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DoesCityExistsAsync_ShouldReturnFalseForNonExistingCity()
        {
            // Arrange
            string cityName = "NonExistingCity";

            // Act
            var result = await cityService.DoesCityExistsAsync(cityName);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DoesCityExistsByIdAsync_ShouldReturnTrueForExistingCityId()
        {
            // Arrange
            string cityId = City1.Id.ToString();

            // Act
            var result = await cityService.DoesCityExistsByIdAsync(cityId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DoesCityExistsByIdAsync_ShouldReturnFalseForNonExistingCityId()
        {
            // Arrange
            string cityId = Guid.NewGuid().ToString();

            // Act
            var result = await cityService.DoesCityExistsByIdAsync(cityId);

            // Assert
            Assert.IsFalse(result);
        }




        [Test]
        public async Task GetCityIdByName_ShouldReturnCityId()
        {
            // Arrange
            string cityName = "London";

            // Act
            var result = await cityService.GetCityIdByName(cityName);

            // Assert
            Assert.AreEqual(City1.Id.ToString(), result);
        }



        [Test]
        public async Task SelectCityAsync_ShouldReturnCitySelectModel()
        {
            // Arrange
            string cityId = City1.Id.ToString();

            // Act
            var result = await cityService.SelectCityAsync(cityId);

            // Assert
            Assert.AreEqual(cityId, result.Id);

        }

        [Test]
        public async Task GetCityForDelete_ShouldReturnCityDeleteModel()
        {
            // Arrange
            string cityId = City1.Id.ToString();

            // Act
            var result = await cityService.GetCityForDelete(cityId);

            // Assert
            Assert.AreEqual(City1.Name, result.Name);
            Assert.AreEqual(City1.Country, result.Country);
            Assert.AreEqual(City1.ImageUrl, result.ImageUrl);

        }

        [Test]
        public async Task EditAsync_ShouldReturnCityFormModel()
        {
            // Arrange
            string cityId = City1.Id.ToString();

            // Act
            var result = await cityService.EditAsync(cityId);

            // Assert
            Assert.AreEqual(City1.Name, result.Name);
            Assert.AreEqual(City1.Country, result.Country);
            Assert.AreEqual(City1.Description, result.Description);
            Assert.AreEqual(City1.NightlifeScore, result.NightlifeScore);
            Assert.AreEqual(City1.TransportScore, result.TransportScore);

        }
    }
}

