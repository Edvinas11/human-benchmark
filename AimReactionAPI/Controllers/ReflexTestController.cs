using AimReactionAPI.Models;
using AimReactionAPI.Data; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


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

        session.EndTime = DateTime.Now;
        await _context.SaveChangesAsync();

        var duration = session.GetDuration();

        return Ok(duration);
    }

    [HttpPost("recordScore")]
    public async Task<ActionResult<Score>> RecordReactionTime(int userId, int reactionTimeInMilliseconds)
    {
        var score = new Score
        {
            UserId = userId,
            ReactionTimeInMilliseconds = reactionTimeInMilliseconds,
            DateAchieved = DateTime.Now
        };

        _context.Scores.Add(score);
        await _context.SaveChangesAsync();

        return Ok(score);
    }
}
