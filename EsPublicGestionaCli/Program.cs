using EsPublicGestionaLib;
using EsPublicGestionaLib.DescargarFacturaResponse;
using EsPublicGestionaLib.Helpers;
using EsPublicGestionaLib.SolicitarNuevasFacturasResponse;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var solicitarNuevasFacturasResponse = manager.SolicitarNuevasFacturas("A05040312");
            if (solicitarNuevasFacturasResponse.@return.resultado.codigo == 0)
            {
                for (int i = 0; i < solicitarNuevasFacturasResponse.@return.facturas.Count(); i++)
                {
                    var factura = solicitarNuevasFacturasResponse.@return.facturas[i];
                    Console.WriteLine($"Nñumero de registro {i}.: {factura.numeroRegistro} - {factura.fechaHoraRegistro}");
                }
                var selected = Convert.ToInt32(Console.ReadLine());
                var numeroRegisto = solicitarNuevasFacturasResponse.@return.facturas[selected].numeroRegistro;
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
    }
}
