using System;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SW_SkyScanner_WebService.Services.Airports.Model
{
    [Serializable]
    public class Coordinate
    {

        public Coordinate()
        {
            Latitude = 0.0;
            Longitude = 0.0;
        }

        public Coordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
            
        }

        public double Latitude { get; }
        
        public double Longitude { get; }
        
    }
}