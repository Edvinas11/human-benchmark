using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HumanBenchmark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumanBenchmarkController : ControllerBase
    {
        private readonly ILogger<HumanBenchmarkController> _logger;

        public HumanBenchmarkController(ILogger<HumanBenchmarkController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetHumanBenchmark")]
        public string Get()
        {
            return "labas";
        }
    }
}
