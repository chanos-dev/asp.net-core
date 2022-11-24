using Microsoft.AspNetCore.Mvc;
using Swagger.Filter;

namespace Swagger.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 날씨 조회 API
    /// </summary>
    /// <remarks>
    /// 예시
    ///
    ///     GET /WeatherForecast 
    ///
    /// </remarks>
    /// <response code="200">조회 성공</response>
    [HttpGet(Name = "GetWeatherForecast")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(WeatherForecast), 200)]
    [ApiKeyFilter]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}