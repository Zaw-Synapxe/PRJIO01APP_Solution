using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogTestController : ControllerBase
    {
        private readonly ILogger<LogTestController> _logger;

        public LogTestController(ILogger<LogTestController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogDebug("This is debug log.");
            _logger.LogInformation("Info Log at: {time}", DateTimeOffset.Now);
            _logger.LogInformation(new { id = 1, name = "John" }.ToString());
            _logger.LogError("This is an error log.");




            return Ok("Test success!");
        }


    }
}
