using System.Text.Json.Serialization;

namespace Weather;

public class DailyWeather
{
    [JsonPropertyName("time")]
    public List<DateTime> Day { get; set; }

    [JsonPropertyName("temperature_2m_max")]
    public List<double> TempDayMax { get; set; }

    [JsonPropertyName("temperature_2m_min")]
    public List<double> TempMinDay { get; set; }

    [JsonPropertyName("sunrise")]
    public List<DateTime> SunriseDay { get; set; }

    [JsonPropertyName("sunset")]
    public List<DateTime> SunsetDay { get; set; }

    [JsonPropertyName("uv_index_max")]
    public List<double> UVMaxDay { get; set; }

    [JsonPropertyName("rain_sum")]
    public List<double> RainSum { get; set; }
}