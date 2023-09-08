using Newtonsoft.Json;
using System.Net.Http;

namespace DachaMentat.Services
{
    public class HttpService
    {
        private HttpClient _httpClient;
        private string _baseUrl;

        public HttpService(string baseUrl,  HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public async Task<TResponse> Get<TResponse>(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(GetFullUrl(url));
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseText);
        }

        private string GetFullUrl(string url)
        {
            return _baseUrl + url;
        }

        public async Task<TResponse> Post<TResponse>(string url, string body)
        {
            HttpResponseMessage response = await _httpClient.PostAsync(GetFullUrl(url), new StringContent(body));
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseBody);
        }

        public async Task<TResponse> Put<TResponse>(string url, string body)
        {
            HttpResponseMessage response = await _httpClient.PutAsync(GetFullUrl(url), new StringContent(body));
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseBody);
        }
    }
}
