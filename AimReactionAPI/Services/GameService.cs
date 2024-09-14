using AimReactionAPI.Models;
using AimReactionAPI.Services;
using System.Reflection.Metadata.Ecma335;

//From FileService.cs we get a gameConfig object. 
// gameConfig object contains: GameConfigId, userId, DifficultyLevel, etc.

namespace AimReactionAPI.Services
{
    public class GameService
    {
        // Get/set staments iškelti į failą panašų į GameConfig.cs?
        public int GameId { get; set; }
        public int UserId { get; set; }
        public string DifficultyLevel { get; set; }
        public int TargetSpeed { get; set; }
        public int MaxTargets { get; set; }
        public int GameDuration { get; set; }
        public GameService createGame(GameConfig gameConfig)
        {
            GameService gameService = null;
            try
            {
                gameService = new GameService
                {
                    // assign gameProperties from gameConfig
                    GameId = gameConfig.GameConfigId,
                    UserId = gameConfig.UserId,
                    DifficultyLevel = gameConfig.DifficultyLevel,
                    TargetSpeed = gameConfig.TargetSpeed,
                    MaxTargets = gameConfig.MaxTargets,
                    GameDuration = gameConfig.GameDuration,
                }; 
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while creating a game: {e.Message}");
            }
            return gameService;
        }
    }
}
