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
        public int TargetId {  get; set; }
        public int ScoreId { get; set; }
        public int UserId { get; set; }
        public string DifficultyLevel { get; set; }
        public int TargetSpeed { get; set; }
        public int MaxTargets { get; set; }
        public int GameDuration { get; set; }
        
        public void createScore()
        {
            Score score = null;
            try
            {
                score = new Score
                {
                    ScoreId = this.ScoreId,
                    UserId = this.UserId,
                    // GameConfigId = this.GameId,
                    ScoreValue = 0,
                    TimeStamp = DateTime.Now,
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error creating score object: {e.Message}");
            }
        }
        public Target createTarget()
        {
            Target target = null;
            try
            {
                target = new Target
                {   
                    //creating a target with random data
                    TargetId = this.TargetId,
                    Shape = "Circle",
                    Size = 10,
                    Speed = 3,
                };
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error creating target object: {e.Message}");
            }
            return target;
        }

        public GameService createGame(GameConfig gameConfig)
        {
            GameService gameService = null;
            try
            {
                gameService = new GameService
                {
                    // assign gameProperties from gameConfig

                    //GameId = gameConfig.GameConfigId,
                    UserId = gameConfig.UserId,
                    DifficultyLevel = gameConfig.DifficultyLevel,
                    TargetSpeed = gameConfig.TargetSpeed,
                    MaxTargets = gameConfig.MaxTargets,
                    GameDuration = gameConfig.GameDuration,

                    //initialize Id for target and score objects
                    TargetId = 0,
                    ScoreId = 0
                };
                //creating one target with Id of 0
                createTarget();
                createScore(); 
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while creating a game: {e.Message}");
            }
            return gameService;
        }
    }
}
