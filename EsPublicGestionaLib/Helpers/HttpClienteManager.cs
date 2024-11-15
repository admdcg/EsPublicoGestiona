using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.ComTypes;
using Newtonsoft.Json;

namespace EsPublicGestionaLib.Helpers
{
    public static class HttpClienteManager
    {
        public enum HttpVerbs
        {
            GET,
            POST,
            DELETE,
        }
        public static string Action(HttpVerbs verb, HttpClient service = null, String body = null, String mediaStype = "application/json")
        {            
            HttpResponseMessage httpResponseMessage = null;
            try
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                if (verb == HttpVerbs.POST)
                {
                    var stringContent = new StringContent(body, Encoding.UTF8, mediaStype);
                    httpResponseMessage = service.PostAsync(service.BaseAddress.OriginalString, stringContent).Result;
                }
                else if (verb == HttpVerbs.GET)
                {
                    httpResponseMessage = service.GetAsync(service.BaseAddress.OriginalString).Result;
                }
                else if (verb == HttpVerbs.DELETE)
                {
                    httpResponseMessage = service.DeleteAsync(service.BaseAddress.OriginalString).Result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error conectando via {verb.ToString()} con {service.BaseAddress.OriginalString}.",
                      new Exception($"{ExceptionHelper.MountMessageException(ex)}"));
            }
            var jsonResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                ResponseErr responseErr;
                try
                {
                    if (mediaStype == "application/json")
                    {
                        responseErr = SerializationUtils.JSONDeserializeFromString<ResponseErr>(jsonResponse);
                    }
                    else if (mediaStype.Contains("xml"))
                    {
                        responseErr = SerializationUtils.XmlDeserializeFromString<ResponseErr>(jsonResponse);
                    }
                    else
                    {
                        responseErr = new ResponseErr
                        {
                            message = jsonResponse
                        };                        
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Respuesta http errónea. Error deserializando: {jsonResponse}", ex);
                }
                throw new Exception($"Respuesta http errónea. statuscode: {httpResponseMessage.StatusCode} - reason: {httpResponseMessage.ReasonPhrase} - message: {responseErr.message}");
            }
            return jsonResponse;
        }

        public static string Post(String body, HttpClient service = null)
        {            
            return Action(HttpVerbs.POST, service, body);
        }
        public static string Delete(HttpClient service = null)
        {         
            return Action(HttpVerbs.DELETE, service);
        }
        public static string Get(HttpClient service = null)
        {         
            return Action(HttpVerbs.GET, service);
        }
    }
    public class ResponseErr
    {
        public String code { get; set; }
        public String message { get; set; }
        public DataErr data { get; set; }
    }
    public class DataErr
    {
        public int status { get; set; }

        [JsonProperty("params")]
        public String[] parametros { get; set; }
    }
}
