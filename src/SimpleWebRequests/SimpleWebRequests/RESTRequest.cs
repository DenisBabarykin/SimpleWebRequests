using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleWebRequests
{
    /// <summary>Class for REST requests.</summary>
    public class RESTRequest
    {
        /// <summary>Async GET request to the api that responses in JSON format</summary>
        /// <returns>Dynamic object with response data</returns>
        public static async Task<dynamic> GetJSONAsync(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    response.Content.Headers.ContentType.MediaType = "application/json";
                    return response.Content.ReadAsAsync<ExpandoObject>().Result;
                }
                else
                    throw new HttpRequestException("Error in GetJSONAsync() with param: " + url);
            }
        }

        /// <summary>Async GET request to the api that responses in text/html format</summary>
        /// <returns>String with response data</returns>
        public static async Task<string> GetHTMLAsync(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                    throw new HttpRequestException("Error in GetHTML() with param: " + url);
            }
        }

        /// <summary>Async GET request to the api that responses in XML format</summary>
        /// <returns>Dynamic object with response data</returns>
        public static async Task<dynamic> GetXMLAsync(string url)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            var xDoc = XDocument.Load((await request.GetResponseAsync()).GetResponseStream());

            return XMLToDynamicConverter.Convert(xDoc.Elements().First());
        }

        /// <summary>Async POST request to the api and getting response</summary>
        /// <param name="body">Body of POST request</param>
        /// <returns>Dynamic object with response data</returns>
        public static async Task<dynamic> PostJSONAsync(string url, Dictionary<string, string> body)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync(url, new FormUrlEncodedContent(body));
                if (response.IsSuccessStatusCode)
                {
                    response.Content.Headers.ContentType.MediaType = "application/json";
                    return response.Content.ReadAsAsync<ExpandoObject>().Result;
                }
                else
                    throw new HttpRequestException("Error in PostJSON() with params: " + url + " body: " + body.ToString());
            }
        }

        static string GetbaseAddress(string url)
        {
            return url.Substring(0, url.IndexOf("?"));
        }

        static string GetParams(string url)
        {
            return url.Substring(url.IndexOf("?"));
        }
    }
}
