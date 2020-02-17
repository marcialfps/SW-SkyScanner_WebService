using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SW_SkyScanner_WebService.Services.Airports.Model;
using SW_SkyScanner_WebService.Services.Planes.Model;

namespace SW_SkyScanner_WebService.Services.Planes
{
    public class PlaneWS : IPlaneWs
    {
        public PlaneStatus GetPlaneStatus(Plane plane)
        {
            throw new NotImplementedException();
        }

        public PlaneStatus GetPlaneStatus(string planeCode)
        {
            throw new NotImplementedException();
        }

        public List<Plane> GetPlanesByDeparture(Airport departureAirport)
        {
            throw new NotImplementedException();
        }

        public List<Plane> GetPlanesByDeparture(string departureAirport)
        {
            throw new NotImplementedException();
        }

        public List<Plane> GetPlanesByArrival(Airport arrivalAirport)
        {
            throw new NotImplementedException();
        }

        public List<Plane> GetPlanesByArrival(string arrivalAirport)
        {
            throw new NotImplementedException();
        }
    }
}