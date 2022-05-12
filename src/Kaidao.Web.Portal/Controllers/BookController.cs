using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Application.Common;
using Kaidao.Application.Responses;
using Kaidao.Web.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Web.Portal.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookAppService _bookAppService;
        private readonly IChapterAppService _chapterAppService;

        public BookController(IBookAppService bookAppService, IChapterAppService chapterAppService)
        {
            _bookAppService = bookAppService;
            _chapterAppService = chapterAppService;
        }

        public IActionResult Detail(string bookId, int pageIndex = 1, int pageSize = 20)
        {
            Guid id;
            if (!Guid.TryParse(bookId, out id))
            {
                return RedirectToAction("Index", "Home");
            }

            var book = _bookAppService.GetById(id);
            var queryCategory = "where:categoryName=" + book.CategoryName + ";where:name!" + book.Name;
            var queryAuthor = "where:authorName=" + book.AuthorName + ";where:name!" + book.Name;
            var queryChapter = "orderBy:order:asc;";

            var chapterList = _chapterAppService.GetChapterListByBookId(id, (pageIndex - 1) * pageSize, pageSize, queryChapter);
            var pagedResult = new PagedResult<ChapterResponse>()
            {
                TotalRecords = chapterList.TotalRecords,
                Items = chapterList.ViewModel.ToList(),
                PageSize = pageSize,
                PageIndex = pageIndex
            };

            var viewModel = new BookDetailViewModel
            {
                CurrentBook = book,
                CategoryBooks = _bookAppService.GetAll(0, 5, queryCategory),
                AuthorBooks = _bookAppService.GetAll(0, 6, queryAuthor),
                ChapterPagedResult = pagedResult
            };

            return View(viewModel);
        }
    }
}
