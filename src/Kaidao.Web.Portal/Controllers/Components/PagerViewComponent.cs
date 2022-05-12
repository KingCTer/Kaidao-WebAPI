using Kaidao.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Web.Portal.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
