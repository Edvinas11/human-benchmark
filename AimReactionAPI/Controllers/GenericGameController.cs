using AimReactionAPI.Models;
using AimReactionAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AimReactionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenericGameController : ControllerBase
{
    private readonly GameSessionHandler<GameType> _gameSessionHandler;

    public GenericGameController(GameSessionHandler<GameType> gameSessionHandler)
    {
        _gameSessionHandler = gameSessionHandler;
    }

    [HttpPost("{userId}/start/{gameType}")]
    public async Task<IActionResult> StartGameSession(int userId, GameType gameType)
    {
        var session = await _gameSessionHandler.StartSessionAsync(userId, gameType);
        return Ok(session);
    }

    [HttpPost("end/{sessionId}")]
    public async Task<IActionResult> EndGameSession(int sessionId)
    {
        var duration = await _gameSessionHandler.EndSessionAsync(sessionId);
        return Ok(duration);
    }


    [HttpGet("active")]
    public IActionResult GetActiveSessionCount()
    {
        var activeCount = _gameSessionHandler.GetActiveSessionCount();
        return Ok(new { activeSessions = activeCount });
    }
}
