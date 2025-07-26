using AdminMvcUi.Controllers;
using AdminMvcUi.Models;
using AdminMvcUi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminMvcUiTest.Controllers
{
    [TestClass]
    public class AdminUiControllerTests
    {
        private Mock<IAdminService> _mockAdminService;
        private AdminUiController _controller;

        [TestInitialize]
        public void SetUp()
        {
            _mockAdminService = new Mock<IAdminService>();
            _controller = new AdminUiController(_mockAdminService.Object);
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult_WithListOfUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { UserId = 1, Username = "ayush", Email = "ayush@fnf.com" },
                new User { UserId = 5, Username = "aditya", Email = "aditya@fnf.com" }
            };
            _mockAdminService.Setup(s => s.GetAll()).ReturnsAsync(users);

            // Act
            var result = await _controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual(users, viewResult.Model);
        }

        [TestMethod]
        public async Task Delete_ReturnsOkResult_WhenUserIsDeleted()
        {
            // Arrange
            var userId = 1;
            _mockAdminService.Setup(s => s.DeleteUser(userId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(userId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));

            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        // False Test Cases

        [TestMethod]
        public async Task Index_ReturnsViewResult_WithIncorrectListOfUsers()
        {
            // Arrange
            var expectedUsers = new List<User>
            {
                new User { UserId = 1, Username = "ayush", Email = "ayush@fnf.com" },
                new User { UserId = 5, Username = "aditya", Email = "aditya@fnf.com" }
            };

            var incorrectUsers = new List<User>
            {
                new User { UserId = 2, Username = "wrongUser", Email = "wrong@fnf.com" },
                new User { UserId = 6, Username = "anotherWrongUser", Email = "anotherWrong@fnf.com" }
            };

            _mockAdminService.Setup(s => s.GetAll()).ReturnsAsync(incorrectUsers);

            // Act
            var result = await _controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            CollectionAssert.AreNotEqual(expectedUsers, (List<User>)viewResult.Model);
        }

        [TestMethod]
        public async Task Delete_ReturnsNotFoundResult_WhenUserIsNotDeleted()
        {
            // Arrange
            var userId = 1;
            _mockAdminService.Setup(s => s.DeleteUser(userId)).Throws(new KeyNotFoundException());

            // Act
            var result = await _controller.Delete(userId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [TestMethod]
        public async Task Index_ReturnsNotFoundResult_WhenNoUsersFound()
        {
            // Arrange
            _mockAdminService.Setup(s => s.GetAll()).ReturnsAsync(new List<User>());

            // Act
            var result = await _controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }
    }
}
