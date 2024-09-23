using AimReactionAPI.Models;
using AimReactionAPI.Services;
using System;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

//From FileService.cs we get a gameConfig object. 
// gameConfig object contains: GameConfigId, userId, DifficultyLevel, etc.

namespace AimReactionAPI.Services
{
    public class GameService
    {
        public List<GameService> _gameServices = new List<GameService>();
        public int GameId { get; set; }
        public int TargetId {  get; set; }
        public int ScoreId { get; set; }
        public int UserId { get; set; }
        public string DifficultyLevel { get; set; }
        public int TargetSpeed { get; set; }
        public int MaxTargets { get; set; }
        public int GameDuration { get; set; }
        public GameType GameType { get; set; }

        public GameService CreateGameService(GameConfig gameConfig)
        {
            GameService gameService = null;
            try
            {
                gameService = new GameService
                {
                    GameId = gameConfig.GameConfigId,
                    UserId = gameConfig.UserId,
                    DifficultyLevel = gameConfig.DifficultyLevel,
                    TargetSpeed = gameConfig.TargetSpeed,
                    MaxTargets = gameConfig.MaxTargets,
                    GameDuration = gameConfig.GameDuration,
                    GameType = gameConfig.GameType
                };
                //add gameService object to the list of GameServices
                _gameServices.Add(gameService);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while creating a game: {e.Message}");
            }
            return gameService;
        }

        public List<GameService> GetAllGames()
        {
            return _gameServices;
        }

        public void CreateScore()
        {
            Score score = null;
            try
            {
                score = new Score
                {
                    ScoreId = ScoreId,
                    UserId = UserId,
                    Value = 0,
                    Timestamp = DateTime.Now,
                    GameType = GameType,
                };
            }
            catch (Exception e)
            {
               Console.WriteLine($"Error creating score object: {e.Message}");
            }
        }

        // save GameService in json format
        public void SaveGame(GameService gameService)
        {
            //probably needs to be saved to the database?
            try
            {
                string json = JsonSerializer.Serialize(gameService, new JsonSerializerOptions
                {
                    WriteIndented = true 
                });

                Console.WriteLine("Game Service Data in JSON format:");
                Console.WriteLine(json);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while saving game data: {e.Message}");
            }
        }
    }
}
