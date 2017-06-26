using Newtonsoft.Json;

namespace WeatherApi.Models
{
    public class Forecast
    {
        // Date in format 'YYYY-MM-DD', which displays related date to this forecast.
        [JsonProperty("date")]
        public string Date { get; set; }

        // Possible values are from 0.0 to 1.0.
        [JsonProperty("humidity")]
        public float Humidity { get; set; }

        [JsonProperty("pressure")]
        public float Pressure { get; set; }

        // Possible values are from 0.0 to 1.0.
        [JsonProperty("cloudCover")]
        public float CloudCover { get; set; }
    }
}