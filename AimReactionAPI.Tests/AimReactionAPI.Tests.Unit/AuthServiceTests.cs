namespace AimReactionAPI.Tests.Unit;
using NUnit;
using Moq;
using AimReactionAPI.Data;
using AimReactionAPI.Services;
using AimReactionAPI.Controllers;
using AimReactionAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AimReactionAPI.Models;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

[TestFixture]
    public class AuthServiceTests
    {
        private AuthService _service;
        private Mock<ILogger<GameService>> _mockLogger;
        private Mock<AppDbContext> _mockContext;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<GameService>>();

            //Needed to add options to mock AppDbConte
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            // Mock AppDbContext
            _mockContext = new Mock<AppDbContext>(options);
            _service = new AuthService(_mockContext.Object, _mockLogger.Object);

        }

        [Test]
        public void HashPassword_ShouldReturnCorrectHash()
        {
            // Arrange
            var password = "TestPassword";
            string expectedHash;
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                expectedHash = Convert.ToBase64String(hashedBytes);
            }

            // Act
            var result = _service.HashPassword(password);

            // Assert
            Assert.AreEqual(expectedHash, result);
        }

        [Test]
        public void VerifyPassword_ShouldReturnTrue_ForMatchingPasswords()
        {
            // Arrange
            var password = "TestPassword";
            var hashedPassword = _service.HashPassword(password);

            // Act
            var result = _service.VerifyPassword(password, hashedPassword);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void VerifyPassword_ShouldReturnFalse_ForNonMatchingPasswords()
        {
            // Arrange
            var password = "TestPassword";
            var wrongPassword = "WrongPassword";
            var hashedPassword = _service.HashPassword(password);

            // Act
            var result = _service.VerifyPassword(wrongPassword, hashedPassword);

            // Assert
            Assert.IsFalse(result);
        }
    }
