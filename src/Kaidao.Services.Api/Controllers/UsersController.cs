using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Core.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Services.Api.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(DomainNotificationHandler notifications, IMediatorHandler mediator) : base(notifications, mediator)
        {
        }
    }
}
