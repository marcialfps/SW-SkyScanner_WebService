namespace SW_SkyScanner_WebService.Services.Airports.Model
{
    public class Airport
    {
        public Airport()
        {}

        public Airport(dynamic dynAirport)
        {
            Code = dynAirport.icao;
            Name = dynAirport.name;
            City = dynAirport.city;
            Country = dynAirport.country;
            Phone = dynAirport.phone;
            PostalCode = dynAirport.postal_code;
            if (dynAirport.latitude != null && dynAirport.longitude != null)
                Location = new Coordinate((double) dynAirport.latitude, (double) dynAirport.longitude);
        }

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

        public override string ToString()
        {
            return $"Airport =>\n\t{nameof(Code)}: {Code}, {nameof(Name)}: {Name}, {nameof(City)}: {City}, " +
                   $"{nameof(Country)}: {Country}, {nameof(Phone)}: {Phone}, {nameof(PostalCode)}: {PostalCode}, " +
                   $"{nameof(Location)}: {Location}, {nameof(Weather)}: {Weather}";
        }
    }
}