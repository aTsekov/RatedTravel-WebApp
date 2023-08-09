using Microsoft.EntityFrameworkCore;
using RatedTravel.Core.Interfaces;
using RatedTravel.Core.Services;
using RatedTravel.Data;
using RatedTravel.Web.ViewModels.City;
using static RatedTravel.Tests.DatabaseSeeder;
namespace RatedTravel.Tests
{
    public class CityServiceTests
    {

        private DbContextOptions<RatedTravelDbContext> dbOptions;
        private RatedTravelDbContext dbContext;
        private RatedTravelDbContext dbContextC;


        private ICityService cityService;
        private ICityService cityServiceC;

        [SetUp]
        public void Setup()
        {
            this.dbOptions = new DbContextOptionsBuilder<RatedTravelDbContext>()
                .UseInMemoryDatabase("RatedTravelInMemoryDb" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RatedTravelDbContext(this.dbOptions);
            this.dbContextC = new RatedTravelDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.cityService = new CityService(this.dbContext);
            this.cityServiceC = new CityService(this.dbContextC);
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

            var result = await cityService.AllCitiesAsync();

            Assert.IsTrue(result.Count() == ActiveCities.Count());
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
        public async Task CreateCityAsync_ShouldAddNewCityToDatabase()
        {
            // Arrange
            string emplId = MyEmployee.Id.ToString();
            string userId = MyEmployee.UserId.ToString();
            var cityFormModel = new CityFormModel
            {
                Name = "New City",
                Country = "New Country",
                Description = "New Description",
                NightlifeScore = 4,
                TransportScore = 4,
            };

            // Act
            await cityServiceC.CreateCityAsync(emplId, userId, cityFormModel);

            // Assert
            var newCity = await dbContext.Cities.FirstOrDefaultAsync(c => c.Name == cityFormModel.Name);
            Assert.NotNull(newCity);
            Assert.AreEqual(cityFormModel.Name, newCity.Name);
            Assert.AreEqual(cityFormModel.Country, newCity.Country);
            Assert.AreEqual(cityFormModel.Description, newCity.Description);
            Assert.AreEqual(cityFormModel.NightlifeScore, newCity.NightlifeScore);
            Assert.AreEqual(cityFormModel.TransportScore, newCity.TransportScore);

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


        public async Task CreateCityAsync_Should_ThrowExceptionForInvalidEmployeeId()
        {
            // Arrange
            string emplId = "CA551B7B-D085-45E5-B26D-F62B7D6965EE";
            string userId = MyEmployee.UserId.ToString();
            var cityFormModel = new CityFormModel
            {
                Name = "New City",
                Country = "New Country",
                Description = "New Description",
                NightlifeScore = 4,
                TransportScore = 4,
            };

            // Act & Assert
            try
            {
                await cityServiceC.CreateCityAsync(emplId, userId, cityFormModel);
            }
            catch (ArgumentException ex)
            {
                // Assert
                Assert.AreEqual("Invalid employee ID.", ex.Message);
                return;
            }
            
            Assert.Fail("Expected ArgumentException was not thrown.");
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }


}