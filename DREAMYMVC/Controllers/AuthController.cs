using Dreamy.Domain.Shared.Dtos.Auth;
using Dreamy.Logic.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DREAMYMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthLogic _authLogic;
        public AuthController(IAuthLogic authLogic)
        {
            _authLogic = authLogic;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginDto request)
        {
            if (!ModelState.IsValid)
                return View(ModelState);
            var result = _authLogic.Login(request);
            if (result.Result.Code != 0)
            {
                ModelState.AddModelError("", result.Result.Message);
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
