using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using SW_SkyScanner_WebService.Services.Airports;
using SW_SkyScanner_WebService.Services.Airports.Model;
using SW_SkyScanner_WebService.Services.Planes;
using SW_SkyScanner_WebService.Services.Planes.Model;
using SW_SkyScanner_WebService.Services.Users;
using SW_SkyScanner_WebService.Services.Users.Model;
using SW_SkyScanner_WebService.Services.Weather;
using SW_SkyScanner_WebService.Services.Weather.Model;

namespace SW_SkyScanner_WebService
{
    /// <summary>
    /// SkyScannerWS offers functionality to track planes and get planes and airports information (including
    /// coordinates, weather, etc.)
    /// </summary>
    [WebService(Namespace = "http://ws.skyscanner/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class SkyScannerWS : WebService
    {
        private IUserWs _userWs = new UserWS();
        private IPlaneWs _planeWs = new PlaneWS();
        private IAirportWS _airportWs = new AirportWS();
        private IWeatherWS _weatherWs = new WeatherWS();

/*        public Security Security { set; get; }

        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public string Login()
        {
            if (Security != null &&
            Security.UserName != null && Security.UserName.Equals("WS-Security"))
                return "Authenticate User " + Security.UserName;
            return "Invalid User!!";
        }*/

/*        [WebMethod]
        public User Register(string username, string password, string airportCode)
        {
            throw new NotImplementedException();
        }*/

/*        [WebMethod]
        public User Login(string username, string password)
        {
            throw new NotImplementedException();
        }*/

        [WebMethod]
        public User GetUser(string username)
        {
            User user = _userWs.GetUser(username).GetAwaiter().GetResult();
            if (user == null)
            {
                HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }
            
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Accepted;
            return user;
        }
        
        [WebMethod]
        public User Login(string username, string password)
        {
            // TODO
            User user = _userWs.GetUser(username, password).GetAwaiter().GetResult();
            if (user == null)
            {
                HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                return null;
            }
            
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Accepted;
            return user;
        }
        
        [WebMethod]
        public User AddUser(string username, string name, string surname, string password, string mail, string airport)
        {
            User user = new User
            {
                Username = username,
                Name = name,
                Surname = surname,
                Password = password,
                Mail = mail,
                Airport = airport
            };
            user = _userWs.CreateUser(user).GetAwaiter().GetResult();
            if (user == null)
            {
                HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Conflict;
                return null;
            }
            
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Created;
            return user;
        }
        
        
        [WebMethod]
        public User EditUser(string username, string name, string surname, string password, string mail, string airport)
        {
            User user = new User
            {
                Username = username,
                Name = name,
                Surname = surname,
                Password = password,
                Mail = mail,
                Airport = airport
            };
            user = _userWs.UpdateUser(user).GetAwaiter().GetResult();
            if (user == null)
            {
                HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }
            
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.OK;
            return user;
        }
        
        [WebMethod]
        public User DeleteUser(string username, string password)
        {
            User user = _userWs.DeleteUser(username, password).GetAwaiter().GetResult();
            if (user == null)
            {
                HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                return null;
            }
            
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.OK;
            return user;
        }

        /// <summary>
        /// Contacts the "Airport info" API (https://rapidapi.com/Active-api/api/airport-info/details) and retrieves
        /// the data of an airport
        /// </summary>
        /// <param name="airportCode">ICAO24 code of the desired airport</param>
        /// <returns>A serialized Airport object (<see cref="Airport"/>) if an airport was found (code 202)
        /// or null if no airport was found (code 404)</returns>
        [WebMethod]
        public Airport GetAirportByCode(string airportCode)
        {
            Airport airport = _airportWs.GetAirport(airportCode).GetAwaiter().GetResult();
            if (airport == null)
            {
                HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }
            
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Accepted;
            return airport;
        }
        
        /// <summary>
        /// Contacts the "Airport info" API (https://rapidapi.com/Active-api/api/airport-info/details) and retrieves
        /// the coordinates of an airport. Works in a similar way than <see cref="GetAirportByCode"/>
        /// </summary>
        [WebMethod]
        public Coordinate GetAirportCoordinatesByCode(string airportCode)
        {
            Coordinate airportCoordinate = _airportWs.GetAirportCoordinates(airportCode).GetAwaiter().GetResult();
            if (airportCoordinate == null)
            {
                HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }
            
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Accepted;
            return airportCoordinate;
        }
        
        /// <summary>
        /// Contacts the "OpenWeatherMap" API (https://openweathermap.org/api) and retrieves
        /// the current weather in the given coordinate
        /// </summary>
        /// <param name="latitude">Latitude of the coordinate</param>
        /// <param name="longitude">Longitude of the coordinate</param>
        /// <returns>A serialized Weather object (<see cref="Weather"/>) if the weather was found (code 202)
        /// or null if the coordinates were wrong or the weather was not found (code 404)</returns>
        [WebMethod]
        public Weather GetWeatherByCoordinate(double latitude, double longitude)
        {
            Coordinate coordinate = new Coordinate(latitude, longitude);
            Weather weather = _weatherWs.GetWeatherByCoordinate(coordinate).GetAwaiter().GetResult();
            if (weather == null)
            {
                HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Accepted;
            return weather;
        }
        
        /// <summary>
        /// Contacts the "OpenWeatherMap" API (https://openweathermap.org/api) and retrieves
        /// the current weather in the given airport. Works in a similar way than <see cref="GetWeatherByCoordinate"/>
        /// </summary>
        [WebMethod]
        public Weather GetWeatherByAirport(string airportCode)
        {
            Weather weather = _weatherWs.GetWeatherByAirport(airportCode).GetAwaiter().GetResult();
            if (weather == null)
            {
                HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }
            
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Accepted;
            return weather;
        }
        
        /// <summary>
        /// Contacts the "OpenWeatherMap" API (https://openweathermap.org/api) and retrieves
        /// the predicted weather for the given airport and moment in time
        /// </summary>
        /// <param name="airportCode">Airport for which the weather is requested</param>
        /// <param name="time">UNIX timestamp for which the weather is requested</param>
        /// <returns>A serialized Weather object (<see cref="Weather"/>) if the weather was found (code 202)
        /// or null if the coordinates were wrong or the weather was not found (code 404)</returns>
        [WebMethod]
        public Weather GetWeatherForecastByAirport(string airportCode, int time)
        {
            Weather weather = _weatherWs.GetForecastByAirport(airportCode, time).GetAwaiter().GetResult();
            if (weather == null)
            {
                HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }
            
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Accepted;
            return weather;
        }
        
        /// <summary>
        /// Contacts the "OpenSky" API (https://opensky-network.org/apidoc/rest.html#) and retrieves
        /// the current status of a plane
        /// </summary>
        /// <param name="planeCode">ICAO24 code of the desired plane</param>
        /// <returns>A serialized PlaneStatus object (<see cref="PlaneStatus"/>) if the status was found (code 202)
        /// or null if no status was found (code 404)</returns>
        [WebMethod]
        public PlaneStatus GetPlaneStatusByCode(string planeCode)
        {
            PlaneStatus status = _planeWs.GetPlaneStatus(planeCode).GetAwaiter().GetResult();
            if (status == null)
            {
                HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }
            
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Accepted;
            return status;
        }
        
        /// <summary>
        /// Contacts the "OpenSky" API (https://opensky-network.org/apidoc/rest.html#) and retrieves
        /// the planes that departed from the given airport in the last 24 hours. The planes returned contain minimal
        /// information (no status, no weather condition and only the codes of their related airport)
        /// </summary>
        /// <param name="departureAirportCode">ICAO24 code of the departure airport</param>
        /// <returns>A serialized list of Plane objects (<see cref="Plane"/>) if any plane was found (code 202)
        /// or null if no plane was found (code 404)</returns>
        [WebMethod]
        public List<Plane> GetPlanesByDeparture(string departureAirportCode)
        {
            IList<Plane> planes = _planeWs.GetPlanesByDeparture(departureAirportCode).GetAwaiter().GetResult();
            if (planes == null)
            {
                HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }
            
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Accepted;
            return planes.ToList();
        }

        /// <summary>
        /// Contacts the "OpenSky" API (https://opensky-network.org/apidoc/rest.html#) and retrieves
        /// the planes that departed from the given airport in the last 24 hours. The planes returned contain all the
        /// details of their departure and arrival airports. This is a VERY SLOW method.
        /// </summary>
        /// <param name="departureAirportCode">ICAO24 code of the departure airport</param>
        /// <param name="getStatusAndWeather">True if the returned planes should also contain their current status
        /// and the weather conditions on the plane location and on the related airports.
        /// </param>
        /// <returns>A serialized list of Plane objects (<see cref="Plane"/>) if any plane was found (code 202)
        /// or null if no plane was found (code 404)</returns>
        [WebMethod]
        public List<Plane> GetPlanesByDepartureDetail(string departureAirportCode, bool getStatusAndWeather)
        {
            IList<Plane> planes = _planeWs.GetPlanesByDepartureDetail(departureAirportCode, getStatusAndWeather).GetAwaiter().GetResult();
            if (planes == null)
            {
                HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }
            
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Accepted;
            return planes.ToList();
        }
        
        /// <summary>
        /// Contacts the "OpenSky" API (https://opensky-network.org/apidoc/rest.html#) and retrieves
        /// the planes that were active in the last 24 hours that arrive in the given airport.
        /// The planes returned contain minimal information (no status, no weather condition and
        /// only the codes of their related airport)
        /// </summary>
        /// <param name="arrivalAirportCode">ICAO24 code of the arrival airport</param>
        /// <returns>A serialized list of Plane objects (<see cref="Plane"/>) if any plane was found (code 202)
        /// or null if no plane was found (code 404)</returns>
        [WebMethod]
        public List<Plane> GetPlanesByArrival(string arrivalAirportCode)
        {
            IList<Plane> planes = _planeWs.GetPlanesByArrival(arrivalAirportCode).GetAwaiter().GetResult();
            if (planes == null)
            {
                HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }

            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Accepted;
            return planes.ToList();
        }
        
        /// <summary>
        /// Contacts the "OpenSky" API (https://opensky-network.org/apidoc/rest.html#) and retrieves
        /// the planes that were active in the last 24 hours that arrive in the given airport.
        /// The planes returned contain all the details of their departure and arrival airports.
        /// This is a VERY SLOW method.
        /// </summary>
        /// <param name="arrivalAirportCode">ICAO24 code of the arrival airport</param>
        /// <param name="getStatusAndWeather">True if the returned planes should also contain their current status
        /// and the weather conditions on the plane location and on the related airports.
        /// </param>
        /// <returns>A serialized list of Plane objects (<see cref="Plane"/>) if any plane was found (code 202)
        /// or null if no plane was found (code 404)</returns>
        [WebMethod]
        public List<Plane> GetPlanesByArrivalDetail(string arrivalAirportCode, bool getStatusAndWeather)
        {
            IList<Plane> planes = _planeWs.GetPlanesByArrivalDetail(arrivalAirportCode, getStatusAndWeather).GetAwaiter().GetResult();
            if (planes == null)
            {
                HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }

            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Accepted;
            return planes.ToList();
        }
        
        /// <summary>
        /// Contacts the "OpenSky" API (https://opensky-network.org/apidoc/rest.html#) and retrieves
        /// the planes that are active and close to the given airport. The planes returned only have their
        /// status available.
        /// </summary>
        /// <param name="airportCode">ICAO24 code of the airport</param>
        /// <returns>A serialized list of Plane objects (<see cref="Plane"/>) if any plane was found (code 202)
        /// or null if no plane was found (code 404)</returns>
        [WebMethod]
        public List<Plane> GetPlanesCloseToAirport(string airportCode)
        {
            IList<Plane> planes = _planeWs.GetPlanesCloseToAirport(airportCode).GetAwaiter().GetResult();
            if (planes == null)
            {
                HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }

            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Accepted;
            return planes.ToList();
        }
    }

/*    [XmlRoot(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
    public class Security : SoapHeader
    {
        public string UserName { set; get; }
        public string Password { set; get; }
    }*/
}
