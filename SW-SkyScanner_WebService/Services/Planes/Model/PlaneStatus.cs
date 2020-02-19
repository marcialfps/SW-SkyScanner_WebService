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
            if (dynStatus[3] != null)
                LastUpdate = (int)dynStatus[3];
            if (dynStatus[6] != null && dynStatus[5] != null)
                Location = new Coordinate((double)dynStatus[6], (double)dynStatus[5]);
            if (dynStatus[13] != null)
                Altitude = (double)dynStatus[13];
            if (dynStatus[9] != null)
                Speed = (double)dynStatus[9];
            if (dynStatus[8] != null)
                OnGround = (bool)dynStatus[8];
            if (dynStatus[11] != null)
            {
                VerticalRate = (double)dynStatus[11];
                Ascending = VerticalRate > 0;
            }
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
        public bool Ascending { get; set; }

        public override string ToString()
        {
            return $"Plane status =>\n\t{nameof(Icao24)}: {Icao24}, {nameof(LastUpdate)}: {LastUpdate}, " +
                   $"{nameof(Location)}: {Location}, {nameof(Altitude)}: {Altitude}, {nameof(Speed)}: {Speed}, " +
                   $"{nameof(OnGround)}: {OnGround}, {nameof(VerticalRate)}: {VerticalRate}, {nameof(Ascending)}: " +
                   $"{Ascending}";
        }
    }
}