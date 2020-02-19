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
    /// Descripción breve de SkyScannerWS
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
        public User AddUser(string username, string password, string airportCode)
        {
            throw new NotImplementedException();
        }
        
        [WebMethod]
        public User GetUser(string username)
        {
            throw new NotImplementedException();
        }
        
        [WebMethod]
        public User EditUser(string username, string password, string airportCode)
        {
            throw new NotImplementedException();
        }
        
        [WebMethod]
        public User DeleteUser(string username)
        {
            throw new NotImplementedException();
        }

        [WebMethod]
        public Airport GetAirportByCode(string airportCode)
        {
            AirportWS airportWs = new AirportWS();
            Airport airport = airportWs.GetAirport(airportCode).GetAwaiter().GetResult();
            if (airport == null)
            {
                HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }
            
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.Accepted;
            return airport;
        }
        
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
