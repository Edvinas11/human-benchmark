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
        private readonly GameService _gameService;

        public GameConfigController(AppDbContext context, ILogger<GameConfigController> logger, GameService gameService)
        {
            _context = context;
            _logger = logger;
            _gameService = gameService;
        }

        // POST api/gameconfig/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadGameConfig([FromBody] GameConfigDto gameConfigDto)
        {
            if (gameConfigDto == null)
            {
                return BadRequest("Invalid game configuration data.");
            }

            GameConfig? gameConfig = new GameConfig
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

                Game? game = await _gameService.CreateGameFromAsync(gameConfig);

                if (game == null)
                {
                    return StatusCode(500, "Game creation failed.");
                }

                return Ok(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while saving game configuration.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
