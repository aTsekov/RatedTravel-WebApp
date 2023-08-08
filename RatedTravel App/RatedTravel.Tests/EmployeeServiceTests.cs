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


        [Test]
        public async Task EmployeeExistsByIdAndHasMoreThanThreeCreatedItemsAsync_ShouldReturnTrueWhenEligible()
        {
            // Arrange
            string userId = ToBecomeEmployee.Id.ToString();

            // Act
            bool result = await employeeService.EmployeeExistsByIdAndHasMoreThanThreeCreatedItemsAsync(userId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task EmployeeExistsByIdAndHasMoreThanThreeCreatedItemsAsync_ShouldReturnFalseWhenIneligible()
        {
            // Arrange
            string userId = MyEmployee.UserId.ToString();

            // Act
            bool result = await employeeService.EmployeeExistsByIdAndHasMoreThanThreeCreatedItemsAsync(userId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task EmployeeByUserIdAsync_ShouldReturnEmployeeWhenExists()
        {
            // Arrange
            string userId = MyEmployee.UserId.ToString();

            // Act
            var employee = await employeeService.EmployeeByUserIdAsync(userId);

            // Assert
            Assert.NotNull(employee);
            Assert.AreEqual(MyEmployee.FullName, employee.FullName);
            Assert.AreEqual(MyEmployee.PhoneNumber, employee.PhoneNumber);
        }

        [Test]
        public async Task EmployeeByUserIdAsync_ShouldReturnNullWhenEmployeeDoesNotExist()
        {
            // Arrange
            string userId = Guid.NewGuid().ToString();

            // Act
            var employee = await employeeService.EmployeeByUserIdAsync(userId);

            // Assert
            Assert.Null(employee);
        }

        [Test]
        public async Task EmployeeExistsByPhoneNumberAsync_ShouldReturnTrueWhenPhoneNumberExists()
        {
            // Arrange
            string existingPhoneNumber = MyEmployee.PhoneNumber;

            // Act
            bool result = await employeeService.EmployeeExistsByPhoneNumberAsync(existingPhoneNumber);

            // Assert
            Assert.IsTrue(result);
        }


        [Test]
        public async Task EmployeeExistsByPhoneNumberAsync_ShouldReturnFalseWhenPhoneNumberDoesNotExist()
        {
            // Arrange
            string nonExistingPhoneNumber = "5555555555"; // A phone number that does not exist in the test data

            // Act
            bool result = await employeeService.EmployeeExistsByPhoneNumberAsync(nonExistingPhoneNumber);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task EmployeeExistsByNameAsync_ShouldReturnTrueWhenNameExists()
        {
            // Arrange
            string existingName = MyEmployee.FullName;

            // Act
            bool result = await employeeService.EmployeeExistsByNameAsync(existingName);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task EmployeeExistsByNameAsync_ShouldReturnFalseWhenNameDoesNotExist()
        {
            // Arrange
            string nonExistingName = "John Smith"; // A name that does not exist in the test data

            // Act
            bool result = await employeeService.EmployeeExistsByNameAsync(nonExistingName);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task EmployeeExistsByUserIdAsync_ShouldReturnTrueWhenEmployeeExists()
        {
            // Arrange
            string existingUserId = MyEmployee.UserId.ToString();

            // Act
            bool result = await employeeService.EmployeeExistsByUserIdAsync(existingUserId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task EmployeeExistsByUserIdAsync_ShouldReturnFalseWhenEmployeeDoesNotExist()
        {
            // Arrange
            string nonExistingUserId = Guid.NewGuid().ToString();

            // Act
            bool result = await employeeService.EmployeeExistsByUserIdAsync(nonExistingUserId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task EmployeeExistsByIdAndHasMoreThanThreeCreatedItemsAsync_ShouldReturnFalseWhenUserCreatedFewerThan3Items()
        {
            // Arrange
            string userId = NonEligibleForEmployee.Id.ToString();
            // Assuming the user created only 2 items

            // Act
            bool result = await employeeService.EmployeeExistsByIdAndHasMoreThanThreeCreatedItemsAsync(userId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task EmployeeExistsByIdAndHasMoreThanThreeCreatedItemsAsync_ShouldReturnTrueWhenUserCreatedExactly3Items()
        {
            // Arrange
            string userId = ToBecomeEmployee.Id.ToString();
            // Assuming the user created exactly 3 items

            // Act
            bool result = await employeeService.EmployeeExistsByIdAndHasMoreThanThreeCreatedItemsAsync(userId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task EmployeeExistsByIdAsync_ShouldReturnFalseWhenEmployeeDoesNotExist()
        {
            // Arrange
            string nonExistingUserId = Guid.NewGuid().ToString();

            // Act
            bool result = await employeeService.EmployeeExistsByIdAsync(nonExistingUserId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task EmployeeExistsByPhoneNumberAsync_ShouldReturnFalseWithInvalidPhoneNumber()
        {
            // Arrange
            string invalidPhoneNumber = null; // Invalid data, phone number cannot be null

            // Act
            bool result = await employeeService.EmployeeExistsByPhoneNumberAsync(invalidPhoneNumber);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task EmployeeExistsByNameAsync_ShouldReturnFalseWithInvalidName()
        {
            // Arrange
            string invalidName = null; // Invalid data, name cannot be null

            // Act
            bool result = await employeeService.EmployeeExistsByNameAsync(invalidName);

            // Assert
            Assert.IsFalse(result);
        }





    }
}