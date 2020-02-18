using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SW_SkyScanner_WebService.Services.Airports.Model;

namespace SW_SkyScanner_WebService.Services.Weather
{
    public class WeatherWS : IWeatherWS
    {
        // Add "&units=metric" to API call to get Celsius temperature.
        public Task<Model.Weather> GetWeatherByAirport(Airport airport)
        {
            return GetWeatherByAirport(airport.Code);
        }

        public Task<Model.Weather> GetWeatherByAirport(string airport)
        {
            throw new NotImplementedException();
        }

        public Task<Model.Weather> GetForecastByAirport(Airport airport, int time)
        {
            return GetForecastByAirport(airport.Code, time);
        }

        public Task<Model.Weather> GetForecastByAirport(string airport, int time)
        {
            throw new NotImplementedException();
        }

        public Task<Model.Weather> GetWeatherByCoordinate(Coordinate coordinate)
        {
            throw new NotImplementedException();
        }
    }
}