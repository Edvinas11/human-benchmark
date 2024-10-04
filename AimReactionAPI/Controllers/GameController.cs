using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AimReactionAPI.Controllers;

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
    public async Task<ActionResult<IEnumerable<GameDescription>>> GetAllGames()
    {
        var games = await _context.Games
            .Select(g => new GameDescription(g.GameName, g.GameDescription))
            .ToListAsync();

        return Ok(games);
    }

    [HttpGet("{id}")]  
    public async Task<ActionResult<GameDto>> GetGameById(int id)
    {
        Game? game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == id);
        
        if (game == null)
        {
            return NotFound("Game not found");
        }

        object boxedGameId = game.GameId;
        int unboxedGameId = (int)boxedGameId;

        Console.WriteLine($"Boxed GameId: {boxedGameId}, Unboxed GameId: {unboxedGameId}");

        var gameDto = new GameDto
        {
            GameId = unboxedGameId,
            GameName = game.GameName,
            GameDescription = game.GameDescription,
            DifficultyLevel = game.DifficultyLevel,
            TargetSpeed = game.TargetSpeed,
            MaxTargets = game.MaxTargets,
            GameDuration = game.GameDuration,
            GameType = game.GameType
        };

        return Ok(gameDto);
    }

    [HttpGet("{id}/targets")]
    public async Task<ActionResult<IEnumerable<Target>>> GetGameTargets(int id)
    {
        Game? game = await _context.Games
            .Include(g => g.Targets)
            .FirstOrDefaultAsync(g => g.GameId == id);

        if (game == null)
        {
            return NotFound("Game not found");
        }

        return Ok(game.Targets);
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

    [HttpPost("{userId}/addscore")]
    public async Task<IActionResult> AddScore(int userId, int value, DateTime dateAchieved, int gameId, GameType gameType)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
        {
            return NotFound("User not found");
        }

        var newScore = new Score
        {
            Value = value,
            DateAchieved = dateAchieved,
            GameId = gameId,
            GameType = gameType,
            UserId = userId
        };

        _context.Scores.Add(newScore);
        await _context.SaveChangesAsync();

        return Ok(newScore);
    }

}
