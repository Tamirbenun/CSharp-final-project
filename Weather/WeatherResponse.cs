using System.Text.Json.Serialization;

namespace Weather;

public class WeatherResponse
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
    
    [JsonPropertyName("utc_offset_seconds")]
    public int UTC { get; set; }

    [JsonPropertyName("timezone")]
    public string TimeZone { get; set; }

    [JsonPropertyName("elevation")]
    public double Elevation { get; set; }

    [JsonPropertyName("current")]
    public WeatherNow WeatherNow { get; set; }
    
    [JsonPropertyName("hourly")]
    public HourlyWeather HourlyWeather { get; set; }

    [JsonPropertyName("daily")]
    public DailyWeather DailyWeather { get; set; }
}
