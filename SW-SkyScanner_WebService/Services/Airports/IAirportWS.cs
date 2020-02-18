using System.Threading.Tasks;
using SW_SkyScanner_WebService.Services.Airports.Model;

namespace SW_SkyScanner_WebService.Services.Airports
{
    public interface IAirportWS
    {
        /// <summary>
        /// Retrieves information about an airport given its ICAO code.
        /// </summary>
        /// <param name="code">ICAO code of the target airport.</param>
        /// <returns>Airport information.</returns>
        Task<Airport> GetAirport(string code);

        /// <summary>
        /// Retrieves the coordinates of an airport given its ICAO code.
        /// </summary>
        /// <param name="code">ICAO code of the target airport.</param>
        /// <returns>Airport coordinates.</returns>
        Task<Coordinate> GetAirportCoordinates(string code);
    }
}