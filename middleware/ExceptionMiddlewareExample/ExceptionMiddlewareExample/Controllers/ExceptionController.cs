using Microsoft.AspNetCore.Mvc;

namespace ExceptionMiddlewareExample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ExceptionController : ControllerBase
    { 
        private readonly ILogger<ExceptionController> _logger;

        public ExceptionController(ILogger<ExceptionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult GetTwo() => Ok();
    }
}