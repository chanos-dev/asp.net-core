using CQRS.Queries;
using CQRS.Repository;
using MediatR;

namespace CQRS.Handlers;

public class GetAllWeatherForecastHandler : IRequestHandler<GetAllWeatherForecastQuery, List<WeatherForecast>>
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public GetAllWeatherForecastHandler(IWeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository;
    }
    
    public async Task<List<WeatherForecast>> Handle(GetAllWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        var weathers = await this._weatherForecastRepository.GetAllAsync();
        return weathers.ToList();
    }
}