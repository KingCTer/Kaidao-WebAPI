using Kaidao.Domain.Constants;
using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Core.Notifications;
using Kaidao.Domain.IdentityEntity;
using Kaidao.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Kaidao.Services.Api.Controllers
{
    public class PermissionsController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly IPermissionRepository _permissionRepository;
        private readonly IUserPermissionRepository _userPermissionRepository;

        public PermissionsController(
            INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator,
            UserManager<AppUser> userManager,
            IPermissionRepository permissionRepository,
            IUserPermissionRepository userPermissionRepository
            ) : base(notifications, mediator)
        {
            _userManager = userManager;
            _permissionRepository = permissionRepository;
            _userPermissionRepository = userPermissionRepository;
        }
        

        [HttpGet("check")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckPermissinStatus()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var realtimePermissionsQuery =
                    (from p in _permissionRepository.GetByRoleId(roles) select p.FunctionId + "_" + p.CommandId)
                    .Union(from up in _userPermissionRepository.GetByAllow(user.Id, true) select up.FunctionId + "_" + up.CommandId)
                    .Except(from up in _userPermissionRepository.GetByAllow(user.Id, false) select up.FunctionId + "_" + up.CommandId);

                var realtimePermissionsList = realtimePermissionsQuery.Distinct().ToList();

                var permissionsClaim = HttpContext.User.Claims.SingleOrDefault(c => c.Type == IdentityConstant.Claims.Permissions);

                if (permissionsClaim != null)
                {
                    var claimPermissionsList = JsonSerializer.Deserialize<List<string>>(permissionsClaim.Value);

                    if (!Enumerable.SequenceEqual(realtimePermissionsList, claimPermissionsList))
                    {
                        return Unauthorized();
                    }

                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Unauthorized();
            }
            
        }
    }
}
