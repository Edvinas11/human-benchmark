using AimReactionAPI.Models;
using AimReactionAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AimReactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TargetController : ControllerBase
    {
        private readonly TargetService _targetService;
        private readonly ILogger<TargetController> _logger;

        public TargetController(TargetService targetService, ILogger<TargetController> logger)
        {
            _targetService = targetService;
            _logger = logger;
        }

        //GET return all targets
        [HttpGet(Name = "GetAllTargets")]
        public ActionResult<List<Target>> GetAllTargets()
        {
            var targets = _targetService.GetAllTargets();
            if (targets == null || targets.Count == 0)
            {
                return NotFound("No targets found.");
            }
            return Ok(targets);
        }

        //POST add target
        [HttpPost(Name = "AddTarget")]
        public async Task<IActionResult> AddTarget([FromBody] Target target)
        {
            if (target == null) 
            {
                return BadRequest("Invalid target data");
            }

            try
            {
                _targetService.AddTarget(target);
                return Ok(target);
            }
            catch (Exception e) 
            {
                _logger.LogError(e, "Error while saving target data");
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

    }
}
