﻿using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Core.Notifications;
using Kaidao.Services.Api.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Duende.IdentityServer.IdentityServerConstants;

namespace Kaidao.Services.Api.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;

        public BaseController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

        protected bool IsValidOperation()
        {
            return !_notifications.HasNotifications();
        }

        protected new IActionResult Response(object result = null, int totalRecords = 0)
        {
            if (IsValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    totalRecords,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                message = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected IActionResult PagedResponse(object result = null, int totalRecords = -1, PaginationFilter filter = null)
        {
            if (IsValidOperation())
            {
                var tempTotalPages = filter.PageSize == 0 ? 0 : Convert.ToInt32(Math.Ceiling((double)totalRecords / (double)filter.PageSize));
                return Ok(new
                {
                    success = true,
                    pageNumber = filter.PageNumber,
                    pageSize = filter.PageSize,
                    totalPages = tempTotalPages,
                    totalRecords,
                    data = result,
                    message = filter.Query
                });
            }

            return BadRequest(new
            {
                success = false,
                message = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected void NotifyModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected void NotifyError(string code, string message)
        {
            _mediator.RaiseEvent(new DomainNotification(code, message));
        }

        protected void AddIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                NotifyError(result.ToString(), error.Description);
            }
        }
    }
}
