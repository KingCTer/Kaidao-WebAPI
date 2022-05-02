using Kaidao.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Services.Api.IdentityServer.Authorization;

public class ClaimRequirementAttribute : TypeFilterAttribute
{
    public ClaimRequirementAttribute(FunctionCode functionId, CommandCode commandId)
        : base(typeof(ClaimRequirementFilter))
    {
        Arguments = new object[] { functionId, commandId };
    }
}
