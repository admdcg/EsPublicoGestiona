using EsPublicGestionaLib;
using EsPublicGestionaLib.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace EsPublicGestionaCli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var settings = new ConfigSettings
            {
                EndpointUrl = "http://econtabilidad-pre.sedelectronica.es/conta2rlf/services/FacturaSRCFWebServiceProxyService/",
                CertificatePath = "Certificado.pfx",
                CertificatePassword = "2222",

            };
            var manager = new ConnectionManager(settings);
            //ConsultarFacturasYDescargarDocumentos(manager);
            //CambiarEstado(manager, "A05040312-FACT-2024-2", "A05040312");
            //ConsultarFactura(manager, "A05040312-FACT-2024-2");
            ConsultarListadoFacturas(manager);

        }

        private static void ConsultarFacturasYDescargarDocumentos(ConnectionManager manager)
        {
            var solicitarNuevasFacturasResponse = manager.SolicitarNuevasFacturas("A05040312");
            if (solicitarNuevasFacturasResponse.@return.resultado.codigo == 0)
            {
                for (int i = 0; i < solicitarNuevasFacturasResponse.@return.facturas.Count(); i++)
                {
                    var factura = solicitarNuevasFacturasResponse.@return.facturas[i];
                    Console.WriteLine($"Número de registro {i}.: {factura.numeroRegistro} - {factura.fechaHoraRegistro}");
                }
                var selected = Convert.ToInt32(Console.ReadLine());
                var facturaSelected = solicitarNuevasFacturasResponse.@return.facturas[selected];
                var numeroRegisto = facturaSelected.numeroRegistro;
                var oficinaContable = facturaSelected.oficinaContable;
                var descargarFacturaResponse = manager.DescargarFactura(numeroRegisto);
                if (descargarFacturaResponse.@return.resultado.codigo == 0)
                {
                    for (int i = 0; i < descargarFacturaResponse.@return.factura.anexos.Count(); i++)
                    {
                        var anexo = descargarFacturaResponse.@return.factura.anexos[i];
                        Console.WriteLine($"anexo {i}.: {anexo.nombre} - {anexo.mime}");
                    }
                    selected = Convert.ToInt32(Console.ReadLine());
                    var anexoSelected = descargarFacturaResponse.@return.factura.anexos[selected];
                    if (anexoSelected.mime != "application/pdf")
                    {
                        throw new Exception("Sólo pdfs porfi.");
                    }
                    var base64BinaryStr = anexoSelected?.anexo;
                    var filename = anexoSelected?.nombre + ".pdf";
                    Converter.ConverBase64ToFile(base64BinaryStr, filename);
                    System.Diagnostics.Process.Start(filename);
                }                
            }
        }
        private static void ConsultarListadoFacturas(ConnectionManager manager)
        {
            var consultarFacturaRespose = manager.ConsultarListadoFacturas(new List<string> { "A05040312-FACT-2024-2", "A05040312-FACT-2024-3" });
            if (consultarFacturaRespose.@return.resultado.codigo == 0)
            {
                foreach(var factura in consultarFacturaRespose.@return.facturas)
                {
                    Console.WriteLine($"Factura: " + factura.factura.numeroRegistro);
                    Console.WriteLine($"Tramitación: " + factura.factura.tramitacion.descripcion);
                    Console.WriteLine($"Anulación: " + factura.factura.anulacion.descripcion);
                }
                
            }
            Console.ReadLine();
        }

        private static void ConsultarFactura(ConnectionManager manager, string numeroRegisto)
        {
            var consultarFacturaRespose = manager.ConsultarFactura(numeroRegisto);
            if (consultarFacturaRespose.@return.resultado.codigo == 0)
            {
                Console.WriteLine($"Tramitación: " + consultarFacturaRespose.@return.factura.tramitacion.descripcion);
                Console.WriteLine($"Anulación: " + consultarFacturaRespose.@return.factura.anulacion.descripcion);
            }
            Console.ReadLine();
        }

        private static void CambiarEstado(ConnectionManager manager, string numeroRegisto, string oficinaContable)
        {
            var consultarFacturaResponsee = manager.CambiarEstadoFactura(oficinaContable, numeroRegisto, "1200", "Pruebas desde conexion WebServices.");
            Debug.WriteLine(consultarFacturaResponsee.@return.resultado.descripcion);
        }
    }
}
