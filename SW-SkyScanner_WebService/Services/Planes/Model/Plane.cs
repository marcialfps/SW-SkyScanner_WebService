using SW_SkyScanner_WebService.Services.Airports.Model;

namespace SW_SkyScanner_WebService.Services.Planes.Model
{
    public class Plane
    {
        public Plane()
        {}

        public Plane(dynamic dynPlane)
        {
            Icao24 = dynPlane.icao24;
            DepartureAirportCode = dynPlane.estDepartureAirport;
            ArrivalAirportCode = dynPlane.estArrivalAirport;
            if (dynPlane.firstSeen != null)
                DepartureTime = (int) dynPlane.firstSeen;
            if (dynPlane.lastSeen != null)
                ArrivalTime = (int) dynPlane.lastSeen;
            if (dynPlane.estDepartureAirportHorizDistance != null)
                DepartureDistance = (int)dynPlane.estDepartureAirportHorizDistance;
            if (dynPlane.estArrivalAirportHorizDistance != null)
                ArrivalDistance = (int)dynPlane.estArrivalAirportHorizDistance;
        }

        /// <summary>
        /// Status of the plane: coordinates, altitude, etc.
        /// </summary>
        public PlaneStatus Status { get; set; }

        /// <summary>
        /// Flight code
        /// </summary>
        public string Icao24 { get; set; }
        
        /// <summary>
        /// Departure airport
        /// </summary>
        public Airport DepartureAirport { get; set; }
        
        /// <summary>
        /// Departure airport code
        /// </summary>
        public string DepartureAirportCode { get; set; }

        /// <summary>
        /// Arrival airport
        /// </summary>
        public Airport ArrivalAirport { get; set; }
        
        /// <summary>
        /// Arrival airport code
        /// </summary>
        public string ArrivalAirportCode { get; set; }
        
        /// <summary>
        /// Weather conditions on the plane location
        /// </summary>
        public Weather.Model.Weather Weather { get; set; }

        /// <summary>
        /// Estimated departure time in Unix time (seconds since epoch)
        /// </summary>
        public int DepartureTime { get; set; }

        /// <summary>
        /// Estimated arrival time in Unix time (seconds since epoch)
        /// </summary>
        public int ArrivalTime { get; set; }

        /// <summary>
        /// Estimated distance between the plane and the departure airport (meters)
        /// </summary>
        public int DepartureDistance { get; set; }

        /// <summary>
        /// Estimated distance between the plane and the arrival airport (meters)
        /// </summary>
        public int ArrivalDistance { get; set; }

        public override string ToString()
        {
            return $"Plane =>\n\t{nameof(Status)}: {Status}, {nameof(Icao24)}: {Icao24}, {nameof(DepartureAirport)}: " +
                   $"{DepartureAirport}, {nameof(DepartureAirportCode)}: {DepartureAirportCode}, " +
                   $"{nameof(ArrivalAirport)}: {ArrivalAirport}, {nameof(ArrivalAirportCode)}: " +
                   $"{ArrivalAirportCode}, {nameof(Weather)}: {Weather}, {nameof(DepartureTime)}: {DepartureTime}, " +
                   $"{nameof(ArrivalTime)}: {ArrivalTime}, {nameof(DepartureDistance)}: {DepartureDistance}, " +
                   $"{nameof(ArrivalDistance)}: {ArrivalDistance}";
        }
    }
}