using AimReactionAPI.Data;
using AimReactionAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AimReactionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeaderboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("top-users/{gameType}")]
        public async Task<ActionResult<IEnumerable<object>>> GetTopUsersForGameType(GameType gameType)
        {
            
            var topUsers = await _context.UserHighScores
                .Where(uhs => uhs.GameType == gameType)
                .OrderByDescending(uhs => uhs.HighScore)
                .Take(10) // Max Users in leaderboard
                .Select(uhs => new
                {
                    uhs.UserId,
                    uhs.HighScore,
                    User = _context.Users.FirstOrDefault(u => u.UserId == uhs.UserId) 
                })
                .ToListAsync();

            
            var result = topUsers.Select(uhs => new 
            {
                UserId = uhs.UserId,
                UserName = uhs.User?.Name, 
                UserEmail = uhs.User?.Email,
                HighScore = uhs.HighScore
            });

            return Ok(result);
        }

    }
}
