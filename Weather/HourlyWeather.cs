using System.Text.Json.Serialization;

namespace Weather;

public class HourlyWeather
{
    [JsonPropertyName("time")]
    public List<DateTime> Time { get; set; }

    [JsonPropertyName("temperature_2m")]
    public List<double> Temp { get; set; }

    [JsonPropertyName("relative_humidity_2m")]
    public List<int> Humidity { get; set; }

    [JsonPropertyName("rain")]
    public List<double> Rain { get; set; }

    [JsonPropertyName("snow_depth")]
    public List<double> Snow { get; set; }

    [JsonPropertyName("cloud_cover")]
    public List<int> Cloud { get; set; }

    [JsonPropertyName("visibility")]
    public List<double> Visibility { get; set; }

    [JsonPropertyName("uv_index")]
    public List<double> UVindex { get; set; }

    [JsonPropertyName("is_day")]
    public List<int> isDayHourly { get; set; }
}
