using System;
using System.IO;
using AimReactionAPI.Models;
using AimReactionAPI.Data;

/*
TEST .TXT FILE EXAMPLE
1, Hard, 5, 10, 120
*/

namespace AimReactionAPI.Services
{
    public class FileService
    {
        private readonly ILogger<FileService> _logger;
        private readonly AppDbContext _context;

        public FileService(ILogger<FileService> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<GameConfig> ParseTextFileAsync(string filePath)
        {
            GameConfig gameConfig = null;

            try
            {
                using var reader = new StreamReader(filePath);
                string line = await reader.ReadLineAsync();

                if (line != null)
                {
                    var values = line.Split(',');

                    Enum.TryParse(values[5].Trim(), out GameType gameType);

                    gameConfig = new GameConfig
                    {
                        UserId = int.Parse(values[0].Trim()),
                        DifficultyLevel = values[1].Trim(),
                        TargetSpeed = int.Parse(values[2].Trim()),
                        MaxTargets = int.Parse(values[3].Trim()),
                        GameDuration = int.Parse(values[4].Trim()),
                        GameType = gameType
                    };

                    _context.GameConfigs.Add(gameConfig);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while parsing text file at {FilePath}", filePath);
            }

            return gameConfig;
        }
    }
}