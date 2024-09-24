using AimReactionAPI.Data;
using AimReactionAPI.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace AimReactionAPI.Services
{
    public class GameService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<GameService> _logger;
        private readonly TargetService _targetService;

        public GameService(AppDbContext context, ILogger<GameService> logger, TargetService targetService)
        {
            _context = context;
            _logger = logger;
            _targetService = targetService;
        }

        public async Task<Game> CreateGameFromAsync(GameConfig gameConfig)
        {
            try
            {
                var game = new Game
                {
                    GameConfigId = gameConfig.GameConfigId,
                    GameName = gameConfig.Name,
                    GameDescription = gameConfig.Description,
                    DifficultyLevel = gameConfig.DifficultyLevel,
                    TargetSpeed = gameConfig.TargetSpeed,
                    MaxTargets = gameConfig.MaxTargets,
                    GameDuration = gameConfig.GameDuration,
                    GameType = gameConfig.GameType,
                    Targets = _targetService.GenerateTargets(gameConfig.MaxTargets, gameConfig.TargetSpeed)
                };

                _context.Games.Add(game);
                await _context.SaveChangesAsync();

                return game;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a game from GameConfig.");
                return null;
            }
        }

        //// Updated score creation with record
        //public Score CreateScore()
        //{
        //    try
        //    {
        //        return new Score
        //        (
        //            ScoreId: new Random().Next(1, 1000), // Random ID generation for simplicity
        //            Value: 0, // Initial score value
        //            Timestamp: DateTime.Now, // Current timestamp
        //            GameType: this.GameType // Set GameType from GameService
        //        );
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine($"Error creating score object: {e.Message}");
        //        return null;
        //    }
        //}
    }
}
