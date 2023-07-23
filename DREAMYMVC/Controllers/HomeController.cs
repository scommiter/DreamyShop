using Dreamy.Domain.Shared.Dtos;
using Dreamy.Logic.Auth;
using Dreamy.Logic.Product;
using DREAMYMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DREAMYMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductLogic _productLogic;

        public HomeController(IProductLogic productLogic)
        {
            _productLogic = productLogic;
        }

        public async Task<IActionResult> Index()
        {
            var productPagings = await _productLogic.GetAllProductPaging(new PagingRequest { Limit = 8, Page = 1});
            return View(productPagings.Result);
        }
    }
}