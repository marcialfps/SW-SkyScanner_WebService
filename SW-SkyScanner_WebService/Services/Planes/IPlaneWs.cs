using System.Collections.Generic;
using System.Threading.Tasks;
using SW_SkyScanner_WebService.Services.Airports.Model;
using SW_SkyScanner_WebService.Services.Planes.Model;

namespace SW_SkyScanner_WebService.Services.Planes
{
    public interface IPlaneWs
    {

        /// <summary>
        /// For a given flight, retrieves the current status of the plane.
        /// </summary>
        /// <param name="plane">Plane whose status is searched</param>
        /// <returns>Status of the requested plane (<paramref name="plane"/>)</returns>
        Task<PlaneStatus> GetPlaneStatus(Plane plane);
        
        /// <summary>
        /// For a given flight, retrieves the status of the plane for a given time.
        /// </summary>
        /// <param name="planeCode">Code of the flight whose status is searched</param>
        /// <param name="time">Time for which the status is requested</param>
        /// <returns>Status of the requested plane (<paramref name="plane"/>) in time <paramref name="time"/></returns>
        Task<PlaneStatus> GetPlaneStatus(string planeCode, long time);
        
        /// <summary>
        /// For a given flight, retrieves the current status of the plane.
        /// </summary>
        /// <param name="planeCode">Code of the flight whose status is searched</param>
        /// <returns>Status of the requested flight (<paramref name="planeCode"/>)</returns>
        Task<PlaneStatus> GetPlaneStatus(string planeCode);
        
        /// <summary>
        /// Return the list of planes currently flying departing from the given airport.
        /// </summary>
        /// <param name="departureAirport">Departure airport to filter the returned planes</param>
        /// <returns>List of active planes departed from <paramref name="departureAirport"/></returns>
        Task<IList<Plane>> GetPlanesByDeparture(Airport departureAirport);
        
        /// <summary>
        /// Return the list of planes currently flying departing from the given airport with their associated
        /// airports, status and current weather conditions.
        /// </summary>
        /// <param name="departureAirport">Departure airport to filter the returned planes</param>
        /// <returns>List of active planes departed from <paramref name="departureAirport"/></returns>
        Task<IList<Plane>> GetPlanesByDepartureDetail(Airport departureAirport);
        
        /// <summary>
        /// Return the list of planes currently flying departing from the given airport.
        /// </summary>
        /// <param name="departureAirport">Code of the departure airport to filter the returned planes</param>
        /// <returns>List of active planes departed from <paramref name="departureAirport"/> airport code</returns>
        Task<IList<Plane>> GetPlanesByDeparture(string departureAirport);
        
        /// <summary>
        /// Return the list of planes currently flying departing from the given airport with their associated
        /// airports, status and current weather conditions.
        /// </summary>
        /// <param name="departureAirport">Departure airport to filter the returned planes</param>
        /// <returns>List of active planes departed from <paramref name="departureAirport"/></returns>
        Task<IList<Plane>> GetPlanesByDepartureDetail(string departureAirport);
        
        /// <summary>
        /// Return the list of planes currently flying arriving in the given airport.
        /// </summary>
        /// <param name="arrivalAirport">Arrival airport to filter the returned planes</param>
        /// <returns>List of active planes arriving in <paramref name="arrivalAirport"/></returns>
        Task<IList<Plane>> GetPlanesByArrival(Airport arrivalAirport);
        
        /// <summary>
        /// Return the list of planes currently flying arriving in the given airport with their associated
        /// airports, status and current weather conditions.
        /// </summary>
        /// <param name="arrivalAirport">Arrival airport to filter the returned planes</param>
        /// <returns>List of active planes arriving in <paramref name="arrivalAirport"/></returns>
        Task<IList<Plane>> GetPlanesByArrivalDetail(Airport arrivalAirport);
        
        /// <summary>
        /// Return the list of planes currently flying arriving in the given airport.
        /// </summary>
        /// <param name="arrivalAirport">Code of the arrival airport to filter the returned planes</param>
        /// <returns>List of active planes arriving in <paramref name="arrivalAirport"/> airport code</returns>
        Task<IList<Plane>> GetPlanesByArrival(string arrivalAirport);
        
        /// <summary>
        /// Return the list of planes currently flying arriving in the given airport with their associated
        /// airports, status and current weather conditions.
        /// </summary>
        /// <param name="arrivalAirport">Arrival airport to filter the returned planes</param>
        /// <returns>List of active planes arriving in <paramref name="arrivalAirport"/></returns>
        Task<IList<Plane>> GetPlanesByArrivalDetail(string arrivalAirport);
        
        
        /// <summary>
        /// Return the list of planes that are close to the requested airport.
        /// </summary>
        /// <param name="airport">Airport used to filter the returned planes</param>
        /// <returns>List of active planes that are nearby <paramref name="airport"/></returns>
        Task<IList<Plane>> GetPlanesCloseToAirport(Airport airport);


        /// <summary>
        /// Return the list of planes that are close to the requested airport.
        /// </summary>
        /// <param name="airport">Code of the airport used to filter the returned planes</param>
        /// <returns>List of active planes that are nearby <paramref name="airport"/></returns>
        Task<IList<Plane>> GetPlanesCloseToAirport(string airport);

    }
}