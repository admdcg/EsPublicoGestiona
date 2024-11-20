using FACeLib;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;
using System.Xml.Linq;

class MessageInspector : IClientMessageInspector
{
    private readonly string _certificatPath;
    private readonly string _certificatePassword;
    public MessageInspector(string certificatPath, string certificatePassword)
    {
        _certificatPath = certificatPath;
        _certificatePassword = certificatePassword;
    }
    public void AfterReceiveReply(ref Message reply, object correlationState)
    {
        XmlDocument doc = new XmlDocument();
        MemoryStream ms = new MemoryStream();
        XmlWriter writer = XmlWriter.Create(ms);
        reply.WriteMessage(writer);
        writer.Flush();
        ms.Position = 0;
        doc.Load(ms);

        var securityNode = doc.DocumentElement.ChildNodes.Item(0);
        if (securityNode == null)
        {
            throw new Exception("No se encontró el nodo Security en el mensaje SOAP.");
        }
        doc.DocumentElement.RemoveChild(securityNode);

        ms.SetLength(0);
        writer = XmlWriter.Create(ms);
        doc.WriteTo(writer);
        writer.Flush();
        ms.Position = 0;
        XmlReader reader = XmlReader.Create(ms);

        reply = System.ServiceModel.Channels.Message.CreateMessage(reader, int.MaxValue, reply.Version);

    }

    public object BeforeSendRequest(ref Message request, IClientChannel channel)
    {
        Message newRequest = TransformMessage2(request);

        // Reemplazar el mensaje original por el mensaje modificado
        request = newRequest;

        return null;
    }

