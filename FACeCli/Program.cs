using FACeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FACeCli
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
            ConsultarFacturasYDescargarDocumentos(manager);
            //CambiarEstado(manager, "A05040312-FACT-2024-2", "A05040312");
            //ConsultarFactura(manager, "A05040312-FACT-2024-2");
            //ConsultarListadoFacturas(manager);

        }

        private static void ConsultarFacturasYDescargarDocumentos(ConnectionManager manager)
        {
            var solicitarNuevasFacturasResponse = manager.SolicitarNuevasFacturas("A05040312");
            if (solicitarNuevasFacturasResponse.resultado.codigo == "0")
            {
                for (int i = 0; i < solicitarNuevasFacturasResponse.facturas.Count(); i++)
                {
                    var factura = solicitarNuevasFacturasResponse.facturas[i];
                    Console.WriteLine($"Número de registro {i}.: {factura.numeroRegistro} - {factura.fechaHoraRegistro}");
                }
                var selected = Convert.ToInt32(Console.ReadLine());
                var facturaSelected = solicitarNuevasFacturasResponse.facturas[selected];
                var numeroRegisto = facturaSelected.numeroRegistro;
                var oficinaContable = facturaSelected.oficinaContable;
                var descargarFacturaResponse = manager.DescargarFactura(numeroRegisto);
                if (descargarFacturaResponse.resultado.codigo == "0")
                {
                    for (int i = 0; i < descargarFacturaResponse.factura.anexos.Count(); i++)
                    {
                        var anexo = descargarFacturaResponse.factura.anexos[i];
                        Console.WriteLine($"anexo {i}.: {anexo.nombre} - {anexo.mime}");
                    }
                    selected = Convert.ToInt32(Console.ReadLine());
                    var anexoSelected = descargarFacturaResponse.factura.anexos[selected];
                    if (anexoSelected.mime != "application/pdf")
                    {
                        throw new Exception("Sólo pdfs porfi.");
                    }
                    var base64BinaryStr = anexoSelected?.anexo;
                    var filename = anexoSelected?.nombre + ".pdf";
                    ConverBase64ToFile(base64BinaryStr, filename);
                    System.Diagnostics.Process.Start(filename);
                }
            }
        }
        private static void ConsultarListadoFacturas(ConnectionManager manager)
        {
            var consultarFacturaRespose = manager.ConsultarListadoFacturas(new List<string> { "A05040312-FACT-2024-2", "A05040312-FACT-2024-3" });
            if (consultarFacturaRespose.resultado.codigo == "0")
            {
                foreach (var factura in consultarFacturaRespose.facturas)
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
            if (consultarFacturaRespose.resultado.codigo == "0")
            {
                Console.WriteLine($"Tramitación: " + consultarFacturaRespose.factura.tramitacion.descripcion);
                Console.WriteLine($"Anulación: " + consultarFacturaRespose.factura.anulacion.descripcion);
            }
            Console.ReadLine();
        }

        private static void CambiarEstado(ConnectionManager manager, string numeroRegisto, string oficinaContable)
        {
            var consultarFacturaResponsee = manager.CambiarEstadoFactura(oficinaContable, numeroRegisto, "1200", "Pruebas desde conexion WebServices.");
            Debug.WriteLine(consultarFacturaResponsee.resultado.descripcion);
        }
        public static void ConverBase64ToFile(string base64BinaryStr, string filename)
        {
            byte[] bytes = Convert.FromBase64String(base64BinaryStr);
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            System.IO.FileStream stream = new FileStream(filename, FileMode.CreateNew);
            System.IO.BinaryWriter writer =
                new BinaryWriter(stream);
            writer.Write(bytes, 0, bytes.Length);
            writer.Close();
        }
    }
}
