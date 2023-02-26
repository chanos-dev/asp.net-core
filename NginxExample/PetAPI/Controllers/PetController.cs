using Microsoft.AspNetCore.Mvc;

namespace PetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetController : ControllerBase
    {
        private static readonly Pet[] Pets = new[]
        {
            new Pet()
            {
                Id = 1,
                Age = 2,
                Name = "Milk",
                Type = "Dog",                
            },
            new Pet()
            {
                Id = 2,
                Age = 1,
                Name = "Blue",
                Type = "Cat",
            }
        };

        private readonly ILogger<PetController> _logger;

        public PetController(ILogger<PetController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<Pet> Get()
        {
            return Pets;
        }
    }
}