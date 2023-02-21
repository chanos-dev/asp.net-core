using Microsoft.AspNetCore.Mvc;

namespace WeatherForecast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThreeController : ControllerBase
    { 
        private readonly ILogger<ThreeController> _logger;
        private readonly HttpClient _httpClient;

        public ThreeController(ILogger<ThreeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            using HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7039/api/BookStore");
            string result = await response.Content.ReadAsStringAsync();
            _logger.LogInformation(result);
            return Ok(result);
        }
    }
}