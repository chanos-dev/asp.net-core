namespace Swagger;

public class WeatherForecast
{
    /// <summary>
    /// ÀÏÀÚ
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// ¼·¾¾
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// È­¾¾
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// ³¯¾¾
    /// </summary>
    public string? Summary { get; set; }
}