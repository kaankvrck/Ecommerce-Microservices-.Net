using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.OrderAPI.Common
{
    public class ApiServiceHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiServiceHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetAsync(string apiUrl)
        {
            var httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            return await GetHttpResponse(response);
        }

        

        public async Task<string> GetWithParamAsync(string apiUrl, string paramValue)
        {
            string fullUrl = $"{apiUrl}/{paramValue}";

            var httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await httpClient.GetAsync(fullUrl);

            return await GetHttpResponse(response);
        }

        public async Task<string> PutAsync(string apiUrl, string data)
        {
            var httpClient = _httpClientFactory.CreateClient();

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(apiUrl, content);

            return await GetHttpResponse(response);
        }
        private static async Task<string> GetHttpResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return $"GET request failed with status code: {response.StatusCode}";
            }
        }
    }
}
