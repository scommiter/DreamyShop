using DREAMYMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DREAMYMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string token = HttpContext.Session.GetString("token");
            return View();
        }
    }
}