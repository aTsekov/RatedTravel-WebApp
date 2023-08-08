using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RatedTravel.App.Web.Controllers;
using RatedTravel.Core.Interfaces;
using RatedTravel.Web.ViewModels.Bar;
using RatedTravel.Data.DataModels;
using System.Security.Claims;
using RatedTravel.Web.ViewModels.Bar;


namespace RatedTravel.Tests.Integration
{
    [TestFixture]
    public class BarControllerIntegrationTests
    {
        private BarController _controller;
        private Mock<IEmployeeService> _employeeServiceMock;
        private Mock<ICityService> _cityServiceMock;
        private Mock<IBarService> _barServiceMock;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;

        [SetUp]
        public void SetUp()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _cityServiceMock = new Mock<ICityService>();
            _barServiceMock = new Mock<IBarService>();
            _userManagerMock = MockUserManager<ApplicationUser>();

            _controller = new BarController(
                _employeeServiceMock.Object,
                _cityServiceMock.Object,
                _barServiceMock.Object,
                _userManagerMock.Object
            );
        }

        [Test]
        public async Task AddBar_Post_ValidData_RedirectsToIndex()
        {
            // Arrange
            var model = new BarFormModel { Name = "Test Bar", CityName = "Test City" };
            var user = new ApplicationUser();
            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _cityServiceMock.Setup(cs => cs.DoesCityExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _barServiceMock.Setup(bs => bs.DoesBarExistsByName(It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _controller.AddBar(model) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Home", result.ControllerName);
        }

        [Test]
        public async Task AddBar_Post_ValidModel_RedirectsToHomeIndex()
        {
            // Arrange
            var user = new ApplicationUser { Id = Guid.NewGuid() };
            var model = new BarFormModel { Name = "Test Bar", CityName = "Test City" };
            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _cityServiceMock.Setup(cs => cs.DoesCityExistsAsync(model.CityName)).ReturnsAsync(true);
            _barServiceMock.Setup(bs => bs.DoesBarExistsByName(model.Name)).ReturnsAsync(false);
           

            // Act
            var result = await _controller.AddBar(model) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Home", result.ControllerName);
        }

       

        private Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            return new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        }
    }
}
