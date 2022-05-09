using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Core.Notifications;
using Kaidao.Services.Api.Controllers.Base;
using Kaidao.Services.Api.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Duende.IdentityServer.IdentityServerConstants;

namespace Kaidao.Services.Api.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    public class UsersController : BaseController
    {
        private readonly IUserAppService _userAppService;

        public UsersController(
            INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator,

            IUserAppService userAppService

        ) : base(notifications, mediator)
        {
            _userAppService = userAppService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var bookViewModels = _userAppService.GetAll();
            return Response(bookViewModels, bookViewModels.Count());
        }

        [HttpGet("pagination")]
        public IActionResult GetAllPagination([FromQuery] PaginationFilter filter)
        {
            var userResponses = _userAppService.GetAll((filter.PageNumber - 1) * filter.PageSize, filter.PageSize, filter.Query);
            return PagedResponse(userResponses.ViewModel, userResponses.TotalRecords, filter);
        }
    }
}
