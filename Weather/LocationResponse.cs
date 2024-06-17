using System.Text.Json.Serialization;

namespace Weather;

public partial class MainWindow
{
    public class LocationResponse
    {
        [JsonPropertyName("results")]
        public List<Results> Results { get; set; }
    }
}
