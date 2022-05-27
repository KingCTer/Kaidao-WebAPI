using Kaidao.Domain.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace Kaidao.Web.Admin.Authorization
{
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private readonly FunctionCode _functionCode;
        private readonly CommandCode _commandCode;

        public ClaimRequirementFilter(FunctionCode functionCode, CommandCode commandCode)
        {
            _functionCode = functionCode;
            _commandCode = commandCode;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var permissionsClaim = context.HttpContext.User.Claims
                .SingleOrDefault(c => c.Type == IdentityConstant.Claims.Permissions);

            var routeUrl = context.RouteData.Values;
            var qsPath = context.HttpContext.Request.QueryString.Value;
            var returnUrl = $"/{routeUrl["controller"]}/{routeUrl["action"]}{qsPath}";

            if (permissionsClaim != null)
            {
                var permissions = JsonSerializer.Deserialize<List<string>>(permissionsClaim.Value);
                if (!permissions!.Contains(_functionCode + "_" + _commandCode))
                {
                    var msg = $"Requirement Permission: {_functionCode}_{_commandCode}";

                    

                    context.Result = new RedirectToActionResult("AccessDenied", "Access", new { msg = msg, returnUrl = returnUrl });
                }
            }
            else
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Access", new { msg = "Permission Null", returnUrl = returnUrl });
            }
        }
    }
}