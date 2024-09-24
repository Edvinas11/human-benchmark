using AimReactionAPI.Models;
using AimReactionAPI.Data; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace AimReactionAPI.Controllers
{
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

       
        [HttpGet(Name = "StartReflexTest")]
        public IActionResult StartReflexTest()
        {
            
            return Ok("Reflex test started.");
        }

        
        [HttpPost(Name = "SubmitResult")]
        public IActionResult SubmitResult([FromBody] Score score)
        {
            if (score == null )
            {
                return BadRequest("Invalid score data.");
            }

            try
            {
                score.Timestamp = DateTime.UtcNow; 
                _context.Scores.Add(score);
                _context.SaveChanges();

                return Ok("Score submitted successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while submitting score data");
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
    }
}
