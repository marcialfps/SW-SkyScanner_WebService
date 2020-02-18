namespace SW_SkyScanner_WebService.Services.Airports.Model
{
    public class Airport
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        
        public string Phone { get; set; }
        
        public string PostalCode { get; set; }
        public Coordinate Location { get; set; }
    }
}