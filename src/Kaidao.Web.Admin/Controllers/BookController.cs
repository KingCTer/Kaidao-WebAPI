using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Application.Common;
using Kaidao.Application.Responses;
using Kaidao.Application.ViewModels;
using Kaidao.Web.Share.ApiClient.Interfaces;
using Kaidao.Web.Share.Query;
using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Web.Admin.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookAppService _bookAppService;

        public BookController(IBaseApiClient baseApiClient, IBookAppService bookAppService) : base(baseApiClient)
        {
            _bookAppService = bookAppService;
        }

        public IActionResult Index(PaginationFilter filter, string queryType = "")
        {
            var query = "";
            if (queryType != "")
            {
                query = queryType + filter.Query;
            }

            var data = _bookAppService.GetAll((filter.PageNumber - 1) * filter.PageSize, filter.PageSize, query);

            var pagedResult = new PagedResult<BookResponse>()
            {
                TotalRecords = data.TotalRecords,
                Items = data.ViewModel.ToList(),
                PageSize = filter.PageSize,
                PageIndex = filter.PageNumber
            };

            ViewBag.Keyword = filter.Query;

            return View(pagedResult);
        }
    }
}
