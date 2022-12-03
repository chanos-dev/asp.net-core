using MediatR;

namespace CQRS.Queries;

public class GetAllWeatherForecastQuery : IRequest<List<WeatherForecast>>
{
    
}