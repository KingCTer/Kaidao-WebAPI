using Kaidao.Web.Share.ApiClient.Interfaces;
using Kaidao.Web.Share.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Web.Portal.Controllers
{
    [AllowAnonymous]
	public class AccountController : Controller
	{
		private readonly IBaseApiClient _baseApiClient;

		public AccountController(
			IBaseApiClient baseApiClient
			)
		{
			_baseApiClient = baseApiClient;
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(RegisterRequest registerRequest, string returnUrl = "/")
		{
			if (!ModelState.IsValid)
			{
				return View(registerRequest);
			}

			var result = _baseApiClient.RegisterUser(registerRequest).Result;
			if (!result)
			{
				ModelState.AddModelError("", "Lỗi");
				return View();
			}

			return Challenge(new AuthenticationProperties
			{
				RedirectUri = returnUrl
			});

			

		}

	}
}
