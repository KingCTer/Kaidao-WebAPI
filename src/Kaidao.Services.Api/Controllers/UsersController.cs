using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Application.ViewModels;
using Kaidao.Domain.Constants;
using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Core.Notifications;
using Kaidao.Domain.IdentityEntity;
using Kaidao.Services.Api.Controllers.Base;
using Kaidao.Services.Api.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Duende.IdentityServer.IdentityServerConstants;

namespace Kaidao.Services.Api.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    public class UsersController : BaseController
    {
        private readonly IUserAppService _userAppService;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(
            INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator,

            IUserAppService userAppService,
            UserManager<AppUser> userManager

        ) : base(notifications, mediator)
        {
            _userAppService = userAppService;
            _userManager = userManager;
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appUser = await _userManager.FindByNameAsync(request.UserName);
            if (appUser != null)
            {
                return Ok(false);
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return Ok(false);
            }

            appUser = new AppUser()
            {
                Dob = request.Dob,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(appUser, request.Password);
            if (result.Succeeded)
            {
                var user = _userManager.FindByNameAsync(appUser.UserName).Result;
                result = _userManager.AddToRoleAsync(user, IdentityConstant.Roles.User).Result;
            }
            if (!result.Succeeded)
            {
                return Ok(false);
            }
            return Ok(true);

        }
    }
}
