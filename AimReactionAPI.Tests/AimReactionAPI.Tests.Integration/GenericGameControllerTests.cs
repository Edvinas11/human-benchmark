using System.Runtime.CompilerServices;
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
using Microsoft.Extensions.DependencyInjection;

namespace AimReactionAPI.Tests.Integration
{
    [TestFixture]
    public class GenericGameControllerTests
    {
        private AppDbContext _context;
        private GenericGameController _controller;
        private GameSessionHandler<GameType> _gameSessionHandler;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;
            _context = new AppDbContext(options);

            SeedDatabase();

            var mockScopeFactory = new Mock<IServiceScopeFactory>();
            var mockScope = new Mock<IServiceScope>();
            var mockServiceProvider = new Mock<IServiceProvider>();

            mockScopeFactory.Setup(x => x.CreateScope()).Returns(mockScope.Object);
            mockScope.Setup(x => x.ServiceProvider).Returns(mockServiceProvider.Object);
            mockServiceProvider.Setup(x => x.GetService(typeof(AppDbContext))).Returns(_context);

            _gameSessionHandler = new GameSessionHandler<GameType>(_context, mockScopeFactory.Object);
            _controller = new GenericGameController(_gameSessionHandler, _context);
        }



        [Test]
        public async Task StartGameSession_ShouldReturnSession()
        {
            var result = await _controller.StartGameSession(1, GameType.ReflexTest);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var gameSessionResult = okResult.Value as GameSession;
            Assert.IsNotNull(gameSessionResult);

            Assert.AreEqual(1, gameSessionResult.UserId);
            Assert.AreEqual(GameType.ReflexTest, gameSessionResult.GameType);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private void SeedDatabase()
        {
            _context.Add(new User { UserId = 1, Name = "test", Email = "test@example.com", PasswordHash = "hash" });

            _context.SaveChangesAsync();
        }
    }
}
