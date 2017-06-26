using Newtonsoft.Json;

namespace WeatherApi.Models
{
    public class FutureDayForecast : Forecast
    {
        // Minimal temperature by Fahrenheit.
        [JsonProperty("temperatureMin")]
        public float TemperatureMin { get; set; }

        // Maximal temperature by Fahrenheit.
        [JsonProperty("temperatureMax")]
        public float TemperatureMax { get; set; }

        // Minimal apparent temperature by Fahrenheit.
        [JsonProperty("apparentTemperatureMin")]
        public float ApparentTemperatureMin { get; set; }

        // Maximal apparent temperature by Fahrenheit.
        [JsonProperty("apparentTemperatureMax")]
        public float ApparentTemperatureMax { get; set; }
    }
}