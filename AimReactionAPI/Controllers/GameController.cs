using AimReactionAPI.Data;
using AimReactionAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AimReactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GameController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<Game>>> GetAllGames()
        {
            return await _context.Games
                .Include(g => g.Targets)  // Include related targets
                .ToListAsync();
        }

        [HttpGet("{id}")]  
        public async Task<ActionResult<Game>> GetGameById(int id)
        {
            Game? game = await _context.Games
                .Include(g => g.Targets)  // Include related targets
                .FirstOrDefaultAsync(g => g.GameId == id);

            if (game == null)
            {
                return NotFound("Game not found");
            }

            return game;
        }

        [HttpPost] 
        public async Task<ActionResult<Game>> AddGame(Game game)
        {
            // Add the new game
            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGameById), new { id = game.GameId }, game);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            Game? game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound("Game not found");
            }

            // Remove the game
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
