namespace CQRS.Repository;

public interface IWeatherForecastRepository
{
    Task<IEnumerable<WeatherForecast>> GetAllAsync();
    Task<WeatherForecast?> GetByIdAsync(int id);

    Task<WeatherForecast> CreateWeatherForecast(string summary);
}