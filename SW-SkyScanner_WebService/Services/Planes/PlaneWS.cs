using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SW_SkyScanner_WebService.Services.Airports;
using SW_SkyScanner_WebService.Services.Airports.Model;
using SW_SkyScanner_WebService.Services.Planes.Model;
using SW_SkyScanner_WebService.Services.Weather;

namespace SW_SkyScanner_WebService.Services.Planes
{
    public class PlaneWS : IPlaneWs
    {
        /// <summary>
        /// Limits the amount of hours in the past/future that we'll request plane data.
        /// e.g.: a limit of 5 hours means we'll ask for plane data arriving/departing from (current time - 5 hours)
        /// to (current time + 5 hours).
        /// </summary>
        private const int HourDetectionLimit = 30;
        
        /// <summary>
        /// Limit the amount of planes we can retrieve at a time when fetching detailed info
        /// </summary>
        private const int PlanesLimit = 5;
        
        /// <summary>
        /// Limit the amount of planes we can retrieve at a time when not fetching detailed info
        /// </summary>
        private const int PlanesLimitStandard = 30;
        
        /// <summary>
        /// When searching for planes on a given coordinate, limit how far the planes can be from the coordinate
        /// to be compliant
        /// </summary>
        private const int CoordinatesThreshold = 1;

        private HttpClient _client;
        private string _apiBaseUrlStatus;
        private string _apiBaseUrlArrival;
        private string _apiBaseUrlDeparture;
        private string _apiCredentials = "edumarcialmiw:servicioswebmiw2020";
        
        private IAirportWS _airportWs;
        private IWeatherWS _weatherWs;
        
        public PlaneWS()
        {
            // Client and weather API config
            _client = new HttpClient();
            _apiBaseUrlStatus = 
                $"https://{_apiCredentials}@opensky-network.org/api/states/all";
            _apiBaseUrlArrival = 
                $"https://{_apiCredentials}@opensky-network.org/api/flights/arrival";
            _apiBaseUrlDeparture = 
                $"https://{_apiCredentials}@opensky-network.org/api/flights/departure";
            
            // Airport service
            _airportWs = new AirportWS();
            
            // Weather service
            _weatherWs = new WeatherWS();
        }
        
        public Task<PlaneStatus> GetPlaneStatus(Plane plane)
        {
            return plane.Icao24 == null ? null : GetPlaneStatus(plane.Icao24);
        }

