using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace EsPublicGestionaLib.Helpers
{
    public static class DocumentExtensionsHelper
    {
        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }
        public static void RemoveAllAttributes(this XDocument doc)
        {
            foreach (var xAttr in doc.Root.Attributes().ToList())
            {
                xAttr.Remove();
            }
            foreach (var des in doc.Descendants())
            {
                des.RemoveAttributes();
            }
        }
        
        public static void RemoveAllAttributes(this XmlDocument doc)
        {
            // Si el nodo tiene atributos, eliminarlos todos
            RemoveAllAttributes(doc.DocumentElement);
        }
        public static void RemoveAllAttributes(XmlNode node)
        {
            // Si el nodo tiene atributos, eliminarlos todos
            if (node.Attributes != null)
            {
                node.Attributes.RemoveAll();
            }

            // Recorrer los nodos hijos y eliminar sus atributos recursivamente
            foreach (XmlNode child in node.ChildNodes)
            {
                RemoveAllAttributes(child);
            }
        }

        public static XmlDocument RemoveAllNamespaces(this XmlDocument doc)
        {
            // Crear un nuevo XmlDocument sin namespaces
            XmlDocument newDoc = new XmlDocument();
            var node = doc.DocumentElement;
            XmlNode newNode = CopyNodeWithoutNamespace(node, newDoc);
            newDoc.AppendChild(newNode);
            return newDoc;
        }

        private static XmlNode CopyNodeWithoutNamespace(XmlNode node, XmlDocument doc)
        {
            // Crear un nuevo nodo en el documento destino, usando solo el nombre local
            XmlNode newNode = doc.CreateElement(node.LocalName);

            // Copiar solo los atributos que no son namespaces
            foreach (XmlAttribute attr in node.Attributes)
            {
                if (!attr.Name.StartsWith("xmlns"))
                {
                    XmlAttribute newAttr = doc.CreateAttribute(attr.Name);
                    newAttr.Value = attr.Value;
                    newNode.Attributes.Append(newAttr);
                }
            }

            // Procesar recursivamente los hijos
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.NodeType == XmlNodeType.Text)
                {
                    newNode.AppendChild(doc.CreateTextNode(child.InnerText));
                }
                else
                {
                    newNode.AppendChild(CopyNodeWithoutNamespace(child, doc));
                }
            }

            return newNode;
        }
    }
}
