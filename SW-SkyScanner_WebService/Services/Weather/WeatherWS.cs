using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;
using SW_SkyScanner_WebService.Services.Airports;
using SW_SkyScanner_WebService.Services.Airports.Model;

namespace SW_SkyScanner_WebService.Services.Weather
{
    public class WeatherWS : IWeatherWS
    {
        private HttpClient _client;
        private string _apiBaseUrl;
        
        private IAirportWS _airportWs;

        public WeatherWS()
        {
            // Client and weather API config
            _client = new HttpClient();
            _apiBaseUrl = "http://api.openweathermap.org/data/2.5/weather?appid=6764c33ce3b420a1f6963c61fc2a371c";
            
            // Airport service
            _airportWs = new AirportWS();
        }

        // Add "&units=metric" to API call to get Celsius temperature.
        public Task<Model.Weather> GetWeatherByAirport(Airport airport)
        {
            return GetWeatherByAirport(airport.Code);
        }

        public Task<Model.Weather> GetWeatherByAirport(string airport)
        {
            // 1. Get airport coordinates
            
            // 2. Request current weather in obtained coordinates
            
            // 3. Parse API response into weather object
            throw new NotImplementedException();
        }

        public Task<Model.Weather> GetForecastByAirport(Airport airport, int time)
        {
            return GetForecastByAirport(airport.Code, time);
        }

        public Task<Model.Weather> GetForecastByAirport(string airport, int time)
        {
            // 1. Get airport coordinates
            
            // 2. Request weather forecast in obtained coordinates
            
            // 3. For all the data returned, pik up the one whose "dt" is closer to the requested time
            
            // 3. Parse data into weather object
            throw new NotImplementedException();
        }

        public async Task<Model.Weather> GetWeatherByCoordinate(Coordinate coordinate)
        {
            Model.Weather weather = null;
            // 1. Call API on given latitude and longitude
            HttpResponseMessage response = _client.GetAsync($"{_apiBaseUrl}&lat={coordinate.Latitude}&" +
                                                            $"lon={coordinate.Longitude}").GetAwaiter().GetResult();
            
            // 2. Parse API response to Weather object if API response was OK
            if (response.IsSuccessStatusCode)
            {
                weather = new Model.Weather();
                dynamic dynWeather = JObject.Parse(await response.Content.ReadAsStringAsync());
                
                // Fill weather object properties
                weather.Main = dynWeather.weather[0].main;
                weather.Description = dynWeather.weather[0].description;
                weather.Temperature = (double)dynWeather.main.temp;
                weather.TemperatureMax = (double)dynWeather.main.temp_max;
                weather.TemperatureMin = (double)dynWeather.main.temp_min;
                weather.Pressure = (int)dynWeather.main.pressure;
                weather.Humidity = (int)dynWeather.main.humidity;
                weather.WindSpeed = (double)dynWeather.wind.speed;
                weather.WindDirection = (double)dynWeather.wind.speed;
                weather.Cloudiness = (int)dynWeather.clouds.all;
            }
            return weather;
        }
    }
}