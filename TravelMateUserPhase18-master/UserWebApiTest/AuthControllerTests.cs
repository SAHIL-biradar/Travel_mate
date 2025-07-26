using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using TravelMate.Services;
using UserWebApi.Controllers;
using UserWebApi.Models;
using UserDll.Models;
using Microsoft.Extensions.Configuration;

namespace UserWebApiTest
{
    [TestClass]
    public class AuthControllerTests
    {
        private Mock<IUserDataService> _mockUserDataService;
        private AuthController _controller;
        private const string PrivateKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        [TestInitialize]
        public void SetUp()
        {
            _mockUserDataService = new Mock<IUserDataService>();
            var configuration = new Mock<IConfiguration>();
            _controller = new AuthController(configuration.Object, _mockUserDataService.Object);
        }

        [TestMethod]
        public async Task Login_ValidUser_ReturnsOkResult_WithToken()
        {
            // Arrange
            var userLogin = new UserLogin { UserName = "testUser", Password = "password" };
            var user = new User { UserId = 1, Username = "testUser", PasswordHash = "password" };
            _mockUserDataService.Setup(service => service.GetUser(userLogin)).ReturnsAsync(user);

            // Act
            var result = await _controller.Login(userLogin) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as dynamic;
            Assert.IsNotNull(response.token);
            Assert.IsTrue(response.token.ToString().StartsWith("eyJ"));
        }

        [TestMethod]
        public async Task Login_InvalidUser_ReturnsUnauthorized()
        {
            // Arrange
            var userLogin = new UserLogin { UserName = "invalidUser", Password = "password" };
            _mockUserDataService.Setup(service => service.GetUser(userLogin)).ReturnsAsync((User)null);

            // Act
            var result = await _controller.Login(userLogin) as UnauthorizedResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Login_ValidUser_WithIncorrectPassword_ReturnsUnauthorized()
        {
            // Arrange
            var userLogin = new UserLogin { UserName = "testUser", Password = "wrongPassword" };
            var user = new User { UserId = 1, Username = "testUser", PasswordHash = "password" }; // Assuming password hashing is correct

            // Setup the mock service to return the user with a correct username but incorrect password
            _mockUserDataService.Setup(service => service.GetUser(userLogin)).ReturnsAsync(user);

            // Act
            var result = await _controller.Login(userLogin) as UnauthorizedResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
