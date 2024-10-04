using AimReactionAPI.Models;
using AimReactionAPI.Services;
using AimReactionAPI.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AimReactionAPI.Data;
using Microsoft.EntityFrameworkCore;


namespace AimReactionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TargetController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<TargetController> _logger;

    public TargetController(AppDbContext context, ILogger<TargetController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Target>>> GetAllTargets()
    {
        return await _context.Targets.ToListAsync();  // Retrieve all targets
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Target>> GetTargetById(int id)
    {
        Target? target = await _context.Targets.FindAsync(id);

        if (target == null)
        {
            return NotFound("Target not found");
        }

        return target;
    }

    [HttpGet("filterBySpeed/{speedThreshold}")]
    public async Task<ActionResult<IEnumerable<Target>>> GetTargetsBySpeed(int speedThreshold)
    {
        var targets = await _context.Targets.ToListAsync();

        var filteredTargets = targets.FilterTargetsBySpeed(speedThreshold).ToList();

        return Ok(filteredTargets);
    }

    [HttpPost]
    public async Task<ActionResult<Target>> AddTarget(Target target)
    {
        // Validate and add the new target
        _context.Targets.Add(target);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTargetById), new { id = target.TargetId }, target);
    }

    [HttpDelete("{gameId}/targets/{id}")]
    public async Task<IActionResult> DeleteTarget(int gameId, int id)
    {
        var game = await _context.Games
        .Include(g => g.Targets)
        .FirstOrDefaultAsync(g => g.GameId == gameId);

        if (game == null)
        {
            return NotFound("Game not found");
        }

        Target? targetToDelete = null;

        foreach (var target in game)
        {
            if (target.TargetId == id)
            {
                targetToDelete = target;
                break;
            }
        }

        if (targetToDelete == null)
        {
            return NotFound("Target not found");
        }

        _context.Targets.Remove(targetToDelete);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
