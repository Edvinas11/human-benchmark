using AimReactionAPI.Models;
using AimReactionAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AimReactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameServiceController : ControllerBase
    {
        private readonly GameService _gameService;
        private readonly ILogger<GameServiceController> _logger;
        
        public GameServiceController(GameService gameService, ILogger<GameServiceController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }
        //GET return all games
        [HttpGet(Name = "GetAllGames")]
        public ActionResult<List<GameService>> GetAllGames()
        {
            var games = _gameService.GetAllGames();
            if (games == null || games.Count == 0)
            {
                return NotFound("No games found.");
            }
            return Ok(games);
        }
    }




}
