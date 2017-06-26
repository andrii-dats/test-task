using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherApi.Models
{
    public class WeeklyForecast
    {
        [JsonProperty("currently")]
        public TodayForecast Current { get; set; }

        [JsonProperty("futureForecasts")]
        public List<FutureDayForecast> FutureDays { get; set; }
    }
}
