using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
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
        public bool EditProfile(string username, string password, string airportCode)
        {
            throw new NotImplementedException();
        }
        
        [WebMethod]
        public bool DeleteAccount(string username)
        {
            throw new NotImplementedException();
        }
        
        [WebMethod]
        public List<Plane> GetPlanesByDepartureAirport(string airportCode)
        {
            throw new NotImplementedException();
        }
        
        [WebMethod]
        public List<Plane> GetPlanesByArrivalAirport(string airportCode)
        {
            throw new NotImplementedException();
        }
        
        [WebMethod]
        public List<Plane> GetPlanesCloseToAirport(string airportCode)
        {
            Airport airport = _airportWs.GetAirport(airportCode).GetAwaiter().GetResult();
            if (airport == null)
                return null;
            return GetPlanesByLocation(airport.Location);
        }
        
        

        [WebMethod]
        public string GetAirportExample(string icao24Code)
        {
            AirportWS airportWs = new AirportWS();
            Airport airport = airportWs.GetAirport(icao24Code).GetAwaiter().GetResult();
            if (airport == null)
                return "No airports found!";
            
            return airport.Location.Latitude + " - " + airport.Location.Longitude;
        }
        
        [WebMethod]
        public string GetWeatherExample(double latitude, double longitude)
        {
            Coordinate coordinate = new Coordinate(latitude, longitude);
            Weather weather = _weatherWs.GetWeatherByCoordinate(coordinate).GetAwaiter().GetResult();
            if (weather == null)
                return "Could not retrieve weather";
            return weather.Description;
        }
        
        [WebMethod]
        public string GetWeatherAirportExample(string airportCode)
        {
            Weather weather = _weatherWs.GetWeatherByAirport(airportCode).GetAwaiter().GetResult();
            if (weather == null)
                return "Could not retrieve weather";
            return weather.Humidity.ToString();
        }
        
        [WebMethod]
        public string GetForecastAirportExample(string airportCode)
        {
            Weather weather = _weatherWs.GetForecastByAirport(airportCode, 0).GetAwaiter().GetResult();
            if (weather == null)
                return "Could not retrieve weather";
            
            return weather.Temperature.ToString(CultureInfo.InvariantCulture);
        }
        
        [WebMethod]
        public string GetPlanesByDepartureExample(string airportCode)
        {
            IList<Plane> planes = _planeWs.GetPlanesByDeparture(airportCode).GetAwaiter().GetResult();
            if (planes == null)
                return "Could not retrieve the list of planes departing from " + airportCode;

            return planes.Count.ToString();
        }
        
        // Non Web Methods -- aux methods
        
        public List<Plane> GetPlanesByLocation(Coordinate coordinate)
        {
            throw new NotImplementedException();
        }
    }

/*    [XmlRoot(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
    public class Security : SoapHeader
    {
        public string UserName { set; get; }
        public string Password { set; get; }
    }*/
}
