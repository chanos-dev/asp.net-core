using Microsoft.AspNetCore.Mvc;

namespace WeatherForecast.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TwoController : ControllerBase
    { 
        private readonly ILogger<TwoController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public TwoController(ILogger<TwoController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookStore() => await GetHttpAsync("BookStore", "api/BookStore");

        [HttpGet]
        public async Task<IActionResult> GetWeatherForecast() => await GetHttpAsync("WeatherForecast", "WeatherForecast");

        private async Task<IActionResult> GetHttpAsync(string name, string endpoint)
        {
            using HttpClient client = _httpClientFactory.CreateClient(name);
            using HttpResponseMessage response = await client.GetAsync(endpoint);

            string result = response.Content.ReadAsStringAsync().Result;
            _logger.LogInformation(result);
            return Ok(result);
        }
    }
}