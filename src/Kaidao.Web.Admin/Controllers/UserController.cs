using Kaidao.Web.Admin.Authorization;
using Kaidao.Web.Admin.Constants;
using Kaidao.Web.Share.ApiClient.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Web.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IBaseApiClient _baseApiClient;

        public UserController(IBaseApiClient baseApiClient) : base(baseApiClient)
        {
            _baseApiClient = baseApiClient;
        }

        [ClaimRequirement(FunctionCode.SYSTEM, CommandCode.CREATE)]
        public async Task<IActionResult> Index()
        {
            
            var data = await _baseApiClient.GetAsync<string>("api/Test/AllowAdmin", true);
            return Ok(data);
        }
    }
}
