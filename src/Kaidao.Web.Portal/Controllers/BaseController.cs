using Kaidao.Web.Share.ApiClient.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Kaidao.Web.Portal.Controllers
{
    public class BaseController : Controller
    {
        private readonly IBaseApiClient _baseApiClient;

        public BaseController(IBaseApiClient baseApiClient)
        {
            _baseApiClient = baseApiClient;
        }

        public override async Task<Task> OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return base.OnActionExecutionAsync(context, next);
            }
            try
            {
                
                var data = await _baseApiClient.GetResponseAsync("api/Permissions/check", true);
                switch (data.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        string redirectUri = "/" + context.RouteData.Values["controller"] + "/" + context.RouteData.Values["action"];

                        var routeUrl = Url.RouteUrl(context.RouteData.Values);
                        var qsPath = context.HttpContext.Request.QueryString.Value;
                        var returnUrl = $"{routeUrl}{qsPath}";

                        await HttpContext.SignOutAsync();
                        context.Result = Challenge(new AuthenticationProperties
                        {
                            RedirectUri = returnUrl
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