        public async Task<PlaneStatus> GetPlaneStatus(string planeCode)
        {
            if (planeCode == null)
                return null;
            return await GetPlaneStatus(planeCode, DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        }
        
        public async Task<PlaneStatus> GetPlaneStatus(string planeCode, long time)
        {
            PlaneStatus status = null;
            
            // 1. Ask API for plane status data
            HttpResponseMessage response = _client.GetAsync($"{_apiBaseUrlStatus}?icao24={planeCode}&time={time}")
                .GetAwaiter().GetResult();
            // 2. Insert JSON recovered data into a PlaneStatus object
            if (response.IsSuccessStatusCode)
            {
                dynamic dynStatus = JObject.Parse(await response.Content.ReadAsStringAsync());
                try
                {
                    status = new PlaneStatus(dynStatus.states[0]);
                }
                catch (Exception)
                {
                    status = null;
                }
            }

            return status;
        }

        public Task<IList<Plane>> GetPlanesByDeparture(Airport departureAirport, bool getStatus = false)
        {
            return departureAirport.Code == null ? null : GetPlanesByDeparture(departureAirport.Code, getStatus);
        }

        public Task<IList<Plane>> GetPlanesByDepartureDetail(Airport departureAirport, bool getStatusAndWeather = false)
        {
            return departureAirport.Code == null ? null : GetPlanesByDepartureDetail(departureAirport.Code, getStatusAndWeather);
        }

        public async Task<IList<Plane>> GetPlanesByDeparture(string departureAirport, bool getStatus = false)
        {
            List<Plane> planes = new List<Plane>();
            
            // 1. Ask API for plane status data
            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            HttpResponseMessage response = _client.GetAsync($"{_apiBaseUrlDeparture}?airport={departureAirport}" +
                                                            $"&begin={currentTime - HourDetectionLimit * 3600}" +
                                                            $"&end={currentTime}")
                .GetAwaiter().GetResult();
            
            // 2. Parse the JSON response into a list of Plane objects.
            if (response.IsSuccessStatusCode)
            {
                dynamic dynPlanes = JArray.Parse(await response.Content.ReadAsStringAsync());
                try
                {
                    /* Create the plane object */
                    foreach (dynamic planeJson in dynPlanes)
                    {
                        Plane plane = new Plane(planeJson);
                        if (getStatus)
                            plane.Status = await GetPlaneStatus(plane.Icao24, plane.DepartureTime);
                        planes.Add(plane);
                    }
                    return planes;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<IList<Plane>> GetPlanesByDepartureDetail(string departureAirport, bool getStatusAndWeather = false)
        {
            List<Plane> planes = new List<Plane>();
            
            // 1. Ask API for plane status data
            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            HttpResponseMessage response = _client.GetAsync($"{_apiBaseUrlDeparture}?airport={departureAirport}" +
                                                            $"&begin={currentTime - HourDetectionLimit * 3600}" +
                                                            $"&end={currentTime}")
                .GetAwaiter().GetResult();
            
            // 2. Parse the JSON response into a list of Plane objects.
            if (response.IsSuccessStatusCode)
            {
                dynamic dynPlanes = JArray.Parse(await response.Content.ReadAsStringAsync());
                int limit = 0;
                try
                {
                    /* Create the plane object AND also:
                        - Get the plane status
                        - Get the plane departure airport data
                        - Get the plane arrival airport data
                        - Get the weather conditions in the plane coordinates
                     */
                    foreach (dynamic planeJson in dynPlanes)
                    {
                        // Limit how many planes we track 
                        if (!getStatusAndWeather && limit >= PlanesLimitStandard || getStatusAndWeather && limit >= PlanesLimit)
                            break;
                        
                        Plane plane = new Plane(planeJson);
                        plane.DepartureAirport = await _airportWs.GetAirport(plane.DepartureAirportCode);
                        plane.ArrivalAirport = await _airportWs.GetAirport(plane.ArrivalAirportCode);
                        if (getStatusAndWeather)
                        {
                            plane.Status = await GetPlaneStatus(plane.Icao24);
                            if (plane.Status != null && plane.Status.Location != null)
                            { 
                                plane.Weather = await _weatherWs.GetWeatherByCoordinate(plane.Status.Location);
                            }
                            
                            if (plane.DepartureAirport != null)
                                plane.DepartureAirport.Weather =
                                    await _weatherWs.GetWeatherByCoordinate(plane.DepartureAirport.Location);
                            
                            if (plane.ArrivalAirport != null)
                                plane.ArrivalAirport.Weather =
                                    await _weatherWs.GetWeatherByCoordinate(plane.ArrivalAirport.Location);
                        }

                        if (!getStatusAndWeather || plane.Status != null)
                        {
                            planes.Add(plane);
                            limit++;
                        }
                    }
                    return planes.OrderBy(p => p.DepartureTime).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        public Task<IList<Plane>> GetPlanesByArrival(Airport arrivalAirport, bool getStatus = false)
        {
            return arrivalAirport.Code == null ? null : GetPlanesByArrival(arrivalAirport.Code, getStatus);
        }

        public Task<IList<Plane>> GetPlanesByArrivalDetail(Airport arrivalAirport, bool getStatusAndWeather = false)
        {
            return arrivalAirport.Code == null ? null : GetPlanesByArrivalDetail(arrivalAirport.Code, getStatusAndWeather);
        }

        public async Task<IList<Plane>> GetPlanesByArrival(string arrivalAirport,bool getStatus = false)
        {
            IList<Plane> planes = new List<Plane>();
            
            // 1. Ask API for plane status data
            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            HttpResponseMessage response = _client.GetAsync($"{_apiBaseUrlArrival}?airport={arrivalAirport}" +
                                                            $"&begin={currentTime - HourDetectionLimit * 3600}" +
                                                            $"&end={currentTime}")
                .GetAwaiter().GetResult();
            
            // 2. Parse the JSON response into a list of Plane objects.
            if (response.IsSuccessStatusCode)
            {
                dynamic dynPlanes = JArray.Parse(await response.Content.ReadAsStringAsync());
                try
                {
                    /* Create the plane object */
                    foreach (dynamic planeJson in dynPlanes)
                    {
                        Plane plane = new Plane(planeJson);
                        if (getStatus)
                            plane.Status = await GetPlaneStatus(plane.Icao24, plane.DepartureTime);
                        planes.Add(plane);
                    }
                    return planes;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<IList<Plane>> GetPlanesByArrivalDetail(string arrivalAirport, bool getStatusAndWeather = false)
        {
            List<Plane> planes = new List<Plane>();
            
            // 1. Ask API for plane status data
            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            HttpResponseMessage response = _client.GetAsync($"{_apiBaseUrlArrival}?airport={arrivalAirport}" +
                                                            $"&begin={currentTime - HourDetectionLimit * 3600}" +
                                                            $"&end={currentTime}")
                .GetAwaiter().GetResult();
            
            // 2. Parse the JSON response into a list of Plane objects.
            if (response.IsSuccessStatusCode)
            {
                dynamic dynPlanes = JArray.Parse(await response.Content.ReadAsStringAsync());
                int limit = 0;
                try
                {
                    /* Create the plane object AND also:
                        - Get the plane status
                        - Get the plane departure airport data
                        - Get the plane arrival airport data
                        - Get the weather conditions in the plane coordinates
                     */
                    foreach (dynamic planeJson in dynPlanes)
                    {
                        // Limit how many planes we track
                        if (!getStatusAndWeather && limit >= PlanesLimitStandard || getStatusAndWeather && limit >= PlanesLimit)
                            break;
                        
                        Plane plane = new Plane(planeJson);
                        plane.DepartureAirport = await _airportWs.GetAirport(plane.DepartureAirportCode);
                        plane.ArrivalAirport = await _airportWs.GetAirport(plane.ArrivalAirportCode);
                        if (getStatusAndWeather)
                        {
                            plane.Status = await GetPlaneStatus(plane.Icao24);
                            if (plane.Status != null && plane.Status.Location != null)
                            { 
                                plane.Weather = await _weatherWs.GetWeatherByCoordinate(plane.Status.Location);
                            }
                            
                            if (plane.DepartureAirport != null)
                                plane.DepartureAirport.Weather =
                                    await _weatherWs.GetWeatherByCoordinate(plane.DepartureAirport.Location);
                            
                            if (plane.ArrivalAirport != null)
                                plane.ArrivalAirport.Weather =
                                    await _weatherWs.GetWeatherByCoordinate(plane.ArrivalAirport.Location);
                        }
                        
                        planes.Add(plane);

                        limit++;
                    }
                    return planes.OrderBy(p => p.ArrivalTime).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        public Task<IList<Plane>> GetPlanesCloseToAirport(Airport airport)
        {
            return airport.Code == null ? null : GetPlanesCloseToAirport(airport.Code);
        }

        public async Task<IList<Plane>> GetPlanesCloseToAirport(string airport)
        {
            // 1. Get airport coordinates
            Coordinate coordinate = await _airportWs.GetAirportCoordinates(airport);
            if (coordinate == null)
                return null;
            
            // 2. Request planes that are in the area
            
            // - Plane states
            HttpResponseMessage response = _client.GetAsync($"{_apiBaseUrlStatus}?" +
                                                            $"lamin={(coordinate.Latitude - CoordinatesThreshold).ToString(CultureInfo.InvariantCulture)}&" +
                                                            $"lamax={(coordinate.Latitude + CoordinatesThreshold).ToString(CultureInfo.InvariantCulture)}&" +
                                                            $"lomin={(coordinate.Longitude - CoordinatesThreshold).ToString(CultureInfo.InvariantCulture)}&" +
                                                            $"lomax={(coordinate.Longitude + CoordinatesThreshold).ToString(CultureInfo.InvariantCulture)}")
                .GetAwaiter().GetResult();
            
            IList<Plane> planes = new List<Plane>();
            if (response.IsSuccessStatusCode)
            {
                dynamic dynStatus = JObject.Parse(await response.Content.ReadAsStringAsync());
                try
                {
                    foreach (dynamic statusJson in dynStatus.states)
                    {
                        planes.Add(new Plane {Status = new PlaneStatus(statusJson)});
                    }
                    
                    // Return the planes whose coordinates we have and that are flying (not on ground)
                    return planes.Where(p => p.Status.Location != null && !p.Status.OnGround).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return null;
        }
    }
}