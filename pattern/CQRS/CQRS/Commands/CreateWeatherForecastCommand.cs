using MediatR;

namespace CQRS.Commands;

public class CreateWeatherForecastCommand : IRequest<WeatherForecast>
{
    public string? Summary { get; set; }
}