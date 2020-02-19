using SW_SkyScanner_WebService.Services.Airports.Model;

namespace SW_SkyScanner_WebService.Services.Planes.Model
{
    public class PlaneStatus
    {
        public PlaneStatus()
        {}
        public PlaneStatus(dynamic dynStatus)
        {
            Icao24 = dynStatus[0];
            LastUpdate = (int)dynStatus[3];
            Location = new Coordinate((double)dynStatus[6], (double)dynStatus[5]);
            Altitude = (double)dynStatus[13];
            Speed = (double)dynStatus[9];
            OnGround = (bool)dynStatus[8];
            VerticalRate = (double)dynStatus[11];
        }

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