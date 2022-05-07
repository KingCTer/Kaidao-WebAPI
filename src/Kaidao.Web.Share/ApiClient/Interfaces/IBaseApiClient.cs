using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaidao.Web.Share.ApiClient.Interfaces
{
    public interface IBaseApiClient
    {
        Task<HttpResponseMessage> GetResponseAsync(string url, bool requiredLogin = false);
        Task<List<T>> GetListAsync<T>(string url, bool requiredLogin = false);
        Task<T> GetAsync<T>(string url, bool requiredLogin = false);
        Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest requestContent, bool requiredLogin = true);
        Task<bool> PutAsync<TRequest, TResponse>(string url, TRequest requestContent, bool requiredLogin = true);
    }
}
