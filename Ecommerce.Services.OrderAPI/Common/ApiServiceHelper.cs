using System.Text;

namespace Ecommerce.Services.OrderAPI.Common
{
    public class ApiServiceHelper
    {
        public static async Task<string> GetAsync(string apiUrl)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"GET request failed with status code: {response.StatusCode}");
                    return null;
                }
            }
        }
        public static async Task<string> PutAsync(string apiUrl, string data)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"PUT request failed with status code: {response.StatusCode}");
                    return null;
                }
            }
        }
    }
}
