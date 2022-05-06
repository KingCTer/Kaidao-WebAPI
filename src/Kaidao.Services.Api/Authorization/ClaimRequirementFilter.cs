using Kaidao.Domain.Constants;
using Kaidao.Domain.IdentityEntity;
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

    public ClaimRequirementFilter(
        FunctionCode functionCode, 
        CommandCode commandCode,
        UserManager<AppUser> userManager,
        AuthDbContext context,
        RoleManager<AppRole> roleManager
        )
    {
        _functionCode = functionCode;
        _commandCode = commandCode;
        _userManager = userManager;
        _context = context;
        _roleManager = roleManager;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = _userManager.GetUserAsync(context.HttpContext.User).Result;
        if (user != null)
        {
            var roles = _userManager.GetRolesAsync(user).Result;
            var query = (from p in _context.Permissions where roles.Contains(p.RoleId) select p.FunctionId + "_" + p.CommandId)
                        .Union(from up in _context.UserPermissions where up.Allow select up.FunctionId + "_" + up.CommandId)
                        .Except(from up in _context.UserPermissions where !up.Allow select up.FunctionId + "_" + up.CommandId);

            var permissions = query.Distinct().ToList();

            var permissionsClaim = context.HttpContext.User.Claims
                .SingleOrDefault(c => c.Type == IdentityConstant.Claims.Permissions);
            if (permissionsClaim != null)
            {
                var permissions2 = JsonSerializer.Deserialize<List<string>>(permissionsClaim.Value);

                if (!Enumerable.SequenceEqual(permissions, permissions2))
                {
                    context.Result = new ForbidResult();
                }

                if (!permissions2!.Contains(_functionCode + "_" + _commandCode))
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