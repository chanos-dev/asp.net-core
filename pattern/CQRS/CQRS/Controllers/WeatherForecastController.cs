using CQRS.Commands;
using CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{ 
    private readonly IMediator _mediator;

    public WeatherForecastController(IMediator mediator)
    { 
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetAllWeatherForecastQuery();
        var result = await this._mediator.Send(query);
        return Ok(result);
    } 
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetWeatherForecastByIdQuery(id);
        var result = await this._mediator.Send(query);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateWeatherForecastCommand command)
    {
        var result = await this._mediator.Send(command);
        return CreatedAtAction("GetById", new { id = result.Id }, result);
    }
}

