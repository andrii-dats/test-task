using Newtonsoft.Json;

namespace WeatherApi.Models
{
    public class City
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("placeId")]
        public string PlaceId { get; set; }
    }
}
