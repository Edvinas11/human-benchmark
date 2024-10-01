using AimReactionAPI.Models;
using AimReactionAPI.Data; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AimReactionAPI.DTOs;


namespace AimReactionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReflexTestController : ControllerBase
{
    private readonly AppDbContext _context; 
    private readonly ILogger<ReflexTestController> _logger; 

    public ReflexTestController(AppDbContext context, ILogger<ReflexTestController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost("start")]
    public async Task<ActionResult<GameSession>> StartReflexTestSession(int userId)
    {
        var session = new GameSession
        {
            UserId = userId,
            StartTime = DateTime.UtcNow,
            GameType = GameType.ReflexTest
        };

        _context.GameSessions.Add(session);
        await _context.SaveChangesAsync();

        return Ok(session);
    }

    [HttpPost("end/{sessionId}")]
    public async Task<ActionResult<TimeSpan>> EndReflexTestSession(int sessionId)
    {
        var session = await _context.GameSessions.FindAsync(sessionId);

        if (session == null)
        {
            return NotFound("Session not found.");
        }

        session.EndTime = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        var duration = session.GetDuration();

        return Ok(duration);
    }

    [HttpPost("recordScore")]
    public async Task<ActionResult<Score>> RecordReactionTime([FromBody] RecordScoreDto recordScoreDto)
    {
        var user = await _context.Users.FindAsync(recordScoreDto.UserId);
        
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var score = new Score
        {
            UserId = user.UserId,
            GameId = 1,
            GameType = GameType.ReflexTest,
            ReactionTime = recordScoreDto.ReactionTimeInMilliseconds,
            DateAchieved = DateTime.UtcNow,
            Value = 100
        };

        _context.Scores.Add(score);
        await _context.SaveChangesAsync();

        return Ok(score);
    }
}
