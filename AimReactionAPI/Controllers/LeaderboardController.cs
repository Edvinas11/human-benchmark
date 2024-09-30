using AimReactionAPI.Data;
using AimReactionAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AimReactionAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaderboardController : ControllerBase
{
    private readonly AppDbContext _context;

    public LeaderboardController(AppDbContext context)
    {
        _context = context;
    }

    // Endpoint to get all users and their scores
    [HttpGet("all-users")]
    public async Task<ActionResult<IEnumerable<object>>> GetAllUsersScores()
    {
        var allUsersScores = await _context.Scores
            .OrderByDescending(s => s.Value)
            .Select(s => new
            {
                s.UserId,
                s.Value,
                User = _context.Users.FirstOrDefault(u => u.UserId == s.UserId)
            })
            .ToListAsync();

        var result = allUsersScores.Select(s => new 
        {
            UserId = s.UserId,
            UserName = s.User?.Name, 
            UserEmail = s.User?.Email,
            Score = s.Value
        });

        return Ok(result);
    }

    // Endpoint to get top N scores
    [HttpGet("top-scores/{topCount}")]
    public async Task<ActionResult<IEnumerable<object>>> GetTopScores(int topCount)
    {
        var topScores = await _context.Scores
            .OrderByDescending(s => s.Value) 
            .Take(topCount)
            .Select(s => new
            {
                s.UserId,
                s.Value,
                User = _context.Users.FirstOrDefault(u => u.UserId == s.UserId)
            })
            .ToListAsync();

        var result = topScores.Select(s => new 
        {
            UserId = s.UserId,
            UserName = s.User?.Name, 
            UserEmail = s.User?.Email,
            Score = s.Value
        });

        return Ok(result);
    }

    [HttpGet("User-Top-Score/{userId}")]
    public async Task<ActionResult<object>> GetUserHighScore(int userId) {
        var userHighScore = await _context.Scores
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.Value)
            .FirstOrDefaultAsync();

        if (userHighScore == null) {
            return NotFound("No scores found for the user.");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == userId);

        var result = new {
            UserId = user.UserId,
            UserName = user.Name,
            UserEmail = user.Email,
            HighScore = userHighScore.Value,
            DateAchieved = userHighScore.DateAchieved,
            gameId = userHighScore.GameId,
            GameType = userHighScore.GameType
        };
        return Ok(result);
    }
}
