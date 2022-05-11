using FluentValidation.Results;
using Kaidao.Web.Admin.Authorization;
using Kaidao.Web.Admin.Constants;
using Kaidao.Web.Share.ApiClient.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Kaidao.Web.Admin.Controllers
{
    [Authorize]
    [ClaimRequirement(FunctionCode.ADMIN, CommandCode.ACCESS)]
    public class BaseController : Controller
    {
        private readonly IBaseApiClient _baseApiClient;

        private readonly ICollection<string> _errors = new List<string>();


        public BaseController(IBaseApiClient baseApiClient)
        {
            _baseApiClient = baseApiClient;
        }

        protected bool ResponseHasErrors(ValidationResult result)
        {
            if (result == null || result.IsValid) return false;

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            }

            return true;
        }

        protected void AddProcessError(string erro)
        {
            _errors.Add(erro);
        }

        public bool IsValidOperation()
        {
            return !_errors.Any();
        }

        public override async Task<Task> OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                var data = await _baseApiClient.GetResponseAsync("api/Permissions/check", true);
                switch (data.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        string redirectUri = "/" + context.RouteData.Values["controller"] + "/" +context.RouteData.Values["action"];
                        await HttpContext.SignOutAsync();
                        context.Result = Challenge(new AuthenticationProperties
                        {
                            RedirectUri = redirectUri
                        });
                        break;

                    default:
                        break;
                }
            }
            catch (Exception)
            {

            }
            return base.OnActionExecutionAsync(context, next);
        }

        
    }
}
