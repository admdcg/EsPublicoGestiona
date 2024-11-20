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
using FACeLib.ServiceReference1;
using System.ServiceModel;

namespace FACeLib
{
    public class ConnectionManager
    {
        private ConfigSettings ConfigSettings;
        public ConnectionManager(ConfigSettings configSettings)
        {
            ConfigSettings = configSettings;
        }

        public SolicitarNuevasFacturasResponse SolicitarNuevasFacturas(string oficinaContable)
        {
            var client = GetService();
            var response = client.solicitarNuevasFacturas(oficinaContable);
            return response;
        }
        public DescargarFacturaResponse DescargarFactura(string numeroRegistro)
        {
            var client = GetService();
            var response = client.descargarFactura(numeroRegistro);
            return response;
        }
        public ConsultarFacturaResponse ConsultarFactura(string numeroRegistro)
        {
            var client = GetService();
            var response = client.consultarFactura(numeroRegistro);
            return response;
        }
        public ConfirmarDescargaFacturaResponse ConfirmarDescargaFactura(string oficinaContable, string numeroRegistro, string codigoRCF)
        {
            var client = GetService();
            var response = client.confirmarDescargaFactura(oficinaContable, numeroRegistro, codigoRCF);
            return response;
        }
        public CambiarEstadoFacturaResponse CambiarEstadoFactura(string oficinaContable, string numeroRegistro, string codigo, string comentarios)
        {
            var client = GetService();
            var response = client.cambiarEstadoFactura(oficinaContable, numeroRegistro, codigo, comentarios);
            return response;
        }
        public ConsultarListadoFacturasResponse ConsultarListadoFacturas(List<string> listadoFacturas)
        {
            var client = GetService();
            var response = client.consultarListadoFacturas(listadoFacturas.ToArray());
            return response;
        }
        private FacturaSRCFWebServiceProxyPortClient GetService()
        {
            // Crear el EndpointAddress
            var endpointAddress = new EndpointAddress(new Uri(ConfigSettings.EndpointUrl));

            // Configurar el binding (como en el ejemplo previo)
            var binding = new BasicHttpBinding(); // o CustomBinding si es necesario

            // Crear el cliente del servicio
            var client = new FacturaSRCFWebServiceProxyPortClient(binding, endpointAddress);

            // Cargar el certificado del cliente            
            //var clientCertificate = new X509Certificate2(File.ReadAllBytes($"Certificado.pfx"), "2222");

            // Agregar el comportamiento personalizado
            client.Endpoint.EndpointBehaviors.Add(new InspectorBehavior(ConfigSettings.CertificatePath, ConfigSettings.CertificatePassword));

            return client;
        }
    }
}
