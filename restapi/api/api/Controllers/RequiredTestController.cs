using api.Model;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RequiredTestController : ControllerBase
{ 
    private readonly ILogger<PersonController> _logger;

    public RequiredTestController(ILogger<PersonController> logger) => _logger = logger;

    [HttpGet]
    public ActionResult Get([FromBody] Options options)
    {
        return Ok(options);
    }
}