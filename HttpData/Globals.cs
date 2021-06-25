using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace HttpData
{
    public static class Globals
    {
        public const string ApiBaseUrl = "https://localhost:44315";
        //public const string ApiBaseUrl = "https://animalshelterwebservice.azurewebsites.net";

        /// <summary>
        /// Generic method for making HTTP GET requests.
        /// </summary>
        /// <typeparam name="T">The object type to get.</typeparam>
        /// <param name="endpoint">The endpoint to send the request to.</param>
        /// <returns>API response of T.</returns>
        public static T HttpGet<T>(string endpoint)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    using HttpResponseMessage response = httpClient.GetAsync(ApiBaseUrl + endpoint).Result;
                    response.EnsureSuccessStatusCode();
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(apiResponse);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Generic method for making HTTP POST requests.
        /// </summary>
        /// <typeparam name="T">The object type to post.</typeparam>
        /// <param name="entity">The object to post.</param>
        /// <param name="endpoint">The endpoint to send the request to.</param>
        public static T HttpPost<T>(T entity, string endpoint)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(entity);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    using HttpResponseMessage response = httpClient.PostAsync(ApiBaseUrl + endpoint, content).Result;
                    response.EnsureSuccessStatusCode();
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(apiResponse);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Generic method for making HTTP PUT requests.
        /// </summary>
        /// <typeparam name="T">The object type to put.</typeparam>
        /// <param name="entity">The object to put.</param>
        /// <param name="endpoint">The endpoint to send the request to.</param>
        public static void HttpPut<T>(T entity, string endpoint)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(entity, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    using HttpResponseMessage response = httpClient.PutAsync(ApiBaseUrl + endpoint, content).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}