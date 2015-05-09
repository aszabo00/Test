using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace Test
{
    public class WebRequests
    {
        private string endpoint { get; set; }

        public WebRequests(string endpoint)
        {
            this.endpoint = endpoint;
        }

        // Deserialize data to a dictionary.
        public Dictionary<string, object> HttpDataToDictionary()
        {
            string data = GetHttpData();
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
        }

        // Gets the json data from a request.
        public string GetHttpData()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(endpoint).Result;
                if (!response.IsSuccessStatusCode) return null;

                HttpContent responseContent = response.Content;
                return responseContent.ReadAsStringAsync().Result;
            }
        }
    }
}