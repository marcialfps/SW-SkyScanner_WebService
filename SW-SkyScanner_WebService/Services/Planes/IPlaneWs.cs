using System.Collections.Generic;
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
        PlaneStatus GetPlaneStatus(Plane plane);
        
        /// <summary>
        /// For a given flight, retrieves the current status of the plane.
        /// </summary>
        /// <param name="planeCode">Code of the flight whose status is searched</param>
        /// <returns>Status of the requested flight (<paramref name="planeCode"/>)</returns>
        PlaneStatus GetPlaneStatus(string planeCode);
        
        /// <summary>
        /// Return the list of planes currently flying departing from the given airport.
        /// </summary>
        /// <param name="departureAirport">Departure airport to filter the returned planes</param>
        /// <returns>List of active planes departed from <paramref name="departureAirport"/></returns>
        List<Plane> GetPlanesByDeparture(Airport departureAirport);
        
        /// <summary>
        /// Return the list of planes currently flying departing from the given airport.
        /// </summary>
        /// <param name="departureAirport">Code of the departure airport to filter the returned planes</param>
        /// <returns>List of active planes departed from <paramref name="departureAirport"/> airport code</returns>
        List<Plane> GetPlanesByDeparture(string departureAirport);
        
        /// <summary>
        /// Return the list of planes currently flying arriving in the given airport.
        /// </summary>
        /// <param name="arrivalAirport">Arrival airport to filter the returned planes</param>
        /// <returns>List of active planes arriving in <paramref name="arrivalAirport"/></returns>
        List<Plane> GetPlanesByArrival(Airport arrivalAirport);
        
        /// <summary>
        /// Return the list of planes currently flying arriving in the given airport.
        /// </summary>
        /// <param name="arrivalAirport">Code of the arrival airport to filter the returned planes</param>
        /// <returns>List of active planes arriving in <paramref name="arrivalAirport"/> airport code</returns>
        List<Plane> GetPlanesByArrival(string arrivalAirport);
    }
}