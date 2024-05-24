namespace Heartbeat.Api.Controllers
{
    using Heartbeat.Lib;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> logger;

        public StatusController(ILogger<StatusController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> PostHeartbeat([FromBody] StatusResource statusResource)
        {
            this.logger.LogInformation($"Received heartbeat: {JsonConvert.SerializeObject(statusResource)}");
            return new OkResult();
        }
    }
}