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
        private readonly FileService _fileService;
        private readonly ILogger<GameConfigController> _logger;

        public GameConfigController(FileService fileService, ILogger<GameConfigController> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }

        // POST api/gameconfig/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadGameConfig([FromBody] GameConfig gameConfig)
        {
            if (gameConfig == null)
            {
                return BadRequest("Invalid game configuration data.");
            }

            try
            {
                // Save the game config to the database
                await _fileService.SaveGameConfigAsync(gameConfig);

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
