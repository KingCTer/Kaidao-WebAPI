using Kaidao.Domain.Constants;
using Kaidao.Services.Api.Controllers;
using Kaidao.Services.Api.IdentityServer.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.IdsApi.Controllers
{
    public class TestController : BaseController
    {
        [HttpGet("AllowAnonymous")]
        [AllowAnonymous]
        public IActionResult AllowAnonymous()
        {
            return Ok("AllowAnonymous");
        }

        [HttpGet("AllowAdmin")]
        [ClaimRequirement(FunctionCode.SYSTEM, CommandCode.CREATE)]
        public IActionResult AllowAdmin()
        {
            return Ok("AllowAdmin");
        }

        [HttpGet("AllowUser")]
        [ClaimRequirement(FunctionCode.SYSTEM, CommandCode.READ)]
        public IActionResult AllowUser()
        {
            return Ok("AllowUser");
        }
    }
}