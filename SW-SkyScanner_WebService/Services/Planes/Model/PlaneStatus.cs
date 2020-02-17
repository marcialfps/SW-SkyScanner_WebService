namespace SW_SkyScanner_WebService.Services.Planes.Model
{
    public class PlaneStatus
    {
        
//        Custom properties

        /// <summary>
        /// Flight code
        /// </summary>
        public string Icao24 { get; set; }
        
        /// <summary>
        /// Last position update time in Unix time (seconds since epoch)
        /// </summary>
        public int LastUpdate => time_position;
        
        /// <summary>
        /// Last detected longitude of the plane
        /// </summary>
        public double Longitude => longitude;
        
        /// <summary>
        /// Last detected latitude of the plane
        /// </summary>
        public double Latitude => latitude;

        /// <summary>
        /// Last detected altitude of the plane
        /// </summary>
        public double Altitude => geo_altitude;
        
        /// <summary>
        /// Last detected speed of the plane
        /// </summary>
        public double Speed => velocity;
        
        /// <summary>
        /// The plane is on ground or not.
        /// </summary>
        public bool OnGround => on_ground;
        
        /// <summary>
        /// The plane is ascending or not
        /// </summary>
        public bool Ascending => vertical_rate > 0;


//        Base properties received from API. We map the API JSON response to these properties.
        
//        private string icao42;
        
        private int time_position;
        private float longitude;
        private float latitude;
        private float geo_altitude;
        private float velocity;
        private float vertical_rate;
        private bool on_ground;
    }
}