using Microsoft.AspNetCore.Mvc;

namespace WeatherForecast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OneController : ControllerBase
    { 
        private readonly ILogger<OneController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public OneController(ILogger<OneController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Get()
        {            
            using HttpClient client = _httpClientFactory.CreateClient();
            using HttpRequestMessage request = new(HttpMethod.Get, new Uri("https://localhost:7039/api/BookStore"));
            using HttpResponseMessage response = client.Send(request);

            string result = response.Content.ReadAsStringAsync().Result;
            _logger.LogInformation(result);
            return Ok(result);
        }
    }
}