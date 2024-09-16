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
        public async Task<IActionResult> UploadGameConfig(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var tempFilePath = Path.GetTempFileName();

            try
            {
                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var gameConfig = await _fileService.ParseTextFileAsync(tempFilePath);

                if (gameConfig == null)
                {
                    return BadRequest("Failed to parse the game configuration.");
                }

                return Ok(gameConfig);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            finally
            {
                if (System.IO.File.Exists(tempFilePath))
                    System.IO.File.Delete(tempFilePath);
            }
        }
    }
}
