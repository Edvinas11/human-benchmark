using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;
using AimReactionAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AimReactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameConfigController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<GameConfigController> _logger;

        public GameConfigController(AppDbContext context, ILogger<GameConfigController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // POST api/gameconfig/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadGameConfig([FromBody] GameConfigDto gameConfigDto)
        {
            if (gameConfigDto == null)
            {
                return BadRequest("Invalid game configuration data.");
            }

            var gameConfig = new GameConfig
            {
                Name = gameConfigDto.Name,
                Description = gameConfigDto.Description,
                DifficultyLevel = gameConfigDto.DifficultyLevel,
                TargetSpeed = gameConfigDto.TargetSpeed,
                MaxTargets = gameConfigDto.MaxTargets,
                GameDuration = gameConfigDto.GameDuration,
                GameType = gameConfigDto.GameType
            };

            try
            {
                _context.GameConfigs.Add(gameConfig);
                await _context.SaveChangesAsync();

                return Ok(gameConfig);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while saving game configuration.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
