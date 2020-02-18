using SW_SkyScanner_WebService.Services.Airports.Model;

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
        public int LastUpdate { get; set; }
        
        /// <summary>
        /// Last detected location of the plane
        /// </summary>
        public Coordinate Location { get; set; }

        /// <summary>
        /// Last detected altitude of the plane
        /// </summary>
        public double Altitude { get; set; }
        
        /// <summary>
        /// Last detected speed of the plane
        /// </summary>
        public double Speed { get; set; }
        
        /// <summary>
        /// The plane is on ground or not.
        /// </summary>
        public bool OnGround { get; set; }
        
        /// <summary>
        /// Vertical rate of the plane
        /// </summary>
        public double VerticalRate { get; set; }
        
        /// <summary>
        /// The plane is ascending or not
        /// </summary>
        public bool Ascending => VerticalRate > 0;

    }
}