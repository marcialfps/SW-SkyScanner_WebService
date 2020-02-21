using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SW_SkyScanner_WebService.Services.Airports;
using SW_SkyScanner_WebService.Services.Airports.Model;

namespace SW_SkyScanner_WebService.Services.Weather
{
    public class WeatherWS : IWeatherWS
    {
        private HttpClient _client;
        private string _apiBaseUrlWeather;
        private string _apiBaseUrlForecast;
        
        private IAirportWS _airportWs;

        public WeatherWS()
        {
            // Client and weather API config
            _client = new HttpClient();
            _apiBaseUrlWeather = 
                "http://api.openweathermap.org/data/2.5/weather?appid=6764c33ce3b420a1f6963c61fc2a371c&units=metric";
            _apiBaseUrlForecast = 
                "http://api.openweathermap.org/data/2.5/forecast?appid=6764c33ce3b420a1f6963c61fc2a371c&units=metric";
            
            // Airport service
            _airportWs = new AirportWS();
        }

        // Add "&units=metric" to API call to get Celsius temperature.
        public Task<Model.Weather> GetWeatherByAirport(Airport airport)
        {
            return airport.Code == null ? null : GetWeatherByAirport(airport.Code);
        }

        public async Task<Model.Weather> GetWeatherByAirport(string airport)
        {
            // 1. Get airport coordinates
            Coordinate coordinate = await _airportWs.GetAirportCoordinates(airport);
            if (coordinate == null)
                return null;
            // 2. Request current weather in obtained coordinates
            Model.Weather weather = await GetWeatherByCoordinate(coordinate);
            return weather;
        }

        public Task<Model.Weather> GetForecastByAirport(Airport airport, int time)
        {
            return airport.Code == null ? null : GetForecastByAirport(airport.Code, time);
        }

        public async Task<Model.Weather> GetForecastByAirport(string airport, int time)
        {
            // 1. Get airport coordinates
            Coordinate coordinate = await _airportWs.GetAirportCoordinates(airport);
            if (coordinate == null)
                return null;
            
            // 2. Request weather forecast in obtained coordinates
            HttpResponseMessage response = _client.GetAsync($"{_apiBaseUrlForecast}&lat={(coordinate.Latitude).ToString(CultureInfo.InvariantCulture)}&" +
                                                            $"lon={(coordinate.Longitude).ToString(CultureInfo.InvariantCulture)}").GetAwaiter().GetResult();

            // 3. Arrange all the weather predictions into a list of weather objects
            IList<Model.Weather> weathers = new List<Model.Weather>();
            if (response.IsSuccessStatusCode)
            {
                weathers = new List<Model.Weather>();
                dynamic dynWeathers = JObject.Parse(await response.Content.ReadAsStringAsync());
                try
                {
                    foreach (dynamic predictedWeather in dynWeathers.list)
                    {
                        weathers.Add(new Model.Weather(predictedWeather));
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            // 3. For all the weather objects stored, return the prediction whose time is closer to the requested time
            if (weathers.Count > 0)
                return weathers.OrderBy(w => Math.Abs(w.Time - time)).First();

            return null;
        }
        
        public async Task<List<Model.Weather>> GetForecastByAirport(string airport)
        {
            // 1. Get airport coordinates
            Coordinate coordinate = await _airportWs.GetAirportCoordinates(airport);
            if (coordinate == null)
                return null;
            
            // 2. Request weather forecast in obtained coordinates
            HttpResponseMessage response = _client.GetAsync($"{_apiBaseUrlForecast}&lat={(coordinate.Latitude).ToString(CultureInfo.InvariantCulture)}&" +
                                                            $"lon={(coordinate.Longitude).ToString(CultureInfo.InvariantCulture)}").GetAwaiter().GetResult();

            // 3. Arrange all the weather predictions into a list of weather objects
            IList<Model.Weather> weathers = new List<Model.Weather>();
            if (response.IsSuccessStatusCode)
            {
                weathers = new List<Model.Weather>();
                dynamic dynWeathers = JObject.Parse(await response.Content.ReadAsStringAsync());
                try
                {
                    foreach (dynamic predictedWeather in dynWeathers.list)
                    {
                        weathers.Add(new Model.Weather(predictedWeather));
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            // 3. For all the weather objects stored, return the prediction whose time is closer to the requested time
            if (weathers.Count > 0)
                return weathers.ToList();

            return null;
        }

        public async Task<Model.Weather> GetWeatherByCoordinate(Coordinate coordinate)
        {
            Model.Weather weather = null;
            // 1. Call API on given latitude and longitude
            HttpResponseMessage response = _client.GetAsync($"{_apiBaseUrlWeather}&lat={(coordinate.Latitude).ToString(CultureInfo.InvariantCulture)}&" +
                                                            $"lon={(coordinate.Longitude).ToString(CultureInfo.InvariantCulture)}").GetAwaiter().GetResult();
            
            // 2. Parse API response to Weather object if API response was OK
            if (response.IsSuccessStatusCode)
            {
                dynamic dynWeather = JObject.Parse(await response.Content.ReadAsStringAsync());
                try
                {
                    weather = new Model.Weather(dynWeather);
                }
                catch (Exception)
                {
                    weather = null;
                }
            }
            return weather;
        }
    }
}