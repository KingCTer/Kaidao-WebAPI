using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Application.Responses;
using Kaidao.Domain.Constants;
using Kaidao.Web.Portal.Authorization;
using Kaidao.Web.Share.ApiClient.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Web.Portal.Controllers
{
    public class ChapterController : BaseController
    {
        private readonly IChapterAppService _chapterAppService;
		private readonly IBookAppService _bookAppService;

		public ChapterController(
			IBaseApiClient baseApiClient, 
			IChapterAppService chapterAppService, 
			IBookAppService bookAppService
			) : base(baseApiClient)
		{
			_chapterAppService = chapterAppService;
			_bookAppService = bookAppService;
		}


		[Authorize]
		[ClaimRequirement(FunctionCode.CHAPTER, CommandCode.READ)]
		public IActionResult Index(Guid bookId, Guid chapterId, int chapterOrder = 1)
        {
			var chapterResponse = new ChapterResponse();
            if (chapterId != Guid.Empty)
            {
				chapterResponse = _chapterAppService.GetChapterById(chapterId);
            }
            else if (bookId != Guid.Empty)
            {
				chapterResponse = _chapterAppService.GetChapterByBookIdAndOrder(bookId, chapterOrder);
			} else
            {
				return View(chapterResponse);
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
