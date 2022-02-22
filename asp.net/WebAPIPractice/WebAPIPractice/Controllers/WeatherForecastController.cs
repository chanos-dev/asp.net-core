using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIPractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public string Get(int id)
        {
            return $"hello!! - {id}";
        }

        [HttpPost]
        [Produces("application/json")] // api response content-type
        public string Post([FromBody] WeatherForecast weather)
        {
            return weather.Date.ToString("yyyy-MM-dd");
        }

        [HttpPost("{name}")]
        [Produces("application/json")]
        public string Post(string name)
        {
            return name;
        }

        [HttpDelete]
        [Produces("text/plain", Type = typeof(WeatherForecast), Order = 1)]      
        [ApiExplorerSettings(GroupName = "other")]
        public IActionResult Delete(int id)
        {            
            return File(System.IO.File.Open(@"C:\Users\Chanos\Downloads\in.txt", System.IO.FileMode.Open), "text/plain");
        }

        [HttpPut]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Put(int id)
        {
            return BadRequest();
        }
    }
}
