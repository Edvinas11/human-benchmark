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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTarget(int id)
    {
        Target? target = await _context.Targets.FindAsync(id);

        if (target == null)
        {
            return NotFound("Target not found");
        }

        // Remove the target
        _context.Targets.Remove(target);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
