using Microsoft.EntityFrameworkCore;
using RatedTravel.Core.Interfaces;
using RatedTravel.Core.Services;
using RatedTravel.Data;
using RatedTravel.Web.ViewModels.Employee;
using static RatedTravel.Tests.DatabaseSeeder;

namespace RatedTravel.Tests
{
    public class EmployeeServiceTests
    {
        private DbContextOptions<RatedTravelDbContext> dbOptions;
        private RatedTravelDbContext dbContext;

        private IEmployeeService employeeService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<RatedTravelDbContext>()
                .UseInMemoryDatabase("RatedTravelInMemoryDb" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RatedTravelDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.employeeService = new EmployeeService(this.dbContext);
        }


        [Test]
        public async Task EmployeeExistsByIdAsyncShouldReturnTrueWhenExists()
        {
            string existingEmployee = MyEmployee.Id.ToString();

            bool result = await this.employeeService.EmployeeExistsByIdAsync(existingEmployee);

            Assert.IsTrue(result);
        }


        [Test]
        public async Task EmployeeExistsByIdAsyncShouldReturnFalseWhenEmlpDoesNotExists()
        {
            string existingEmployee = EmployeeUser.Id.ToString();

            bool result = await this.employeeService.EmployeeExistsByIdAsync(existingEmployee);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task EmployeeExistsByPhoneNumberAsync_ShouldReturnTrue_WhenPhoneNumberExists()
        {
            // Arrange
            string phoneNumber = "1234567890";

            // Act
            bool result = await employeeService.EmployeeExistsByPhoneNumberAsync(phoneNumber);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task EmployeeExistsByPhoneNumberAsync_ShouldReturnFalse_WhenPhoneNumberDoesNotExist()
        {
            // Arrange
            string phoneNumber = "9999999999"; // A phone number that is not in the test data

            // Act
            bool result = await employeeService.EmployeeExistsByPhoneNumberAsync(phoneNumber);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task EmployeeExistsByNameAsync_ShouldReturnTrue_WhenNameExists()
        {
            // Arrange
            string name = "Antoni Tsekov";

            // Act
            bool result = await employeeService.EmployeeExistsByNameAsync(MyEmployee.FullName);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task EmployeeExistsByNameAsync_ShouldReturnFalse_WhenNameDoesNotExist()
        {
            // Arrange
            string name = "John Wick"; // A name that is not in the test data

            // Act
            bool result = await employeeService.EmployeeExistsByNameAsync(name);

            // Assert
            Assert.IsFalse(result);
        }




        [Test]
        public async Task EmployeeExistsByUserIdAsync_ShouldReturnFalse_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();

            // Act
            var result = await employeeService.EmployeeExistsByUserIdAsync(userId);

            // Assert
            Assert.IsFalse(result);
        }



    }
}