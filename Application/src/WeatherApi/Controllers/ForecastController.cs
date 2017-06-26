using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherApi.Models;

namespace WeatherApi.Controllers
{
    [Route("api/[controller]")]
    public class ForecastController : Controller
    {
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        const string FORECAST_API_KEY = "2c43986f39cf0dd6f5ce3a8378301aa2";

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        const string WORLD_WEATHER_API_KEY = "1918c3ee0f1845369ae192513170406";

        // GET: api/forecast?longitude&latitude&source
        public async Task<string> Get([FromQuery]float latitude, [FromQuery]float longitude, [FromQuery]string source)
        {
            switch (source)
            {
                case "FORECAST_IO":
                    return await loadForecastFromForecastIO(latitude, longitude);
                case "WORLD_WEATHER":
                    return await loadForecastFromWorldWeather(latitude, longitude);
                default:
                    return "{error: \"Incorrect type of the source.\"}";
            }
        }

        private async Task<string> loadForecastFromForecastIO(float latitude, float longitude)
        {
            using (var client = new HttpClient())
            {
                var address = string.Format(
                    "https://api.forecast.io/forecast/{0}/{1},{2}",
                    FORECAST_API_KEY, longitude, latitude);
                var response = await client.GetStringAsync(address).ConfigureAwait(false);

                return formatForecastIOResponse(response);
            }
        }

        private string formatForecastIOResponse(string response)
        {
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response);

            var currentlyWeather = jsonResponse["currently"];
            var todayForecast = new TodayForecast
            {
                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                Humidity = currentlyWeather["humidity"],
                Pressure = currentlyWeather["pressure"],
                CloudCover = currentlyWeather["cloudCover"],
                Temperature = currentlyWeather["temperature"],
                ApparentTemperature = currentlyWeather["apparentTemperature"]
            };

            var currentDate = DateTime.Now.Date;
            var futureForecasts = jsonResponse["daily"]["data"];

            var futureDayForecasts = new List<FutureDayForecast>();
            foreach (var futureForecast in futureForecasts)
            {

                long seconds = futureForecast["time"];
                DateTime targetDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                targetDate = targetDate.AddSeconds(seconds);

                if (targetDate.Date > currentDate)
                {
                    var forecastForFutureDay = new FutureDayForecast
                    {
                        Date = targetDate.ToString("yyyy-MM-dd"),
                        Humidity = futureForecast["humidity"],
                        Pressure = futureForecast["pressure"],
                        CloudCover = futureForecast["cloudCover"],
                        TemperatureMin = futureForecast["temperatureMin"],
                        TemperatureMax = futureForecast["temperatureMax"],
                        ApparentTemperatureMin = futureForecast["apparentTemperatureMin"],
                        ApparentTemperatureMax = futureForecast["apparentTemperatureMax"]
                    };
                    futureDayForecasts.Add(forecastForFutureDay);
                }
            }

            var weeklyForecast = new WeeklyForecast
            {
                Current = todayForecast,
                FutureDays = futureDayForecasts
            };
            return JsonConvert.SerializeObject(weeklyForecast);
        }

        private async Task<string> loadForecastFromWorldWeather(float latitude, float longitude)
        {
            using (var client = new HttpClient())
            {
                var address = string.Format(
                    "http://api.worldweatheronline.com/premium/v1/weather.ashx?key={0}&q={1},{2}&format=json&num_of_days=7&fx24=no&show_comments=no&tp=24",
                    WORLD_WEATHER_API_KEY, latitude, longitude);
                var response = await client.GetStringAsync(address).ConfigureAwait(false);
                return formatWorldWeatherResponse(response);
            }
        }

        private string formatWorldWeatherResponse(string response)
        {
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response);

            var currentlyWeather = jsonResponse["data"]["current_condition"][0];
            var todayForecast = new TodayForecast
            {
                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                Humidity = float.Parse((string)currentlyWeather["humidity"]) / 100,
                Pressure = float.Parse((string)currentlyWeather["pressure"]),
                CloudCover = float.Parse((string)currentlyWeather["cloudcover"]) / 100,
                Temperature = float.Parse((string)currentlyWeather["temp_F"]),
                ApparentTemperature = float.Parse((string)currentlyWeather["FeelsLikeF"])
            };

            var currentDate = DateTime.Now.Date;
            var futureForecasts = jsonResponse["data"]["weather"];

            var futureDayForecasts = new List<FutureDayForecast>();
            foreach (var futureForecast in futureForecasts)
            {
                var forecastDate = DateTime.Parse((string)futureForecast["date"]);
                if (forecastDate > currentDate)
                {
                    var weatherOfFutureDay = futureForecast["hourly"][0];

                    var forecastForFutureDay = new FutureDayForecast()
                    {
                        Date = futureForecast["date"],
                        Humidity = float.Parse((string)weatherOfFutureDay["humidity"]) / 100,
                        Pressure = float.Parse((string)weatherOfFutureDay["pressure"]),
                        CloudCover = float.Parse((string)weatherOfFutureDay["cloudcover"]) / 100,
                        TemperatureMin = futureForecast["mintempF"],
                        TemperatureMax = futureForecast["maxtempF"],
                        ApparentTemperatureMax = float.Parse((string)weatherOfFutureDay["FeelsLikeF"])
                    };
                    futureDayForecasts.Add(forecastForFutureDay);
                }
            }

            var weeklyForecast = new WeeklyForecast
            {
                Current = todayForecast,
                FutureDays = futureDayForecasts
            };
            return JsonConvert.SerializeObject(weeklyForecast);
        }
    }
}
