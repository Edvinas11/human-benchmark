using System.Runtime.CompilerServices;
using NUnit;
using Moq;
using AimReactionAPI.Data;
using AimReactionAPI.Services;
using AimReactionAPI.Controllers;
using AimReactionAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using AimReactionAPI.Models;
using Microsoft.Extensions.DependencyInjection;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;


namespace API_tests
{
    [TestFixture]
    public class AuthControllerTests
    {
        private AppDbContext _context;
        private AuthService _authService;
        private AuthController _controller;
        private Mock<ILogger<GameService>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            //Use in-memory database for testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);

            _loggerMock = new Mock<ILogger<GameService>>();

            _authService= new AuthService(_context, _loggerMock.Object);

            // Instantiate the controller with the mocked dependencies
            _controller = new AuthController(_context, _authService);
        }

        [TearDown]
        //cleanup database
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }           

        //Test if Register returns a bad request if email already exists
        [Test]
        public async Task Register_EmailAlreadyRegistered_ReturnsBadRequest()
        {
            // Arrange: Add a user with the same email to the in-memory database
            var existingUser = new User { Email = "test@example.com" , Name = "test", PasswordHash = _authService.HashPassword("test")};
            await _context.Users.AddAsync(existingUser);
            await _context.SaveChangesAsync();

            var userDto = new UserDto { Email = "test@example.com", Name = "test"};

            // Act: Call the Register method
            var result = await _controller.Register(userDto);

            // Assert: Ensure the response is a BadRequest
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            Assert.AreEqual("Email is already registered", ((BadRequestObjectResult)result.Result).Value);
        }

        //Test if password is empty
        [Test]
        public async Task Register_PasswordIsNullOrEmpty_ReturnsBadRequest()
        {   
            //Arrage
            var existingUser = new User
            {
                Email = "test@example.com",
                Name = "test",
                PasswordHash = _authService.HashPassword("test")
            };
            await _context.Users.AddAsync(existingUser);
            await _context.SaveChangesAsync();

            var userDto = new UserDto { Email = "different@example.com", Name = "test", Password = "" };
            
            //Act 
            var result = await _controller.Register(userDto);

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            Assert.AreEqual("Password cannot be empty.", ((BadRequestObjectResult)result.Result).Value);
        }

        [Test]
        public async Task Register_NewUser_ReturnsUser()
        {
            // Arrange:
            var userDto = new UserDto
            {
                Email = "newuser@example.com",
                Name = "test",
                Password = "test"
            };

            // Act
            var result = await _controller.Register(userDto);

            // Assert that the result is a CreatedAtActionResult
            var createdAtResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtResult, "Expected a CreatedAtActionResult");

            // Assert that the action result contains a User object
            var createdUser = createdAtResult.Value as User;
            Assert.IsNotNull(createdUser, "Expected the result to contain a User object");

            // Assert that the returned User contains the same email and name as provided in the UserDto
            Assert.AreEqual(userDto.Email, createdUser.Email, "The created user's email does not match the input email");
            Assert.AreEqual(userDto.Name, createdUser.Name, "The created user's name does not match the input name");

            // Ensure the result is a valid ActionResult<User>
            Assert.IsInstanceOf<ActionResult<User>>(result);

            // Additional check: Ensure the correct routing in CreatedAtActionResult
            Assert.AreEqual("GetUserById", createdAtResult.ActionName, "Expected action name to be 'GetUserById'");
            Assert.IsTrue(createdAtResult.RouteValues.ContainsKey("id"), "Expected route values to contain 'id'");
        }

        [Test]
        public async Task Login_InvalidEmail_ReturnsUnauthorized()
        {
            var existingUser = new User
            {
                Email = "test@example.com",
                Name = "test",
                PasswordHash = _authService.HashPassword("test")
            };

            await _context.Users.AddAsync(existingUser);
            await _context.SaveChangesAsync();

            var loginDto = new LoginDto
            {
                Email = "different@example.com",
                Password = "test"
            };

            var result = await _controller.Login(loginDto);

            Assert.IsInstanceOf<UnauthorizedObjectResult>(result.Result);
            Assert.AreEqual("Invalid email or password", ((UnauthorizedObjectResult)result.Result).Value);
        }

        [Test]
        public async Task Login_InvalidPassword_ReturnsUnauthorized()
        {
            var existingUser = new User
            {
                Email = "test@example.com",
                Name = "test",
                PasswordHash = _authService.HashPassword("test")
            };

            await _context.Users.AddAsync(existingUser);
            await _context.SaveChangesAsync();

            var loginDto = new LoginDto
            {
                Email = "test@example.com",
                Password = "different"
            };

            var result = await _controller.Login(loginDto);
            Assert.IsInstanceOf<UnauthorizedObjectResult>(result.Result);
            Assert.AreEqual("Invalid email or password", ((UnauthorizedObjectResult)result.Result).Value);
        }

        [Test]
        public async Task Login_ReturnsUserId()
        {
            var existingUser = new User
            {
                Email = "test@example.com",
                Name = "test",
                PasswordHash = _authService.HashPassword("test"),
                UserId = 0
            };

            await _context.Users.AddAsync(existingUser);
            await _context.SaveChangesAsync();

            var loginDto = new LoginDto
            {
                Email = "test@example.com",
                Password = "test"
            };

            var result = await _controller.Login(loginDto);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            Assert.AreEqual(existingUser.UserId, okResult.Value);
        }
    }
}