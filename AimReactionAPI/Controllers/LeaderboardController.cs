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

        //[HttpGet("TopScores")]
        //public async Task<ActionResult<IEnumerable<Score>>> GetTopScores(int top = 10)
        //{
        //    var topScores = await _context.Scores
        //        .OrderByDescending(s => s.Value)  
        //        .Take(top)   // limit may be changed                     
        //        .ToListAsync();

        //    return Ok(topScores);
        //}
    }
}
