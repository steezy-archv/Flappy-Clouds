using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Flappy_Clouds.Controllers;
using Flappy_Clouds.Entities;
using Flappy_Clouds.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace FlappyClouds.Tests
{
    [TestClass]
    public class AccountControllerTests
    {
        private Mock<IPasswordHasher<User>> _passwordHasherMock;
        private FlappyCloudsContext _dbContext;
        private AccountController _controller;

        [TestInitialize]
        public void Setup()
        {
            // Mocking DbContext using InMemory database
            var options = new DbContextOptionsBuilder<FlappyCloudsContext>()
                .UseInMemoryDatabase(databaseName: "FlappyCloudsTestDb")
                .Options;

            _dbContext = new FlappyCloudsContext(options);

            // Mocking PasswordHasher
            _passwordHasherMock = new Mock<IPasswordHasher<User>>();

            // Initializing Controller
            _controller = new AccountController(_dbContext, _passwordHasherMock.Object);
        }

        [TestMethod]
        public async Task Registration_Should_Add_User_To_Database()
        {
            // Arrange
            var model = new RegistrationViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Password = "SecurePass123",
                ConfirmPassword = "SecurePass123",
                Address = "123 Main St",
                PhoneNumber = "1234567890"
            };

            _passwordHasherMock
                .Setup(h => h.HashPassword(It.IsAny<User>(), model.Password))
                .Returns("hashedpassword123");

            // Act
            var result = _controller.Registration(model) as RedirectToActionResult;

            // Assert
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == model.Email);
            Assert.IsNotNull(user);
            Assert.AreEqual("John", user.FirstName);
            Assert.AreEqual("hashedpassword123", user.PasswordHash);
            Assert.AreEqual("Registration", result.ActionName);
        }

        [TestMethod]
        public async Task Login_With_Correct_Credentials_Should_Redirect_To_Home()
        {
            // Arrange
            var user = new User
            {
                Email = "test@example.com",
                PasswordHash = "hashedpassword123",
                FirstName = "Test",
                LastName = "User",
                Role = "User"
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            var model = new LoginViewModel
            {
                Email = "test@example.com",
                Password = "CorrectPassword"
            };

            _passwordHasherMock
                .Setup(h => h.VerifyHashedPassword(It.IsAny<User>(), user.PasswordHash, model.Password))
                .Returns(PasswordVerificationResult.Success);

            // Act
            var result = await _controller.Login(model) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Home", result.ControllerName);
        }

        [TestMethod]
        public async Task Login_With_Invalid_Credentials_Should_Return_Error()
        {
            // Arrange
            var model = new LoginViewModel
            {
                Email = "wrong@example.com",
                Password = "WrongPassword"
            };

            // Act
            var result = await _controller.Login(model) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ViewData.ModelState.ContainsKey(""));
        }

        [TestMethod]
        public async Task Logout_Should_SignOut_User()
        {
            // Act
            var result = await _controller.Logout() as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Home", result.ControllerName);
        }
    }
}
