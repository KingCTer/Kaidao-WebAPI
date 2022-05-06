using Kaidao.Domain.Constants;
using Kaidao.Domain.IdentityEntity;
using Kaidao.Domain.Interfaces;
using Kaidao.Infra.CrossCutting.Identity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace Kaidao.Services.Api.IdentityServer.Authorization;

public class ClaimRequirementFilter : IAuthorizationFilter
{
    private readonly FunctionCode _functionCode;
    private readonly CommandCode _commandCode;

    private readonly UserManager<AppUser> _userManager;
    private readonly AuthDbContext _context;
    private readonly RoleManager<AppRole> _roleManager;

    private readonly IPermissionRepository _permissionRepository;
    private readonly IUserPermissionRepository _userPermissionRepository;

    public ClaimRequirementFilter(
        FunctionCode functionCode, 
        CommandCode commandCode,
        UserManager<AppUser> userManager,
        AuthDbContext context,
        RoleManager<AppRole> roleManager,
        IPermissionRepository permissionRepository,
        IUserPermissionRepository userPermissionRepository
        )
    {
        _functionCode = functionCode;
        _commandCode = commandCode;
        _userManager = userManager;
        _context = context;
        _roleManager = roleManager;
        _permissionRepository = permissionRepository;
        _userPermissionRepository = userPermissionRepository;
}

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = _userManager.GetUserAsync(context.HttpContext.User).Result;
        if (user != null)
        {
            var roles = _userManager.GetRolesAsync(user).Result;

            var realtimePermissionsQuery = 
                (from p in _permissionRepository.GetByRoleId(roles) select p.FunctionId + "_" + p.CommandId)
                .Union(from up in _userPermissionRepository.GetByAllow(true) select up.FunctionId + "_" + up.CommandId)
                .Except(from up in _userPermissionRepository.GetByAllow(false) select up.FunctionId + "_" + up.CommandId);


            var realtimePermissionsList = realtimePermissionsQuery.Distinct().ToList();

            var permissionsClaim = context.HttpContext.User.Claims
                .SingleOrDefault(c => c.Type == IdentityConstant.Claims.Permissions);
            if (permissionsClaim != null)
            {
                var claimPermissionsList = JsonSerializer.Deserialize<List<string>>(permissionsClaim.Value);

                if (!Enumerable.SequenceEqual(realtimePermissionsList, claimPermissionsList))
                {
                    context.Result = new ForbidResult();
                }

                if (!claimPermissionsList!.Contains(_functionCode + "_" + _commandCode))
                {
                    context.Result = new ForbidResult();
                }
            }
            else
            {
                context.Result = new ForbidResult();
            }
        }
        else
        {
            context.Result = new ForbidResult();
        }

    }
}