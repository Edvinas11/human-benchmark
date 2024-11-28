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
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace API_tests.AimReactionAPI.Test.Integration
{
    [TestFixture]
    public class TargetControllerTests
    {
        private AppDbContext _context;
        private TargetController _controller;
        private ILogger<TargetController> _logger;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;
            _context = new AppDbContext(options);

            SeedDatabase();
            _logger = new LoggerFactory().CreateLogger<TargetController>();
            _controller = new TargetController(_context, _logger);
        }

        [Test]
        public async Task GetTargetById_ShouldReturnTarget_WhenTargetExists()
        {
            var result = await _controller.GetTargetById(1);

            Assert.IsInstanceOf<ActionResult<Target>>(result);

            var target = result.Value;
            Assert.IsNotNull(target);
            Assert.AreEqual(1, target.TargetId);
        }

        [Test]
        public async Task GetTargetsBySpeed_ShouldReturnFilteredTargets()
        {

            var result = await _controller.GetTargetsBySpeed(3);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var targets = okResult.Value as IEnumerable<Target>;

            Assert.IsNotNull(targets);
            Assert.AreEqual(2, targets.Count()); // two targets with the same count
        }

        [Test]
        public async Task AddTarget_ShouldAddTarget()
        {
            var newTarget = new Target { GameId = 1, Size = 3, Speed = 4, X = 20, Y = 20 };

            var result = await _controller.AddTarget(newTarget);

            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);

            var addedTarget = createdResult.Value as Target;
            Assert.IsNotNull(addedTarget);
            Assert.AreEqual(newTarget.Size, addedTarget.Size);
        }

        [Test]
        public async Task DeleteTarget_ShouldRemoveTarget_WhenTargetExists()
        {

            var result = await _controller.DeleteTarget(1, 1);

            Assert.IsInstanceOf<NoContentResult>(result);

            var target = await _context.Targets.FindAsync(1);
            Assert.IsNull(target);
        }


        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        public void SeedDatabase()
        {
            var testGame = new Game
            {
                GameId = 1,
                GameName = "Test Game",
                GameDescription = "Test",
                DifficultyLevel = "Medium",
                TargetSpeed = 5,
                MaxTargets = 10,
                GameDuration = 30,
                GameType = GameType.MovingTargets,
                Targets = new List<Target>
                {
                    new Target { GameId = 1, TargetId = 1, Size = 3, Speed = 5, X = 10, Y = 15 },
                    new Target { GameId = 1, TargetId = 2, Size = 4, Speed = 7, X = 20, Y = 25 }
                },
                Scores = new List<Score>
                {
                    new Score { UserId = 1, Value = 100, GameType = GameType.MovingTargets, DateAchieved = DateTime.Now },
                    new Score { UserId = 2, Value = 85, GameType = GameType.MovingTargets, DateAchieved = DateTime.Now }
                }
            };
            _context.Add(testGame);
            _context.SaveChangesAsync();
        }
    }
}
