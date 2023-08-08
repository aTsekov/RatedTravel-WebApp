using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Interfaces;
using RatedTravel.Core.Interfaces;
using RatedTravel.Core.Services;
using RatedTravel.Data;
using RatedTravel.Web.ViewModels.Bar;
using static RatedTravel.Tests.DatabaseSeeder;

namespace RatedTravel.Tests
{
    public class BarServiceTests
    {
        private DbContextOptions<RatedTravelDbContext> dbOptions;
        private RatedTravelDbContext dbContext;
        //private RatedTravelDbContext dbContextB;

        private ICityService cityService;
        private IBarService barService;
        private IBarService barServiceB;
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
            this.barService = new BarService(this.dbContext, this.cityService, employeeService);
            //this.barServiceB = new BarService(this.dbContextB, this.cityService, employeeService);
        }

        [Test]
        public async Task GetOverallScoreOfBar_Should_CalculateAverageScoreCorrectly()
        {
            // Arrange
            var barId = "1";

            // Act
            var overallScore = await barService.GetOverallScoreOfBar(barId);

            // Assert
            Assert.AreEqual(4.0, overallScore);
            // Add more assertions as needed to validate the data
        }

        [Test]
        public async Task DoesBarExistsByIdAsync_Should_ReturnTrueForExistingBarId()
        {
            // Arrange
            var barId = "1"; // Assuming this ID exists in the seeded data

            // Act
            var result = await barService.DoesBarExistsByIdAsync(barId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DoesBarExistsByIdAsync_Should_ReturnFalseForNonExistingBarId()
        {
            // Arrange
            var barId = "999"; // Assuming this ID does not exist in the seeded data

            // Act
            var result = await barService.DoesBarExistsByIdAsync(barId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DoesBarExistsByName_Should_ReturnTrueForExistingBarName()
        {
            // Arrange
            var barName = "The Pub";

            // Act
            var result = await barService.DoesBarExistsByName(barName);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DoesBarExistsByName_Should_ReturnFalseForNonExistingBarName()
        {
            // Arrange
            var barName = "NonExistingBar"; // Assuming this bar name does not exist in the seeded data

            // Act
            var result = await barService.DoesBarExistsByName(barName);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AllBarsAsync_Should_ReturnAllActiveBars()
        {
            // Act
            var result = await barService.AllBarsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetBarForEditAsync_Should_ReturnCorrectBarForEdit()
        {
            // Arrange
            var barId = "1"; // Assuming this bar ID exists in the seeded data

            // Act
            var result = await barService.GetBarForEditAsync(barId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("London", result.CityName);
            // Add more assertions for other properties as needed.
        }


        [Test]
        public async Task AllBarsInACityAsync_Should_ReturnBarsInSpecifiedCity()
        {
            // Arrange
            var cityId = "7e980128-41f1-4351-b11f-2e9ac6d0cade";

            // Act
            var result = await barService.AllBarsInACityAsync(cityId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());


        }

        [Test]
        public async Task AllBarsInACityAsync_Should_ReturnEmptyListForNonExistingCity()
        {
            // Arrange
            var cityId = "999"; // Assuming this city ID does not exist in the seeded data

            // Act
            var result = await barService.AllBarsInACityAsync(cityId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count()); // Assuming there are no bars in the non-existing city
        }

        [Test]
        public async Task DetailsAsync_Should_ReturnBarDetailsForValidIds()
        {
            // Arrange
            var cityId = "7e980128-41f1-4351-b11f-2e9ac6d0cade"; // Assuming this city ID exists in the seeded data
            var barId = "1"; // Assuming this bar ID exists in the seeded data

            // Act
            var result = await barService.DetailsAsync(cityId, barId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("The Pub", result.Name); // Assuming the name of the bar with ID 1 is "The Pub"
                                                     // Add more assertions for other properties as needed.
        }

        [Test]
        public async Task DetailsAsync_Should_ThrowExceptionForInvalidBarId()
        {
            // Arrange
            var cityId = "7e980128-41f1-4351-b11f-2e9ac6d0cade"; // Assuming this city ID exists in the seeded data
            var barId = "999"; // Assuming this bar ID does not exist in the seeded data

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await barService.DetailsAsync(cityId, barId);
            }, "Bar not found");
        }






        [Test]
        public async Task EditBarByIdAndFormModelAsync_Should_ThrowExceptionForInvalidCityId()
        {
            // Arrange
            var barId = "1"; // Assuming this bar ID exists in the seeded data
            var model = new BarFormModel
            {
                Name = "Edited Bar",
                CityName = "Invalid City", // Assuming this city name does not exist in the seeded data
                Address = "456 Edited Street",
                Description = "An edited bar in New York",
                ImageFile = null // Assuming there is no image for this test case
            };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await barService.EditBarByIdAndFormModelAsync(int.Parse(barId), model);
            }, "Invalid city Id");
        }

        [Test]
        public async Task EditBarByIdAndFormModelAsync_Should_ThrowExceptionForInvalidBarId()
        {
            // Arrange
            var barId = "999"; // Assuming this bar ID does not exist in the seeded data
            var model = new BarFormModel
            {
                Name = "Edited Bar",
                CityName = "New York",
                Address = "456 Edited Street",
                Description = "An edited bar in New York",
                ImageFile = null // Assuming there is no image for this test case
            };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await barService.EditBarByIdAndFormModelAsync(int.Parse(barId), model);
            }, "Bar not found");
        }


        [Test]
        public async Task DeleteReviewByIdAsync_Should_ThrowExceptionForInvalidReviewId()
        {
            // Arrange
            var reviewId = "999";

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await barService.DeleteReviewByIdAsync(reviewId);
            }, "Review not found");
        }





        [Test]
        public async Task GetBarForDeleteAsync_Should_ReturnCorrectBarForDelete()
        {
            // Arrange
            var barId = "1"; // Assuming this bar ID exists in the seeded data

            // Act
            var result = await barService.GetBarForDeleteAsync(barId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(barId, result.Id);
            Assert.AreEqual("The Pub", result.Name); // Assuming the name of the bar with ID 1 is "The Pub"
            Assert.AreEqual("London", result.City); // Assuming the city of the bar with ID 1 is "London"
                                                    // Add more assertions for other properties as needed.
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}