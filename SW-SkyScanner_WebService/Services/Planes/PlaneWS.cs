using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SW_SkyScanner_WebService.Services.Airports.Model;
using SW_SkyScanner_WebService.Services.Planes.Model;

namespace SW_SkyScanner_WebService.Services.Planes
{
    public class PlaneWS : IPlaneWs
    {
        public Task<PlaneStatus> GetPlaneStatus(Plane plane)
        {
            return GetPlaneStatus(plane.Icao24);
        }

        public Task<PlaneStatus> GetPlaneStatus(string planeCode)
        {
            throw new NotImplementedException();
        }

        public Task<List<Plane>> GetPlanesByDeparture(Airport departureAirport)
        {
            return GetPlanesByDeparture(departureAirport.Code);
        }

        public Task<List<Plane>> GetPlanesByDeparture(string departureAirport)
        {
            throw new NotImplementedException();
        }

        public Task<List<Plane>> GetPlanesByArrival(Airport arrivalAirport)
        {
            return GetPlanesByArrival(arrivalAirport.Code);
        }

        public Task<List<Plane>> GetPlanesByArrival(string arrivalAirport)
        {
            throw new NotImplementedException();
        }

        public Task<List<Plane>> GetPlanesCloseToAirport(Airport airport)
        {
            return GetPlanesCloseToAirport(airport.Code);
        }

        public Task<List<Plane>> GetPlanesCloseToAirport(string airport)
        {
            throw new NotImplementedException();
        }
    }
}