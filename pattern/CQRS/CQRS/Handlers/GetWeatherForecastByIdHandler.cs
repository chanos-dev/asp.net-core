using CQRS.Queries;
using CQRS.Repository;
using MediatR;

namespace CQRS.Handlers;

public class GetWeatherForecastByIdHandler : IRequestHandler<GetWeatherForecastByIdQuery, WeatherForecast>
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public GetWeatherForecastByIdHandler(IWeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository;
    }
    
    public async Task<WeatherForecast?> Handle(GetWeatherForecastByIdQuery request, CancellationToken cancellationToken)
    {
        return await this._weatherForecastRepository.GetByIdAsync(request.Id);
    }
}