using Microsoft.AspNetCore.Mvc;
using Swagger.Filter;
using Swagger.Model;
using Swashbuckle.AspNetCore.Annotations;

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

    [HttpGet]
    [Route("GetTwo")]
    [SwaggerOperation("두번쨰 조회 방법", "단일 날씨 조회")]
    [SwaggerResponse(StatusCodes.Status200OK, "조회 성공", typeof(WeatherForecast))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "조회 실패", typeof(Error))]
    public ActionResult GetTwo()
    {
        if (DateTime.Now.Second % 2 == 0)
        {
            return BadRequest(new Error
            {
                Code =StatusCodes.Status400BadRequest,
                Message = "Error!!",
            });
        }
        
        return Ok(new WeatherForecast
        {
            Date = DateTime.Now,
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });
    }
}