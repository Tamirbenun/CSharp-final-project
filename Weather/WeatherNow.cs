using System.Text.Json.Serialization;

namespace Weather;

public class WeatherNow
{
    [JsonPropertyName("time")]
    public DateTime TimeNow { get; set; }

    [JsonPropertyName("temperature_2m")]
    public double TempNow { get; set; }

    [JsonPropertyName("is_day")]
    public int isDayNow { get; set; }

    [JsonPropertyName("rain")]
    public double RainNow { get; set; }

    [JsonPropertyName("cloud_cover")]
    public int CloudCoverNow { get; set; }

    [JsonPropertyName("wind_speed_10m")]
    public double WindSpeedNow { get; set; }

    [JsonPropertyName("wind_direction_10m")]
    public int WindDirectionNow { get; set; }

    [JsonPropertyName("surface_pressure")]
    public double Pressure { get; set; }
}
