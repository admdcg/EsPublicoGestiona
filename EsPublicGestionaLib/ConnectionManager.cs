using Chilkat;
using EsPublicGestionaLib.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Collections.Specialized.BitVector32;
using EsPublicGestionaLib.Models;

namespace EsPublicGestionaLib
{
    public class ConnectionManager
    {
        private ConfigSettings ConfigSettings;
        public ConnectionManager(ConfigSettings configSettings)
        {
            ConfigSettings = configSettings;
        }

        public solicitarNuevasFacturasResponse SolicitarNuevasFacturas(string oficinaContable)
        {            
            var content = GetSoapRequestWithSignXml(ConfigSettings.CertificatePath, ConfigSettings.CertificatePassword, "solicitarNuevasFacturas", 
                new Dictionary<string, object> { { "oficinaContable", oficinaContable } } );
            var response = HttpPost(ConfigSettings.EndpointUrl, content);
            var solicitarNuevasFacturasResponse = ProcesaRespuesta<solicitarNuevasFacturasResponse>(response);
            return solicitarNuevasFacturasResponse;
        }
        public descargarFacturaResponse DescargarFactura(string numeroRegistro)
        {   
            var content = GetSoapRequestWithSignXml(ConfigSettings.CertificatePath, ConfigSettings.CertificatePassword,
                "descargarFactura", new Dictionary<string, object> { { "numeroRegistro", numeroRegistro } });
            var response = HttpPost(ConfigSettings.EndpointUrl, content);
            var descargarFacturaResponse = ProcesaRespuesta<descargarFacturaResponse>(response);
            return descargarFacturaResponse;
        }
        public consultarFacturaResponse ConsultarFactura(string numeroRegistro)
        {
            var content = GetSoapRequestWithSignXml(ConfigSettings.CertificatePath, ConfigSettings.CertificatePassword,
                "consultarFactura", new Dictionary<string, object> { { "numeroRegistro", numeroRegistro } });
            var response = HttpPost(ConfigSettings.EndpointUrl, content);
            var descargarFacturaResponse = ProcesaRespuesta<consultarFacturaResponse>(response);
            return descargarFacturaResponse;
        }
        public confirmarDescargaFacturaResponse ConfirmarDescargaFactura(string oficinaContable, string numeroRegistro, string codigoRCF)
        {
            var content = GetSoapRequestWithSignXml(ConfigSettings.CertificatePath, ConfigSettings.CertificatePassword, 
                "confirmarDescargaFactura", new Dictionary<string, object> 
                { 
                    { "oficinaContable", oficinaContable },
                    { "numeroRegistro", numeroRegistro },
                    { "codigoRCF", codigoRCF }
                });
            var response = HttpPost(ConfigSettings.EndpointUrl, content);
            var solicitarNuevasFacturasResponse = ProcesaRespuesta<confirmarDescargaFacturaResponse>(response);
            return solicitarNuevasFacturasResponse;
        }
        public cambiarEstadoFacturaResponse CambiarEstadoFactura(string oficinaContable, string numeroRegistro, string codigo, string comentarios)
        {
            var content = GetSoapRequestWithSignXml(ConfigSettings.CertificatePath, ConfigSettings.CertificatePassword, 
                "cambiarEstadoFactura", new Dictionary<string, object>
                {
                    { "oficinaContable", oficinaContable },
                    { "numeroRegistro", numeroRegistro },
                    { "codigo", codigo },
                    { "comentarios", comentarios }
                });
            var response = HttpPost(ConfigSettings.EndpointUrl, content);
            var solicitarNuevasFacturasResponse = ProcesaRespuesta<cambiarEstadoFacturaResponse>(response);
            return solicitarNuevasFacturasResponse;
        }
        public consultarListadoFacturasResponse ConsultarListadoFacturas(List<string> listadoFacturas)
        {            
            var parameterValue = new Dictionary<string, List<string>> { { "registro", listadoFacturas } };
            var parameter = new Dictionary<string, object> { { "listadoFacturas", parameterValue } };
            
            var content = GetSoapRequestWithSignXml(ConfigSettings.CertificatePath, ConfigSettings.CertificatePassword,
                "consultarListadoFacturas", parameter);
            var response = HttpPost(ConfigSettings.EndpointUrl, content);
            var solicitarNuevasFacturasResponse = ProcesaRespuesta<consultarListadoFacturasResponse>(response);
            return solicitarNuevasFacturasResponse;
        }


