using System.Collections.Generic;
using System.Threading.Tasks;
using SW_SkyScanner_WebService.Services.Airports.Model;


namespace SW_SkyScanner_WebService.Services.Weather
{
    public interface IWeatherWS
    {
        /// <summary>
        /// Get the current weather conditions in a given airport
        /// </summary>
        /// <param name="airport">Airport for which the weather is requested</param>
        /// <returns>Current weather in the target airport (<paramref name="airport"/>)</returns>
        Task<Model.Weather> GetWeatherByAirport(Airport airport);
        
        /// <summary>
        /// Get the current weather conditions in a given airport
        /// </summary>
        /// <param name="airport">Code of the airport for which the weather is requested</param>
        /// <returns>Current weather in the target airport (<paramref name="airport"/>)</returns>
        Task<Model.Weather> GetWeatherByAirport(string airport);
        
        /// <summary>
        /// Get the approximate weather conditions of an airport at a given time.
        /// </summary>
        /// <param name="airport">Airport for which the weather is requested</param>
        /// <param name="time">Seconds since epoch for which the forecast is requested</param>
        /// <returns>Weather forecast for the given airport in the given time</returns>
        Task<Model.Weather> GetForecastByAirport(Airport airport, int time);
        
        /// <summary>
        /// Get the approximate weather conditions of an airport at a given time.
        /// </summary>
        /// <param name="airport">Code of the airport for which the weather is requested</param>
        /// <param name="time">Seconds since epoch for which the forecast is requested</param>
        /// <returns>Weather forecast for the given airport in the given time</returns>
        Task<Model.Weather> GetForecastByAirport(string airport, int time);
        
        /// <summary>
        /// Get the approximate weather conditions of an airport for the next 5 days (3 hour intervals).
        /// </summary>
        /// <param name="airport">Code of the airport for which the weather is requested</param>
        /// <returns>Weather forecast for the given airport in the given time</returns>
        Task<List<Model.Weather>> GetForecastByAirport(string airport);
        
        /// <summary>
        /// Get the current weather conditions in a given location
        /// </summary>
        /// <param name="coordinate">Coordinates of the location for which the weather is requested</param>
        /// <returns>Current weather in the target location defined by (<paramref name="coordinate"/>)</returns>
        Task<Model.Weather> GetWeatherByCoordinate(Coordinate coordinate);
    }
}