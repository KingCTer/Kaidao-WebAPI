using Kaidao.Web.Share.ApiClient.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using IdentityModel.Client;

namespace Kaidao.Web.Share.ApiClient
{
    public class BaseApiClient : IBaseApiClient
    {
        private readonly string CLIENT_NAME;
        private readonly string CLIENT_BASE;

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseApiClient(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;

            CLIENT_NAME = _configuration["KaidaoServicesApi:ClientName"];
            CLIENT_BASE = _configuration["KaidaoServicesApi:BaseAddress"];
        }

        public async Task<HttpResponseMessage> GetResponseAsync(string url, bool requiredLogin = false)
        {
            var client = _httpClientFactory.CreateClient(CLIENT_NAME);
            client.BaseAddress = new Uri(CLIENT_BASE);

            if (requiredLogin)
            {
                var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await client.GetAsync(url);

            return response;
        }

        public async Task<List<T>> GetListAsync<T>(string url, bool requiredLogin = false)
        {
            var client = _httpClientFactory.CreateClient(CLIENT_NAME);
            client.BaseAddress = new Uri(CLIENT_BASE);

            if (requiredLogin)
            {
                var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await client.GetAsync(url);

            var body = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<List<T>>(body);

            return data;
        }

        public async Task<T> GetAsync<T>(string url, bool requiredLogin = false)
        {
            var client = _httpClientFactory.CreateClient(CLIENT_NAME);
            client.BaseAddress = new Uri(CLIENT_BASE);

            if (requiredLogin)
            {
                var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync(url);

            var body = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<T>(body);

            return data;
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest requestContent, bool requiredLogin = true)
        {
            var client = _httpClientFactory.CreateClient(CLIENT_NAME);
            client.BaseAddress = new Uri(CLIENT_BASE);

            StringContent httpContent = null;
            if (requestContent != null)
            {
                var json = JsonSerializer.Serialize(requestContent);
                httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            }

            if (requiredLogin)
            {
                var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.PostAsync(url, httpContent);

            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<TResponse>(body);
            }

            throw new Exception(body);
        }

        public async Task<bool> PutAsync<TRequest, TResponse>(string url, TRequest requestContent, bool requiredLogin = true)
        {
            var client = _httpClientFactory.CreateClient(CLIENT_NAME);
            client.BaseAddress = new Uri(CLIENT_BASE);

            HttpContent httpContent = null;
            if (requestContent != null)
            {
                var json = JsonSerializer.Serialize(requestContent);
                httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            }

            if (requiredLogin)
            {
                var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.PutAsync(url, httpContent);

            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return true;

            throw new Exception(body);
        }
    }
}