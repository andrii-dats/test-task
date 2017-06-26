using Newtonsoft.Json;

namespace WeatherApi.Models
{
    public class TodayForecast : Forecast
    {
        // Temperature by Fahrenheit.
        [JsonProperty("temperature")]
        public float Temperature { get; set; }

        // Apparent temperature by Fahrenheit.
        [JsonProperty("apparentTemperature")]
        public float ApparentTemperature { get; set; }
    }
}