        private static string HttpPost(String endpointUrl, String content)

        {            
            //var url = "http://econtabilidad-pre.sedelectronica.es/conta2rlf/services/FacturaSRCFWebServiceProxyService/";
            var client = new HttpClient();            
            client.BaseAddress = new Uri(endpointUrl);
            var response = HttpClienteManager.Action(HttpClienteManager.HttpVerbs.POST, client, content, "text/xml");
            return response;
        }

        private static T ProcesaRespuesta<T>(String response, string extraNode = "")
        {
            var xmlResponse = new XmlDocument();
            xmlResponse.LoadXml(response);

            // Crear un XmlNamespaceManager para manejar los namespaces de la respuesta
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlResponse.NameTable);
            namespaceManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");

            // Usar XPath para obtener el primer nodo hijo de soap:Body
            XmlNode firstChildNode = xmlResponse.SelectSingleNode($"/soap:Envelope/soap:Body/*[1]{extraNode}", namespaceManager);
            if (firstChildNode == null)
            {
                throw new Exception("No se encontró la respuesta en el Body.");
            }
            var shortResponse = new XmlDocument();
            shortResponse.LoadXml(firstChildNode.OuterXml);

            var responseClear = shortResponse.RemoveAllNamespaces();
            var objectResponse = SerializationUtils.XmlDeserializeFromString<T>(responseClear.OuterXml);
            return objectResponse;
        }
        private static string GetSoapRequestWithSignXml(string certificatePath, string certificatePassword, string action, Dictionary<string, object> parameters)
        {
            //UnlockChilkat();
            bool success = true;
            // Create the XML to be signed...
            Chilkat.Xml xmlToSign = new Chilkat.Xml();
            xmlToSign.Tag = "soapenv:Envelope";
            xmlToSign.AddAttribute("xmlns:soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            xmlToSign.AddAttribute("xmlns:web", "https://webservice.face.gob.es");
            xmlToSign.AddAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
            xmlToSign.AddAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            xmlToSign.UpdateAttrAt("soapenv:Header|wsse:Security", true, "xmlns:wsse", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            xmlToSign.UpdateAttrAt("soapenv:Header|wsse:Security", true, "xmlns:wsu", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
            xmlToSign.UpdateAttrAt("soapenv:Body", true, "wsu:Id", "id-B88764AFBD17E5C06917310456194329");
            xmlToSign.UpdateAttrAt("soapenv:Body", true, "xmlns:wsu", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");

            xmlToSign.UpdateAttrAt($"soapenv:Body|web:{action}", true, "soapenv:encodingStyle", "http://schemas.xmlsoap.org/soap/encoding/");
            foreach(var pairValue in parameters)
            {
                if (pairValue.Value is string)
                {
                    string value = (string)pairValue.Value;
                    xmlToSign.UpdateAttrAt($"soapenv:Body|web:{action}|{pairValue.Key}", true, "xsi:type", "xsd:string");
                    xmlToSign.UpdateChildContent($"soapenv:Body|web:{action}|{pairValue.Key}", value);
                }
                else if (pairValue.Value is Dictionary<string, List<string>>)
                {
                    xmlToSign.UpdateAttrAt($"soapenv:Body|web:{action}|{pairValue.Key}", true, "xsi:type", "soapenc:Array");
                    var arrayValues = (Dictionary<string, List<string>>)pairValue.Value;
                    //Suponemos que sólo viene un array
                    var arrayValue = arrayValues.FirstOrDefault();
                    var contador = 0;
                    foreach (var value in arrayValue.Value)
                    {
                        xmlToSign.UpdateAttrAt($"soapenv:Body|web:{action}|{pairValue.Key}|{arrayValue.Key}", true, null, null);
                        if (contador > 0)
                        {
                            xmlToSign.UpdateChildContent($"soapenv:Body|web:{action}|{pairValue.Key}|{arrayValue.Key}[{contador}]", value);
                        }
                        else
                        {
                            xmlToSign.UpdateChildContent($"soapenv:Body|web:{action}|{pairValue.Key}|{arrayValue.Key}", value);
                        }
                        contador++;
                    }
                }
            }
            Chilkat.XmlDSigGen gen = new Chilkat.XmlDSigGen();
            gen.SigLocation = "soapenv:Envelope|soapenv:Header|wsse:Security";
            gen.SigLocationMod = 0;
            gen.SigId = "SIG-B88764AFBD17E5C069173104561944110";
            gen.SigNamespacePrefix = "ds";
            gen.SigNamespaceUri = "http://www.w3.org/2000/09/xmldsig#";
            gen.SignedInfoPrefixList = "soapenv web xsd xsi";
            gen.IncNamespacePrefix = "ec";
            gen.IncNamespaceUri = "http://www.w3.org/2001/10/xml-exc-c14n#";
            gen.SignedInfoCanonAlg = "EXCL_C14N";
            gen.SignedInfoDigestMethod = "sha1";

            // Set the KeyInfoId before adding references..
            gen.KeyInfoId = "KI-B88764AFBD17E5C06917310456194307";

            // -------- Reference 1 --------
            gen.AddSameDocRef("id-B88764AFBD17E5C06917310456194329", "sha1", "EXCL_C14N", "web xsd xsi", "");

            // Provide a certificate + private key. (PFX password is test123)
            Chilkat.Cert cert = new Chilkat.Cert();
            success = cert.LoadPfxFile(certificatePath, certificatePassword);
            if (success != true)
            {
                Debug.WriteLine(cert.LastErrorText);
                throw new Exception(cert.LastErrorText);
            }

            gen.SetX509Cert(cert, true);

            gen.KeyInfoType = "Custom";

            // Create the custom KeyInfo XML..
            Chilkat.Xml xmlCustomKeyInfo = new Chilkat.Xml();
            xmlCustomKeyInfo.Tag = "wsse:SecurityTokenReference";
            xmlCustomKeyInfo.AddAttribute("wsu:Id", "STR-B88764AFBD17E5C06917310456194308");
            xmlCustomKeyInfo.UpdateChildContent("ds:X509Data|ds:X509IssuerSerial|ds:X509IssuerName", "1.2.840.113549.1.9.1=#161c736f706f72746540696e67656e69756d76656e74757265732e636f6d,CN=Carlos Mendez Socas,OU=Productos,O=Ingenium Ventures,L=Santa Cruz de Tenerife,ST=Santa Cruz de Tenerife,C=ES");
            xmlCustomKeyInfo.UpdateChildContent("ds:X509Data|ds:X509IssuerSerial|ds:X509SerialNumber", "172665430729509100814651526493747771790854985633");

            xmlCustomKeyInfo.EmitXmlDecl = false;
            gen.CustomKeyInfoXml = xmlCustomKeyInfo.GetXml();

            // Load XML to be signed...
            Chilkat.StringBuilder sbXml = new Chilkat.StringBuilder();
            xmlToSign.GetXmlSb(sbXml);

            gen.Behaviors = "CompactSignedXml";

            // Sign the XML...
            success = gen.CreateXmlDSigSb(sbXml);
            if (success != true)
            {
                Debug.WriteLine(gen.LastErrorText);
                throw new Exception(cert.LastErrorText);
            }

            // -----------------------------------------------

            // Save the signed XML to a file.
            //success = sbXml.WriteFile("signedXml.xml", "utf-8", false);
            var content = sbXml.GetAsString();
            Debug.WriteLine(content);
            return content;
        }
        private static void UnlockChilkat()
        {
            // This example assumes the Chilkat API to have been previously unlocked.
            // See Global Unlock Sample for sample code.

            // The Chilkat API can be unlocked for a fully-functional 30-day trial by passing any
            // string to the UnlockBundle method.  A program can unlock once at the start. Once unlocked,
            // all subsequently instantiated objects are created in the unlocked state. 
            // 
            // After licensing Chilkat, replace the "Anything for 30-day trial" with the purchased unlock code.
            // To verify the purchased unlock code was recognized, examine the contents of the LastErrorText
            // property after unlocking.  For example:
            Global glob;
            bool success;
            glob = new Chilkat.Global();
            success = glob.UnlockBundle("NGENLB.CB1032023_E95VEAey9FBU");
            if (success != true)
            {
                Debug.WriteLine(glob.LastErrorText);
                return;
            }

            int status = glob.UnlockStatus;
            if (status == 2)
            {
                Debug.WriteLine("Unlocked using purchased unlock code.");
            }
            else
            {
                Debug.WriteLine("Unlocked in trial mode.");
            }
            // The LastErrorText can be examined in the success case to see if it was unlocked in
            // trial more, or with a purchased unlock code.
            Debug.WriteLine(glob.LastErrorText);
        }
    }
}
