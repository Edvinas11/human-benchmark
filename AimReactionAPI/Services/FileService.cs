using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AimReactionAPI.Data;
using AimReactionAPI.Models;

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

        // This method will save the GameConfig directly to the database
        public async Task SaveGameConfigAsync(GameConfig gameConfig)
        {
            try
            {
                _context.GameConfigs.Add(gameConfig);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while saving game configuration to the database.");
                throw;
            }
        }
    }
}
