using System;

namespace SW_SkyScanner_WebService.Services.Airports.Model
{
    [Serializable]
    public class Coordinate
    {
        public Coordinate()
        {}

        public Coordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        
        public double Longitude { get; set; }

        public override string ToString()
        {
            return $"Coordinate =>\n\t{nameof(Latitude)}: {Latitude}, {nameof(Longitude)}: {Longitude}";
        }
    }
}