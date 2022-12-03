namespace CQRS.Repository;

public class WeatherForecastRepository : IWeatherForecastRepository
{
    public static List<WeatherForecast> MemoryStorage { get; set; }

    static WeatherForecastRepository()
    { 
        string[] summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        MemoryStorage = new List<WeatherForecast>(Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Id = index,
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        }));
    }
    public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
    {
        return await Task.FromResult(MemoryStorage);
    }

    public async Task<WeatherForecast?> GetByIdAsync(int id)
    {
        return await Task.FromResult(MemoryStorage.FirstOrDefault(d => d.Id == id));
    }

    public async Task<WeatherForecast> CreateWeatherForecast(string summary)
    {
        var idx = MemoryStorage.Count + 1;
        
        WeatherForecast weather = new()
        {
            Id = idx,
            Date = DateTime.Now.AddDays(idx),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summary,
        };

        MemoryStorage.Add(weather);
        return await Task.FromResult(weather);
    }
}