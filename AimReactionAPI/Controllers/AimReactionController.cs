using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AimReactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AimReactionController : ControllerBase
    {
        private readonly ILogger<AimReactionController> _logger;

        public AimReactionController(ILogger<AimReactionController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAimReaction")]
        public string Get()
        {
            return "Healthy";
        }
    }
}
