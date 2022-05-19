using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Web.Portal.Models;
using Kaidao.Web.Portal.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kaidao.Web.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IBookAppService _bookAppService;

        public HomeController(ILogger<HomeController> logger, IBookAppService bookAppService)
        {
            _logger = logger;
            _bookAppService = bookAppService;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                NewlyBooks = _bookAppService.GetAll(0, 12, "orderBy:key:desc"),
                FavoriteBooks = _bookAppService.GetAll(0, 12, "orderBy:like:desc"),
                PopularBook = _bookAppService.GetAll(0, 12, "orderBy:view:desc")
            };


            return View(viewModel);
        }

        public IActionResult Login(string returnUrl)
        {

            return Challenge(new AuthenticationProperties
            {
                RedirectUri = returnUrl
            });
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return SignOut("oidc");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}