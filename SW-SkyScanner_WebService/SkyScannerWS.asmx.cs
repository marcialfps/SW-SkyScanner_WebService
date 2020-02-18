using System.IO;
using System.Threading.Tasks;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using SW_SkyScanner_WebService.Services.Airports;
using SW_SkyScanner_WebService.Services.Airports.Model;

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

        public Security Security { set; get; }

        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public string Login()
        {
            if (Security != null &&
            Security.UserName != null && Security.UserName.Equals("WS-Security"))
                return "Authenticate User " + Security.UserName;
            return "Invalid User!!";
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
    }

    [XmlRoot(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
    public class Security : SoapHeader
    {
        public string UserName { set; get; }
        public string Password { set; get; }
    }
}
