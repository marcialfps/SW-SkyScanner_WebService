namespace SW_SkyScanner_WebService.Services.Planes.Model
{
    public class Plane
    {
        
//        Custom properties

        /// <summary>
        /// Status of the plane: coordinates, altitude, etc.
        /// </summary>
        public PlaneStatus Status { get; set; }

        /// <summary>
        /// Flight code
        /// </summary>
        public string Icao24 { get; set; }
        
        /// <summary>
        /// Departure airport code
        /// </summary>
        public string DepartureAirport => estDepartureAirport;

        /// <summary>
        /// Arrival airport code
        /// </summary>
        public string ArrivalAirport => estArrivalAirport;

        /// <summary>
        /// Estimated departure time in Unix time (seconds since epoch)
        /// </summary>
        public int DepartureTime => firstSeen;

        /// <summary>
        /// Estimated arrival time in Unix time (seconds since epoch)
        /// </summary>
        public int ArrivalTime => lastseen;

        /// <summary>
        /// Estimated distance between the plane and the departure airport (meters)
        /// </summary>
        public int DepartureDistance => estDepartureAirportHorizDistance;

        /// <summary>
        /// Estimated distance between the plane and the arrival airport (meters)
        /// </summary>
        public int ArrivalDistance => estArrivalAirportHorizDistance;


//        Base properties received from API. We map the API JSON response to these properties.
        
//        private string icao42;
        
        private int firstSeen;
        private int lastseen;
        
        private string estDepartureAirport;
        private string estArrivalAirport;
        
        private int estDepartureAirportHorizDistance;
        private int estArrivalAirportHorizDistance;
    }
}