using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Kaidao.Web.Admin.Authorization;
using Kaidao.Web.Admin.Constants;

namespace Kaidao.Web.Admin.Controllers
{
    public class TestController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestController(
            IHttpClientFactory httpClientFactory
            )
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> AllowAnonymous()
        {
            var client = _httpClientFactory.CreateClient("Kaidao.Services.Api");
            client.BaseAddress = new Uri("https://localhost:5000");

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync("api/Test/AllowAnonymous");

            var body = await response.Content.ReadAsStringAsync();

            return Ok(body);
        }

        [ClaimRequirement(FunctionCode.SYSTEM, CommandCode.CREATE)]
        public async Task<IActionResult> AllowAdmin()
        {
            var client = _httpClientFactory.CreateClient("Kaidao.Services.Api");
            client.BaseAddress = new Uri("https://localhost:5000");

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync("api/Test/AllowAdmin");

            var body = await response.Content.ReadAsStringAsync();


            return Ok(body);
        }

        [ClaimRequirement(FunctionCode.SYSTEM, CommandCode.READ)]
        public async Task<IActionResult> AllowUser()
        {
            var client = _httpClientFactory.CreateClient("Kaidao.Services.Api");
            client.BaseAddress = new Uri("https://localhost:5000");

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync("api/Test/AllowUser");

            var body = await response.Content.ReadAsStringAsync();

            return Ok(body);
        }
    }
}
