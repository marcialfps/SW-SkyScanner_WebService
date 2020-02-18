namespace SW_SkyScanner_WebService.Services.Airports.Model
{
    public class Airport
    {
        /// <summary>
        /// Airport icao code
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Airport name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Airport city
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// Airport country
        /// </summary>
        public string Country { get; set; }
        
        /// <summary>
        /// Airport phone contact
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// Airport postal code
        /// </summary>
        public string PostalCode { get; set; }
        
        /// <summary>
        /// Airport coordinates
        /// </summary>
        public Coordinate Location { get; set; }
        
        /// <summary>
        /// Airport weather condition
        /// </summary>
        public Weather.Model.Weather Weather { get; set; }
    }
}