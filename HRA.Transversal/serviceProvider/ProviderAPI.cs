using Newtonsoft.Json;
using System.Collections;
using System.Net.Http.Headers;
using System.Text;
using System.Xml;

namespace HRA.Transversal.serviceProvider
{
    public class ProviderAPI
    {
        public async Task<dynamic> APICall<T>(string url, string method, object param, string token = "", string mediaType = "json", string returnObject = "")
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(new HttpMethod(method), url);
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                if (mediaType == "xml")
                {
                    request.Content = new StringContent(param.ToString(), Encoding.UTF8, "text/xml");
                }
                else
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, $"application/{mediaType}");
                }

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (mediaType == "xml")
                    {
                        if (returnObject == "enumerable")
                        {
                            var xmlDocument = new XmlDocument();
                            xmlDocument.LoadXml(content);
                            return JsonConvert.DeserializeObject(JsonConvert.SerializeXmlNode(xmlDocument));
                        }
                        else
                        {
                            var xmlDocument = new XmlDocument();
                            xmlDocument.LoadXml(content);
                            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeXmlNode(xmlDocument));
                        }
                    }
                    else
                    {
                        if (returnObject == "enumerable")
                        {
                            return JsonConvert.DeserializeObject(content);
                        }
                        else
                        {
                            return JsonConvert.DeserializeObject<T>(content);
                        }
                    }
                }
                else
                {
                    throw new HttpRequestException($"API request failed with status code {(int)response.StatusCode}");
                }
            }
        }
    }
}
