using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FACeLib
{
    public class SecurityHeader : MessageHeader
    {
        private readonly XmlElement _securityElement;

        public SecurityHeader(XmlElement securityElement)
        {
            _securityElement = securityElement;
        }

        public override string Name => "Security";
        public override string Namespace => "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";

        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            _securityElement.WriteTo(writer);
        }
        protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            ////Escribe el inicio del encabezado con los espacios de nombres wsse y wsu
            writer.WriteStartElement("wsse", Name, Namespace);

            //// Agrega los atributos xmlns:wsse y xmlns:wsu
            //writer.WriteAttributeString("xmlns", "wsse", null, Namespace);
            writer.WriteAttributeString("xmlns", "wsu", null, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
        }
    }
}
