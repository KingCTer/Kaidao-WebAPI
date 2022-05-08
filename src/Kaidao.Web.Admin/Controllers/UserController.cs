using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Application.Common;
using Kaidao.Application.ViewModels;
using Kaidao.Web.Admin.Authorization;
using Kaidao.Web.Admin.Constants;
using Kaidao.Web.Share.ApiClient.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Web.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IBaseApiClient _baseApiClient;

        private readonly IUserAppService _userAppService;

        public UserController(IBaseApiClient baseApiClient, IUserAppService userAppService) : base(baseApiClient)
        {
            _baseApiClient = baseApiClient;
            _userAppService = userAppService;
        }

        [ClaimRequirement(FunctionCode.SYSTEM, CommandCode.CREATE)]
        public IActionResult Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var data = _userAppService.GetAll((pageIndex - 1) * pageSize, pageSize, keyword);

            var pagedResult = new PagedResult<UserViewModel>()
            {
                TotalRecords = data.TotalRecords,
                Items = data.ViewModel.ToList(),
                PageSize = pageSize,
                PageIndex = pageIndex
            };

            ViewBag.Keyword = keyword;
            ViewBag.PageSize = pageSize;
            ViewBag.PageIndex = pageIndex;
            return View(pagedResult);
        }

        [ClaimRequirement(FunctionCode.SYSTEM, CommandCode.CREATE)]
        public async Task<IActionResult> Test()
        {

            var bookViewModels = _userAppService.GetAll();
            return Ok(bookViewModels);
        }
    }
}
