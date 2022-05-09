using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Core.Notifications;
using Kaidao.Services.Api.Controllers.Base;
using Kaidao.Services.Api.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Services.Api.Controllers
{
    [AllowAnonymous]
    public class BookController : BaseController
    {
        private readonly IBookAppService _bookAppService;

        public BookController(
            IBookAppService bookAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator
            ) : base(notifications, mediator)
        {
            _bookAppService = bookAppService;
        }

        [HttpPost("Crawl")]
        public IActionResult Crawl(string url)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(url);
            }

            _bookAppService.Crawl(url);

            return Response(url);
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var bookResponse = _bookAppService.GetById(id);

            if (bookResponse == null)
            {
                return Response(bookResponse, 0);
            }

            return Response(bookResponse, 1);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var bookViewModels = _bookAppService.GetAll();
            return Response(bookViewModels, bookViewModels.Count());
        }

        [HttpGet("pagination")]
        public IActionResult Paginationt([FromQuery] PaginationFilter filter)
        {
            var booksResponses = _bookAppService.GetAll((filter.PageNumber - 1) * filter.PageSize, filter.PageSize, filter.Query);
            return PagedResponse(booksResponses.ViewModel, booksResponses.TotalRecords, filter);
        }
    }
}