using Microsoft.AspNetCore.Mvc;
using WebAPI.Http;

namespace WeatherForecast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FourController : ControllerBase
    { 
        private readonly ILogger<FourController> _logger;
        private readonly IBookStoreClient _client;

        public FourController(ILogger<FourController> logger, IBookStoreClient client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            _logger.LogInformation("refit!!");
            string result = await _client.GetBookStoreAsync();
            _logger.LogInformation(result);
            return Ok(result);
        }
    }
}