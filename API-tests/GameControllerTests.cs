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
using Newtonsoft.Json.Linq;
using System.Security.AccessControl;



namespace API.Tests.Unit
{

	[TestFixture]
	public class GameControllerTests
	{
		private AppDbContext _context;
		private GameController _controller;
        private ICollection<Target> targets;
        private ICollection<Score> scores;
		[SetUp]
		public void Setup()
		{
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

			_context = new AppDbContext(options);
			_controller = new GameController(_context);
            targets = new List<Target>();
            scores = new List<Score>();
        }

        [Test]
        public async Task GetGameById_GameIdNotFound_ReturnsNotFoundRequest()
        {
            // no games in the database, should not find game with Id of 0
            var result = await _controller.GetGameById(0);
            
            Assert.IsInstanceOf<ActionResult<GameDto>>(result);
            var notFoundResult = result.Result as NotFoundObjectResult;

            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual("Game not found", notFoundResult.Value);
        }

        [Test]
        public async Task GetGameById_GameIdFound_ReturnsOkRequest()
        {
            var existingGame = new Game
            {
                GameId = 1,
                GameName = "Test",
                GameDescription = "Test",
                DifficultyLevel = "Difficulty",
                TargetSpeed = 1,
                MaxTargets = 1,
                GameDuration = 1,
                GameType = GameType.MovingTargets,
                Targets = targets,
                Scores = scores
            };
            await _context.Games.AddAsync(existingGame);
            await _context.SaveChangesAsync();

            var result = await _controller.GetGameById(1);
            Assert.IsInstanceOf<ActionResult<GameDto>>(result);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var gameDto = okResult.Value as GameDto;
            Assert.IsNotNull(gameDto);

            Assert.AreEqual(existingGame.GameId, gameDto.GameId);
        }

        [Test]
        public async Task DeleteGame_GameIdNotFound_ReturnsNotFoundRequest()
        {
            //no game with such id, should return NotFound
            var result = await _controller.DeleteGame(2);
            Assert.IsInstanceOf<IActionResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);

            Assert.AreEqual("Game not found", notFoundResult.Value);
        }

        [Test]
        public async Task DeleteGame_GameIdFound_ReturnsNoContent()
        {
            var existingGame = new Game
            {
                GameId = 2,
                GameName = "Test",
                GameDescription = "Test",
                DifficultyLevel = "Difficulty",
                TargetSpeed = 1,
                MaxTargets = 1,
                GameDuration = 1,
                GameType = GameType.MovingTargets,
                Targets = targets,
                Scores = scores
            };
            await _context.Games.AddAsync(existingGame);
            await _context.SaveChangesAsync();
            var result = await _controller.DeleteGame(2);

            Assert.IsInstanceOf<IActionResult>(result);
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);

            Assert.AreEqual(204, noContentResult.StatusCode);
        }

        [Test]
        public async Task AddScore_UserIsNull_ReturnsNotFoundRequest()
        {
            // no such userId, should be null user
            var result = await _controller.AddScore(0, 10, DateTime.Parse("11-13-2024"), 1, GameType.MovingTargets);
            Assert.IsInstanceOf<IActionResult> (result); 
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);

            Assert.AreEqual("User not found", notFoundResult.Value);
        }
        [Test]
        public async Task AddScore_UserFound_ReturnsOkRequest()
        {
            var existingUser = new User
            {
                UserId = 1,
                Name = "test",
                Email = "test@example.com",
                PasswordHash = "hash"
            };

            await _context.AddAsync(existingUser);
            await _context.SaveChangesAsync();

            var result = await _controller.AddScore(1, 10, DateTime.Parse("11-13-2024"), 1, GameType.MovingTargets);

            Assert.IsInstanceOf<IActionResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var scoreResult = okResult.Value as Score;
            Assert.IsNotNull(scoreResult);

            //check if correct amount of score is added
            Assert.AreEqual(10, scoreResult.Value);
            Assert.AreEqual(1, scoreResult.UserId);
        }

        [TearDown]
        //cleanup database
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
