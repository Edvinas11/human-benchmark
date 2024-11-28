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
using Microsoft.OpenApi.Validations;

namespace API_tests.AimReactionAPITests.Unit
{
    [TestFixture]
    public class GameConfigControllerTests
    {
        private AppDbContext _context;
        private GameService _gameService;
        private Mock<ILogger<GameService>> _gameServiceLoggerMock;
        private TargetService _targetService;
        private ILogger<GameConfigController> _logger;
        private GameConfigController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _gameServiceLoggerMock = new Mock<ILogger<GameService>>();
            _targetService = new TargetService(_context);
            _gameService = new GameService(_context, _gameServiceLoggerMock.Object, _targetService);
            _controller = new GameConfigController(_context, _logger, _gameService);
        }

        [Test]
        public async Task UploadGameConfig_ValidDto_ReturnsOkRequest()
        {
            var gameConfigDto = new GameConfigDto
            {
                Name = "Test Game",
                Description = "Description",
                DifficultyLevel = "Difficulty",
                TargetSpeed = 1,
                MaxTargets = 1,
                GameDuration = 1,
                GameType = GameType.MovingTargets
            };

            var result = await _controller.UploadGameConfig(gameConfigDto);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var createdGame = okResult.Value as Game;
            Assert.IsNotNull(createdGame);
            Assert.AreEqual("Test Game", createdGame.GameName);
        }

        [Test]
        public async Task UploadGameConfig_gameConfigDtoIsNull_ReturnsBadRequest()
        {
            var result = await _controller.UploadGameConfig(null);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("Invalid game configuration data.", badRequestResult.Value);
        }

        [Test]
        public async Task UploadGameConfig_gameIsNull_ReturnsServerError()
        {
            var gameConfigDto = new GameConfigDto
            {
                Name = "Test Game",
                Description = "Description",
                DifficultyLevel = "Difficulty",
                TargetSpeed = 1,
                MaxTargets = 1,
                GameDuration = 1,
                GameType = GameType.MovingTargets
            };

            _gameService = new GameService(_context, _gameServiceLoggerMock.Object, _targetService);
            _controller = new GameConfigController(_context, _logger, new GameServiceStub(null));

            var result = await _controller.UploadGameConfig(gameConfigDto);

            Assert.IsInstanceOf<ObjectResult>(result);
            var serverErrorResult = result as ObjectResult;
            Assert.AreEqual(500, serverErrorResult.StatusCode);
            Assert.AreEqual("Game creation failed.", serverErrorResult.Value);
        }

        [TearDown]
        //cleanup database
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }

    //simulating _gameService.CreateGameFromAsync failure 
    public class GameServiceStub : GameService
    {
        private readonly Game _returnValue;

        public GameServiceStub(Game returnValue) : base(null)
        {
            _returnValue = returnValue;
        }

        public override Task<Game?> CreateGameFromAsync(GameConfig gameConfig)
        {
            return Task.FromResult(_returnValue);
        }
    }
}