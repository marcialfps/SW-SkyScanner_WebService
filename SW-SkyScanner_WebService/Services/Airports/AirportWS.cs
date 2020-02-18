﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
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
                    airport = new Airport();
                
                    airport.Code = dynAirport.icao;
                    airport.Name = dynAirport.name;
                    airport.City = dynAirport.city;
                    airport.Country = dynAirport.country;
                    airport.Phone = dynAirport.phone;
                    airport.PostalCode = dynAirport.postal_code;
                    airport.Location = new Coordinate((double)dynAirport.latitude, (double)dynAirport.longitude);
                }
            }
            return airport;
        }

        public async Task<Coordinate> GetAirportCoordinates(string code)
        {
            Coordinate coordinate = null;
            HttpResponseMessage response = _client.GetAsync($"{_apiBaseUrl}?icao={code}").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                // Build dynamic object from JSON response
                dynamic dynAirport = JObject.Parse(await response.Content.ReadAsStringAsync());
                
                // Construct Coordinate object from JSON response
                coordinate = new Coordinate(dynAirport.latitude, dynAirport.longitude);
            }
            return coordinate;
        }
    }
}