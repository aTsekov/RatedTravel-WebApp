using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Interfaces;
using RatedTravel.Core.Interfaces;
using RatedTravel.Core.Services;
using RatedTravel.Data;
using RatedTravel.Web.ViewModels.Bar;
using static RatedTravel.Tests.DatabaseSeeder;

namespace RatedTravel.Tests
{
    public class UserServiceTests
    {

        private DbContextOptions<RatedTravelDbContext> dbOptions;
        private RatedTravelDbContext dbContext;

        private IUserService userService;

        [SetUp]
        public void Setup()
        {
            this.dbOptions = new DbContextOptionsBuilder<RatedTravelDbContext>()
                .UseInMemoryDatabase("RatedTravelInMemoryDb" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RatedTravelDbContext(this.dbOptions);
            

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.userService = new UserService(this.dbContext);
           
        }

        [Test]
        public async Task GetFullNameByEmailAsync_Should_ReturnFullNameForExistingEmail()
        {
            // Arrange
            var userEmail = "antk@abv.bg"; // Assuming this email exists in the seeded data

            // Act
            var fullName = await userService.GetFullNameByEmailAsync(userEmail);

            // Assert
            Assert.AreEqual("Antoni Tsekov", fullName);
        }

        [Test]
        public async Task GetFullNameByEmailAsync_Should_ReturnEmptyStringForNonExistingEmail()
        {
            // Arrange
            var userEmail = "nonexisting@example.com"; // Assuming this email does not exist in the seeded data

            // Act
            var fullName = await userService.GetFullNameByEmailAsync(userEmail);

            // Assert
            Assert.AreEqual(string.Empty, fullName);
        }


        [Test]
        public async Task GetFullNameByIdAsync_Should_ReturnEmptyStringForNonExistingUserId()
        {
            // Arrange
            var userId = "nonexistinguserid"; // Assuming this user ID does not exist in the seeded data

            // Act
            var fullName = await userService.GetFullNameByIdAsync(userId);

            // Assert
            Assert.AreEqual(string.Empty, fullName);
        }

        [Test]
        public async Task AllAsync_Should_ReturnAllUsersWithPhoneNumbers()
        {
            // Act
            var users = await userService.AllAsync();

            // Assert
            Assert.IsNotNull(users);
            Assert.IsNotEmpty(users);
            Assert.AreEqual(3, users.Count()); 
        }

       


        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }


}

