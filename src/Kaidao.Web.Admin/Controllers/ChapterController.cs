using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Application.Common;
using Kaidao.Application.Responses;
using Kaidao.Web.Admin.ViewModels;
using Kaidao.Web.Share.ApiClient.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Web.Admin.Controllers
{
    public class ChapterController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IChapterAppService _chapterAppService;

        public ChapterController(
            IBaseApiClient baseApiClient,
            IConfiguration configuration,
            IChapterAppService chapterAppService
            ) : base(baseApiClient)
        {
            _chapterAppService = chapterAppService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult List(string bookId = "", int pageIndex = 1, int pageSize = 50, string keyword = "", string bookName = "")
        {
            Guid id;
            if (!Guid.TryParse(bookId, out id))
            {
                return RedirectToAction("Index", "Book");
            }
            var queryChapter = "orderBy:order:desc;";
            if (keyword != "")
            {
                queryChapter += "where:keyword=" + keyword;
            }

            var chapterList = _chapterAppService.GetChapterListByBookId(id, (pageIndex - 1) * pageSize, pageSize, queryChapter);
            var pagedResult = new PagedResult<ChapterResponse>()
            {
                TotalRecords = chapterList.TotalRecords,
                Items = chapterList.ViewModel.ToList(),
                PageSize = pageSize,
                PageIndex = pageIndex
            };

            var viewModel = new ChapterListViewModel
            {
                ChapterPagedResult = pagedResult,
                BookName = bookName,
                BookId = bookId
            };

            ViewBag.Keyword = keyword;
            ViewBag.PortalAddress = _configuration["PortalAddress"];

            return View(viewModel);
        }
    }
}
