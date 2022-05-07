using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Kaidao.Domain.IdentityEntity;
using Kaidao.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text.Json;

namespace Kaidao.Services.Api.IdentityServer;

public class IdentityProfileService : IProfileService
{
    private readonly IUserClaimsPrincipalFactory<AppUser> _claimsFactory;
    private readonly UserManager<AppUser> _userManager;

    private readonly IPermissionRepository _permissionRepository;
    private readonly IUserPermissionRepository _userPermissionRepository;

    public IdentityProfileService(
        IUserClaimsPrincipalFactory<AppUser> claimsFactory,
        UserManager<AppUser> userManager,
        IPermissionRepository permissionRepository,
        IUserPermissionRepository userPermissionRepository
    )
    {
        _claimsFactory = claimsFactory;
        _userManager = userManager;
        _permissionRepository = permissionRepository;
        _userPermissionRepository = userPermissionRepository;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        if (user == null)
        {
            throw new ArgumentException("");
        }

        var principal = await _claimsFactory.CreateAsync(user);
        var claims = principal.Claims.ToList();
        var roles = await _userManager.GetRolesAsync(user);

        var realtimePermissionsQuery =
                (from p in _permissionRepository.GetByRoleId(roles) select p.FunctionId + "_" + p.CommandId)
                .Union(from up in _userPermissionRepository.GetByAllow(user.Id, true) select up.FunctionId + "_" + up.CommandId)
                .Except(from up in _userPermissionRepository.GetByAllow(user.Id, false) select up.FunctionId + "_" + up.CommandId);

        var realtimePermissionsList = realtimePermissionsQuery.Distinct().ToList();

        //Add more claims like this
        //claims.Add(new Claim(ClaimTypes.Name, user.UserName));
        //claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        //claims.Add(new Claim(ClaimTypes.Role, string.Join(";", roles)));
        claims.Add(new Claim("permissions", JsonSerializer.Serialize(realtimePermissionsQuery)));

        context.IssuedClaims = claims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        context.IsActive = user != null;
    }
}