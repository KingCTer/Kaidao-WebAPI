using Kaidao.Domain.Constants;
using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Core.Notifications;
using Kaidao.Services.Api.IdentityServer.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Kaidao.Services.Api.Controllers
{
    public class TestController : BaseController
    {
        public TestController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator) : base(notifications, mediator)
        {
        }

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
            var permissionsClaim = User.Claims.SingleOrDefault(c => c.Type == IdentityConstant.Claims.Permissions);
            if (permissionsClaim != null)
            {
                var claimPermissionsList = JsonSerializer.Deserialize<List<string>>(permissionsClaim.Value);
            }
            return Ok(JsonSerializer.Serialize("AllowAdmin"));
        }

        [HttpGet("AllowUser")]
        [ClaimRequirement(FunctionCode.SYSTEM, CommandCode.READ)]
        public IActionResult AllowUser()
        {
            return Ok("AllowUser");
        }
    }
}