using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Web.Portal.Controllers
{
    public class ChapterController : Controller
    {
        private readonly IChapterAppService _chapterAppService;
		private readonly IBookAppService _bookAppService;

		public ChapterController(IChapterAppService chapterAppService, IBookAppService bookAppService)
		{
			_chapterAppService = chapterAppService;
			_bookAppService = bookAppService;
		}

		public IActionResult Index(Guid bookId, Guid chapterId, int chapterOrder = 1)
        {
			var chapterResponse = new ChapterResponse();
			if (chapterId == Guid.Empty)
			{
				chapterResponse = _chapterAppService.GetChapterByBookIdAndOrder(bookId, chapterOrder);
			}
			else if (bookId == Guid.Empty)
			{
				return View(chapterResponse);
			} 
			else
			{
				chapterResponse = _chapterAppService.GetChapterById(chapterId);
			}

			if (chapterResponse == null)
			{
				return View(chapterResponse);
			}

			if (chapterResponse.Content == "" && chapterResponse.Url != null)
			{
				chapterResponse.Content = _chapterAppService.CrawlContent(chapterResponse);
			}

			ViewBag.BookName = _bookAppService.GetById(chapterResponse.BookId).Name;

			return View(chapterResponse);
        }
    }
}
