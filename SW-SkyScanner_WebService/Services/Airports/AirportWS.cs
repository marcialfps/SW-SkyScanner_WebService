using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SW_SkyScanner_WebService.Services.Airports.Model;

namespace SW_SkyScanner_WebService.Services.Airports
{
    public class AirportWS : IAirportWS
    {
        private HttpClient _client;
        private string _apiBaseUrl;

        public AirportWS()
        {
            // Configure client with API credentials
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("x-rapidapi-host","airport-info.p.rapidapi.com");
            _client.DefaultRequestHeaders.Add("x-rapidapi-key","d943b24b2cmsh8a5de079b66bffcp1a634djsnf1f647cfb704");
            _apiBaseUrl = "https://airport-info.p.rapidapi.com/airport";
        }
        
        public async Task<Airport> GetAirport(string code)
        {
            Airport airport = null;
            HttpResponseMessage response = _client.GetAsync($"{_apiBaseUrl}?icao={code}").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                // Build dynamic object from JSON response
                dynamic dynAirport = JObject.Parse(await response.Content.ReadAsStringAsync());
                // Check the response was not and error
                if (dynAirport.error == null)
                {
                    // Construct Airport object from JSON response
                    try
                    {
                        airport = new Airport(dynAirport);
                    }
                    catch (Exception)
                    {
                        airport = null;
                    }
                }
            }
            return airport;
        }

        public async Task<Coordinate> GetAirportCoordinates(string code)
        {
            Airport airport = await GetAirport(code);
            
            if (airport == null)
                return null;
            
            return airport.Location;
        }
    }
}