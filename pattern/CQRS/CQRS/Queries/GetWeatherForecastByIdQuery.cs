using MediatR;

namespace CQRS.Queries;

public class GetWeatherForecastByIdQuery : IRequest<WeatherForecast>
{
    public int Id { get; set; }

    public GetWeatherForecastByIdQuery(int id)
    {
        this.Id = id;
    }
}