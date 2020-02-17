using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

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
    }

    [XmlRoot(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
    public class Security : SoapHeader
    {
        public string UserName { set; get; }
        public string Password { set; get; }
    }
}
