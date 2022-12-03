using CQRS.Commands;
using CQRS.Repository;
using MediatR;

namespace CQRS.Handlers;

public class CreateWeatherForecastHandler : IRequestHandler<CreateWeatherForecastCommand, WeatherForecast>
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;
    private readonly ILogger<CreateWeatherForecastHandler> _logger;


    public CreateWeatherForecastHandler(IWeatherForecastRepository weatherForecastRepository, ILogger<CreateWeatherForecastHandler> logger)
    {
        _weatherForecastRepository = weatherForecastRepository;
        _logger = logger;
    }
    
    public async Task<WeatherForecast> Handle(CreateWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        var weather = await this._weatherForecastRepository.CreateWeatherForecast(request.Summary);
        this._logger.LogInformation($"Created weatherforecast: {weather.Id}, {weather.Summary}");
        return weather;
    }
}