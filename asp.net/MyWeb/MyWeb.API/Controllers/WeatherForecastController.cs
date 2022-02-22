using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWeb.API.Controllers
{
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

        //Read
        [HttpGet]
        public IEnumerable<WeatherForecast> Get(int num)
        {
            var rng = new Random();
            return Enumerable.Range(1, num).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //Insert
        [HttpPost]
        public WeatherForecast Post([FromForm]int addDay)
        {
            return new WeatherForecast() { Date = DateTime.Now.AddDays(addDay) };
        }

        //Update
        [HttpPut]
        public IActionResult Put()
        {
            return Ok();
        }

        //Delete
        [HttpDelete]
        public IActionResult Delete(int key)
        {
            return BadRequest(new { message = "잘못된 요청입니다", hello = "name!!", world = new { one = 1, two = 2, three = "3" } }) ;
        }
    }
}
