using Kaidao.Web.Share.ApiClient.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Kaidao.Web.Portal.Controllers
{
    public class AccessController : Controller
    {
        private readonly string CLIENT_NAME;
        private readonly string CLIENT_BASE;

        private readonly IBaseApiClient _baseApiClient;

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccessController(
            IBaseApiClient baseApiClient,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _baseApiClient = baseApiClient;

            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;

            CLIENT_NAME = _configuration["KaidaoServicesApi:ClientName"];
            CLIENT_BASE = _configuration["KaidaoServicesApi:BaseAddress"];
        }

        [AllowAnonymous]
        public async Task<IActionResult> AccessDenied()
        {
            var data = await _baseApiClient.GetResponseAsync("api/Permissions/check", true);
            switch (data.StatusCode)
            {
                case HttpStatusCode.Unauthorized:

                    await HttpContext.SignOutAsync();
                    return Challenge(new AuthenticationProperties
                    {
                        RedirectUri = HttpContext.Request.Query["ReturnUrl"]
                    });

                default:
                    break;
            }
            return View();
        }
    }
}
