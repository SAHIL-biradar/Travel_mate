using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using TravelMate.Controllers;
using TravelMate.Services;
using UserDll.Models;
using UserWebApi.Models;

namespace UserWebApiTest
{
    [TestClass]
    public class UserControllerTests
    {
        private Mock<IUserDataService> _mockUserDataService;
        private UserController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockUserDataService = new Mock<IUserDataService>();
            _controller = new UserController(_mockUserDataService.Object);
        }

        [TestMethod]
        public async Task Get_ValidUser_ReturnsOkResult_WithUser()
        {
            // Arrange
            var userLogin = new UserLogin { UserName = "test@example.com", Password = "password" };
            var user = new User { UserId = 1, Email = "test@example.com" };
            _mockUserDataService.Setup(service => service.GetUser(userLogin)).ReturnsAsync(user);

            // Act
            var result = await _controller.Get(userLogin) as ActionResult<User>;

            // Assert
            Assert.IsNotNull(result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(user, okResult.Value);
        }

        [TestMethod]
        public async Task Get_InvalidUser_ReturnsNotFound()
        {
            // Arrange
            var userLogin = new UserLogin { UserName = "invalid@example.com", Password = "password" };
            _mockUserDataService.Setup(service => service.GetUser(userLogin)).ThrowsAsync(new Exception("User not found"));

            // Act
            var result = await _controller.Get(userLogin) as ActionResult<User>;

            // Assert
            Assert.IsNotNull(result);
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual("User not found", notFoundResult.Value);
        }


        [TestMethod]
        public async Task Post_ValidUser_ReturnsOkResult()
        {
            // Arrange
            var user = new User { UserId = 1, Email = "new@example.com" };

            // Act
            var result = await _controller.Post(user);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task Post_InvalidUser_ReturnsBadRequest()
        {
            // Arrange
            var user = new User { UserId = 1, Email = "new@example.com" };
            _mockUserDataService.Setup(service => service.RegisterUser(user)).ThrowsAsync(new Exception("Registration failed"));

            // Act
            var result = await _controller.Post(user) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Registration failed", result.Value);
        }

        [TestMethod]
        public async Task Put_ValidUser_ReturnsOkResult()
        {
            // Arrange
            var user = new User { UserId = 1, Email = "update@example.com" };

            // Act
            var result = await _controller.Put(user.UserId, user);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task Put_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var user = new User { UserId = 1, Email = "update@example.com" };
            var mismatchedId = 2;

            // Act
            var result = await _controller.Put(mismatchedId, user) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("User ID mismatch.", result.Value);
        }

        [TestMethod]
        public async Task Put_InvalidUser_ReturnsBadRequest()
        {
            // Arrange
            var user = new User { UserId = 1, Email = "update@example.com" };
            _mockUserDataService.Setup(service => service.UpdateUser(user)).ThrowsAsync(new Exception("Update failed"));

            // Act
            var result = await _controller.Put(user.UserId, user) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Update failed", result.Value);
        }
    }
}
