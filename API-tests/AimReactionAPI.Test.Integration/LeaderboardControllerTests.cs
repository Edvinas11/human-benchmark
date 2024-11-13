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


namespace API_tests.AimReactionAPI.Test.Integration
{
    
    [TestFixture]
    public class LeaderboardControllerTests
    {
        private AppDbContext _context;
        private LeaderboardController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new AppDbContext(options);

            SeedDatabase();

            _controller = new LeaderboardController(_context); 
        }

        [Test]
        public async Task GetAllUsersScores_ShouldReturnAllUsersWithScores()
        {

            var result = await _controller.GetAllUsersScores();
            var okResult = result.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<IEnumerable<object>>(okResult.Value);
            var usersScores = okResult.Value as IEnumerable<dynamic>;
            Assert.IsNotNull(usersScores);

            Assert.AreEqual(2, usersScores.Count());
        }

        [Test]
        public async Task GetTopScores_ShouldReturnTopNScores()
        {
            var result = await _controller.GetTopScores(1, GameType.MovingTargets);
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<IEnumerable<object>>(okResult.Value);

            var topScores = okResult.Value as IEnumerable<dynamic>;
            Assert.IsNotNull(topScores);
            
            //check if correct amount is being returned
            Assert.AreEqual(1, topScores.Count());

            var firstScore = topScores.FirstOrDefault();
            Assert.IsNotNull(firstScore);

            //check if correct user id is being returned

            var userIdProperty = firstScore.GetType().GetProperty("UserId");
            Assert.IsNotNull(userIdProperty);

            var userIdValue = userIdProperty.GetValue(firstScore);
            Assert.AreEqual(1, userIdValue);

            //check if gametype being returned is correct

            var gameTypeProperty = firstScore.GetType().GetProperty("GameType"); 
            Assert.IsNotNull(gameTypeProperty);

            var gameTypeValue = gameTypeProperty.GetValue(firstScore);
            Assert.AreEqual(GameType.MovingTargets, gameTypeValue);
        }

        [Test]
        public async Task GetUserHighScore_ShouldReturnUserHighScore()
        {
            var result = await _controller.GetUserHighScore(2);
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<object>(okResult.Value);

            var highScore = okResult.Value as object;
            Assert.IsNotNull(highScore);

            //check if correct user if is being returned

            var userIdProperty = highScore.GetType().GetProperty("UserId");
            Assert.IsNotNull(userIdProperty);

            Assert.AreEqual(2, userIdProperty.GetValue(highScore));

            //check if correct score value is being returned

            var userHighScoreProperty = highScore.GetType().GetProperty("HighScore");
            Assert.IsNotNull(userHighScoreProperty);

            Assert.AreEqual(80, userHighScoreProperty.GetValue(highScore));
        }   



        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private void SeedDatabase()
        {
            _context.Users.AddRange(
                new User {UserId = 1, Name = "test", Email = "test@example.com", PasswordHash = "hash"},
                new User { UserId = 2, Name = "test1", Email = "test1@example.com", PasswordHash = "hash1" }
            );

            _context.Scores.AddRange(
                new Score { UserId = 1, Value = 100, GameType = GameType.MovingTargets, DateAchieved = System.DateTime.Now },
                new Score { UserId = 2, Value = 80, GameType = GameType.ReflexTest, DateAchieved = System.DateTime.Now }
            );

            _context.SaveChangesAsync();
        }
    }
}
