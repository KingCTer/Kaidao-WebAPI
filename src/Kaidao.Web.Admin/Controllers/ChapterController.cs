using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Application.Common;
using Kaidao.Application.Responses;
using Kaidao.Application.ViewModels;
using Kaidao.Domain.Constants;
using Kaidao.Web.Admin.Authorization;
using Kaidao.Web.Admin.ViewModels;
using Kaidao.Web.Share.ApiClient.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Web.Admin.Controllers
{
    public class ChapterController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IChapterAppService _chapterAppService;
        private readonly IBookAppService _bookAppService;

        public ChapterController(
            IBaseApiClient baseApiClient,
            IConfiguration configuration,
            IChapterAppService chapterAppService,
            IBookAppService bookAppService
            ) : base(baseApiClient)
        {
            _chapterAppService = chapterAppService;
            _configuration = configuration;
            _bookAppService = bookAppService;
        }


        [HttpGet]
        [ClaimRequirement(FunctionCode.CHAPTER, CommandCode.LIST)]
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

        [HttpGet]
        [ClaimRequirement(FunctionCode.CHAPTER, CommandCode.CREATE)]
        public IActionResult Create(string bookId = "")
        {
            Guid id;
            if (!Guid.TryParse(bookId, out id))
            {
                return RedirectToAction("Index", "Book");
            }

            var lastChapter = _chapterAppService.GetLastChapterByBookId(id);
            var viewModel = new ChapterCreateViewModel
            {
                Book = _bookAppService.GetById(id),
                Order = lastChapter.Order + 1
            };

            return View(viewModel);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ClaimRequirement(FunctionCode.CHAPTER, CommandCode.CREATE)]
        public IActionResult Create([FromForm] ChapterCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Create", new { bookId = viewModel.Book.Id });

            var chapterViewModel = new ChapterViewModel
            {
                BookId = viewModel.Book.Id,
                Order = viewModel.Order,
                Name = viewModel.Name,
                Url = viewModel.Url,
                Content = viewModel.Content
            };
            var result = _chapterAppService.Create(chapterViewModel);
            if (result)
            {
                return RedirectToAction("List", new { bookId = viewModel.Book.Id, bookName = viewModel.Book.Name });
            }

            ModelState.AddModelError("", "Cập nhật sản phẩm thất bại");
            return View(viewModel);

        }

        [HttpGet]
        [ClaimRequirement(FunctionCode.CHAPTER, CommandCode.UPDATE)]
        public IActionResult Edit(string chapterId)
        {
            Guid id;
            if (!Guid.TryParse(chapterId, out id))
            {
                return RedirectToAction("Index", "Book");
            }

            var chapter = _chapterAppService.GetChapterById(id);
            var viewModel = new ChapterUpdateRequest
            {
                Chapter = chapter,
                Book = _bookAppService.GetById(chapter.BookId)
            };

            return View(viewModel);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ClaimRequirement(FunctionCode.CHAPTER, CommandCode.UPDATE)]
        public IActionResult Edit([FromForm] ChapterUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Edit", new { chapterId = request.Chapter.Id });


            var chapterViewModel = new ChapterViewModel
            {
                Id = request.Chapter.Id,
                BookId = request.Book.Id,
                Name = request.Name,
                Order = request.Order,
                Url = request.Url,
                Content = request.Content,
            };

            var result = _chapterAppService.Update(chapterViewModel);
            if (result)
            {
                return RedirectToAction("List", new { bookId = request.Book.Id, bookName = request.Book.Name });
            }

            ModelState.AddModelError("", "Cập nhật chương truyện thất bại");
            return View(request);
        }

        [HttpPost]
        [ClaimRequirement(FunctionCode.CHAPTER, CommandCode.DELETE)]
        public IActionResult Delete(string chapterId, string bookId, string bookName)
        {
            Guid id;
            if (!Guid.TryParse(chapterId, out id))
            {
                return RedirectToAction("Index", "Book");
            }

            _chapterAppService.Remove(id);

            return RedirectToAction("List", new { bookId = bookId, bookName = bookName });
        }
    }
}
