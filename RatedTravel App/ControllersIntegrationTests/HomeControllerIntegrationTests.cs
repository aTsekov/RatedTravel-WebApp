using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RatedTravel.App.Web.Controllers;
using RatedTravel.Common;
using RatedTravel.Core.Interfaces;
using RatedTravel.Web.ViewModels.Home;

namespace RatedTravel.Tests.Integration
{
    [TestFixture]
    public class HomeControllerIntegrationTests
    {
        private HomeController _controller;
        private Mock<ICityService> _cityServiceMock;

        [SetUp]
        public void Setup()
        {
            _cityServiceMock = new Mock<ICityService>();
            _controller = new HomeController(_cityServiceMock.Object);
        }


        [Test]
        public void Error_ReturnsError404View_ForNotFoundStatusCode()
        {
            // Arrange
            var statusCode = 404;

            // Act
            var result = _controller.Error(statusCode) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Error404", result.ViewName);
        }

        [Test]
        public async Task Error_ReturnsError404View_WhenStatusCodeIs404()
        {
            // Arrange
            const int statusCode = 404;

            // Act
            var result = _controller.Error(statusCode) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Error404", result.ViewName);
        }

        [Test]
        public async Task Error_ReturnsDefaultErrorView_WhenStatusCodeIsNot404()
        {
            // Arrange
            const int statusCode = 500; 

            // Act
            var result = _controller.Error(statusCode) as ViewResult;

            // Assert
            Assert.IsNull(result.ViewName); 
        }

    }
}