    private Message CreateMessageRequest(ref Message request)
    {
        // Crear un MessageBuffer para clonar el mensaje y permitir modificaciones
        var buffer = request.CreateBufferedCopy(int.MaxValue);
        request = buffer.CreateMessage(); // Crear una copia del mensaje original

        // Convertir el mensaje en un XmlDocument para agregar el encabezado de seguridad
        var doc = new XmlDocument();
        doc.PreserveWhitespace = true;

        using (var memoryStream = new System.IO.MemoryStream())
        {
            using (var xmlWriter = XmlWriter.Create(memoryStream))
            {
                request.WriteMessage(xmlWriter);
                xmlWriter.Flush();
                memoryStream.Position = 0;
                doc.Load(memoryStream);
            }
        }
        XmlElement root = doc.DocumentElement;
        root.SetAttribute("xmlns:web", "https://webservice.face.gob.es");
        Chilkat.Xml xmlToSign = new Chilkat.Xml();
        xmlToSign.LoadXml(doc.OuterXml);
        //xmlToSign.Tag = "s:Envelope";
        //xmlToSign.UpdateAttrAt("s:Envelope", true, "xmlns:s", "http://schemas.xmlsoap.org/soap/envelope/");

        //xmlToSign.UpdateAttrAt("s:Envelope", true, "xmlns:web", "https://webservice.face.gob.es");
        //xmlToSign.UpdateAttrAt("s:Envelope", true, "xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
        //xmlToSign.UpdateAttrAt("s:Envelope", true, "xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        xmlToSign.UpdateAttrAt("s:Header|wsse:Security", true, "xmlns:wsse", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
        xmlToSign.UpdateAttrAt("s:Header|wsse:Security", true, "xmlns:wsu", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
        xmlToSign.UpdateAttrAt("s:Body", true, "wsu:Id", "id-B88764AFBD17E5C06917310456194329");
        xmlToSign.UpdateAttrAt("s:Body", true, "xmlns:wsu", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");

        xmlToSign.UpdateAttrAt($"s:Body|web:solicitarNuevasFacturas", true, "s:encodingStyle", "http://schemas.xmlsoap.org/soap/encoding/");
        //xmlToSign.UpdateAttrAt($"soapenv:Body|web:{action}|{parameter}", true, "xsi:type", "xsd:string");
        //xmlToSign.UpdateChildContent($"soapenv:Body|web:{action}|{parameter}", value);
        var requestDoc = new XmlDocument();
        Chilkat.StringBuilder sbXml = new Chilkat.StringBuilder();
        xmlToSign.GetXmlSb(sbXml);
        var content = sbXml.GetAsString();
        requestDoc.LoadXml(content);

        // Añadir Namespace wsse
        var namespaceManager = new XmlNamespaceManager(doc.NameTable);
        namespaceManager.AddNamespace("wsse", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
        namespaceManager.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");

        var bodyNode = requestDoc.SelectSingleNode("//soapenv:Body/*[1]", namespaceManager);
        if (bodyNode == null)
        {
            throw new Exception("No se encontró el nodo Body en el mensaje SOAP.");
        }
        var bodyDoc = new XmlDocument();
        bodyDoc.LoadXml(bodyNode.OuterXml);


        var securityHeader = CreateSecurityHeader(xmlToSign);
        var version = buffer.CreateMessage().Version;
        var action = buffer.CreateMessage().Headers.Action;
        var body = bodyDoc.DocumentElement;
        var newRequest = Message.CreateMessage(version, action, body);

        // Agregar el encabezado de seguridad al nuevo mensaje
        newRequest.Headers.Add(new SecurityHeader(securityHeader));
        return newRequest;
    }

    private Message TransformMessage2(Message request)
    {
        XmlDocument doc = new XmlDocument();
        XmlDocument requestDoc = new XmlDocument();
        MemoryStream ms = new MemoryStream();
        XmlWriter writer = XmlWriter.Create(ms);
        request.WriteMessage(writer);
        writer.Flush();
        ms.Position = 0;
        doc.Load(ms);

        requestDoc = ChangeMessage(doc);
        var securityHeader = CreateSecurityHeader(requestDoc);

        var securityNode = requestDoc.DocumentElement.ChildNodes.Item(0);
        if (securityNode == null)
        {
            throw new Exception("No se encontró el nodo Body en el mensaje SOAP.");
        }
        requestDoc.DocumentElement.RemoveChild(securityNode);

        ms.SetLength(0);
        writer = XmlWriter.Create(ms);
        requestDoc.WriteTo(writer);
        writer.Flush();
        ms.Position = 0;
        XmlReader reader = XmlReader.Create(ms);

        request = System.ServiceModel.Channels.Message.CreateMessage(reader, int.MaxValue, request.Version);
        request.Headers.Add(new SecurityHeader(securityHeader));

        //string result = "Client ready to send message:\n" + request + "\n";
        //Console.WriteLine(result);
        return request;
    }
    private XmlDocument ChangeMessage(XmlDocument doc)
    {        
        XmlElement root = doc.DocumentElement;
        root.SetAttribute("xmlns:web", "https://webservice.face.gob.es");
        Chilkat.Xml xmlToSign = new Chilkat.Xml();
        xmlToSign.LoadXml(doc.OuterXml);        
        xmlToSign.UpdateAttrAt("s:Header|wsse:Security", true, "xmlns:wsse", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
        xmlToSign.UpdateAttrAt("s:Header|wsse:Security", true, "xmlns:wsu", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
        xmlToSign.UpdateAttrAt("s:Body", true, "wsu:Id", "id-B88764AFBD17E5C06917310456194329");
        xmlToSign.UpdateAttrAt("s:Body", true, "xmlns:wsu", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
        //xmlToSign.UpdateAttrAt($"s:Body|web:solicitarNuevasFacturas", true, "s:encodingStyle", "http://schemas.xmlsoap.org/soap/encoding/");
        
        var requestDoc = new XmlDocument();
        Chilkat.StringBuilder sbXml = new Chilkat.StringBuilder();
        xmlToSign.GetXmlSb(sbXml);
        var content = sbXml.GetAsString();
        requestDoc.LoadXml(content);

        return requestDoc;
    }
    public XmlElement CreateSecurityHeader(XmlDocument doc)
    {
        Chilkat.Xml xmlToSign = new Chilkat.Xml();
        xmlToSign.LoadXml(doc.OuterXml);
        return CreateSecurityHeader(xmlToSign);
    }
    public XmlElement CreateSecurityHeader(Chilkat.Xml xmlToSign)
    {
        bool success = true;
        // Create the XML to be signed...

        Chilkat.XmlDSigGen gen = new Chilkat.XmlDSigGen();
        gen.SigLocation = "s:Envelope|s:Header|wsse:Security";
        gen.SigLocationMod = 0;
        gen.SigId = "SIG-B88764AFBD17E5C069173104561944110";
        gen.SigNamespacePrefix = "ds";
        gen.SigNamespaceUri = "http://www.w3.org/2000/09/xmldsig#";
        gen.SignedInfoPrefixList = "s web xsd xsi";
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
        success = cert.LoadPfxFile(_certificatPath, _certificatePassword);
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
        var content = sbXml.GetAsString();
        Debug.WriteLine(content);
        var requestDoc = new XmlDocument();
        requestDoc.LoadXml(content);
        // Crear un XmlNamespaceManager para manejar los namespaces de la respuesta
        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(requestDoc.NameTable);
        namespaceManager.AddNamespace("s", "http://schemas.xmlsoap.org/soap/envelope/");
        namespaceManager.AddNamespace("wsse", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");

        // Usar XPath para obtener el primer nodo hijo de soap:Body
        XmlNode firstChildNode = requestDoc.SelectSingleNode("//s:Header/wsse:Security/*[1]", namespaceManager);
        if (firstChildNode == null)
        {
            throw new Exception("No se encontró la respuesta en el Body.");
        }
        var shortResponse = new XmlDocument();
        shortResponse.LoadXml(firstChildNode.OuterXml);
        return shortResponse.DocumentElement;
    }
}

