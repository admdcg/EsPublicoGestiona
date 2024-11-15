using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace EsPublicGestionaLib.Helpers
{
    public static class SerializationUtils
    {

        public static string JSONSerializeToString(this object objectInstance, Boolean skipNullValues = false)
        {
            if (skipNullValues)
            {
                return JsonConvert.SerializeObject(objectInstance, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            else
            {
                return JsonConvert.SerializeObject(objectInstance);
            }
        }
        public static T JSONDeserializeFromString<T>(this string objectData)
        {
            return (T)JsonConvert.DeserializeObject<T>(objectData);
        }
        public static JArray JSONArray(this string data)
        {
            return JArray.Parse(data);
        }
            
        public static string XmlSerializeToString(this object objectInstance, bool ignoreNewLinehandling = false, Boolean withoutNameSpace = false)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = false;
            settings.OmitXmlDeclaration = true;
            if (ignoreNewLinehandling) {
                settings.NewLineOnAttributes = false;
                settings.NewLineChars = "";
                settings.NewLineHandling = NewLineHandling.None;
            }

            StringWriter stringWriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings);

            XmlSerializer serializer = new XmlSerializer(objectInstance.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            if (withoutNameSpace)
            {
                ns.Add("", "");
            }
            serializer.Serialize(xmlWriter, objectInstance, ns);

            string serializated = stringWriter.ToString();
            stringWriter.Dispose();
            return serializated;
        }

        public static T XmlDeserializeFromString<T>(this string objectData)
        {
            return (T)XmlDeserializeFromString(objectData, typeof(T));
        }

        public static object XmlDeserializeFromString(this string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }

        public static T XmlDeserializeFromString<T>(this string objectData, XmlRootAttribute xmlRootAttribute)
        {
            return (T)XmlDeserializeFromString(objectData, typeof(T), xmlRootAttribute);
        }

        public static object XmlDeserializeFromString(this string objectData, Type type, XmlRootAttribute xmlRootAttribute)
        {
            var serializer = new XmlSerializer(type, xmlRootAttribute);
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }

        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObjecttoFile"></param>
        /// <param name="fileName"></param>
        public static void SerializeObjectToFile<T>(T serializableObject, string fileName, Boolean withoutNameSpace = false)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                if (withoutNameSpace)
                {
                    ns.Add("", "");
                }                
                using (MemoryStream stream = new MemoryStream())
                {                    
                    serializer.Serialize(stream, serializableObject, ns);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deserializes an xml file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T DeSerializeObjectFromFile<T>(string fileName, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return default(T);
            }
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            if (encoding == null)
            {
                encoding = Encoding.Default;                
            }
            using (StreamReader streamReader = new StreamReader(fileName, encoding))
            {       
                var returnObject = (T)xmlSerializer.Deserialize(streamReader);                
                return returnObject;
            }
        }

        public static T DeSerializeObjectFromArray<T>(byte[] param)
        {
            using (MemoryStream ms = new MemoryStream(param))
            {
                IFormatter br = new BinaryFormatter();
                return (T)br.Deserialize(ms);
            }
        }

        public static string SerializeObject<T>(this T toSerialize, bool withOutXmlns = false)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            using (StringWriter textWriter = new StringWriter())
            {
                if (withOutXmlns)
                {
                    ns.Add("", "");
                }                                
                xmlSerializer.Serialize(textWriter, toSerialize, ns);
                return textWriter.ToString();
            }
        }

        public static T ConvertEqualObject<T>(Object objectFrom) 
        {
            if (objectFrom == null)
                return default(T);
            String strObectFrom = XmlSerializeToString(objectFrom);
            T objectTo = XmlDeserializeFromString<T>(strObectFrom);
            return objectTo;
        }

        public static string RemoveAllNamespaces(string xmlDocument)
        {
            XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(xmlDocument));
            return xmlDocumentWithoutNs.ToString();
        }

        //Core recursion function
        private static XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    if (!attribute.IsNamespaceDeclaration)
                        xElement.Add(attribute);

                return xElement;
            }
            return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
        }       
        public static T CopyObject<T>(T source)
        {
            var sourceSerialized = SerializeObject<T>(source);
            var targetDesSerialized = XmlDeserializeFromString<T>(sourceSerialized);
            return targetDesSerialized;
        }
    }   
}
