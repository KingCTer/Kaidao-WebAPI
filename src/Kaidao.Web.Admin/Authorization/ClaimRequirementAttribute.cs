﻿using Kaidao.Web.Admin.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Web.Admin.Authorization
{
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(FunctionCode functionId, CommandCode commandId)
            : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { functionId, commandId };
        }
    }
}
