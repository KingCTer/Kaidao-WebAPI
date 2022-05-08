using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Core.Notifications;
using Kaidao.Services.Api.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Services.Api.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator) : base(notifications, mediator)
        {
        }
    }
}
