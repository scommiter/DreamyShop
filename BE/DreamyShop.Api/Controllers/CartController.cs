using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Logic.Cart;
using Microsoft.AspNetCore.Mvc;

namespace DreamyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartLogic _cartService;
        private readonly ILogger<CategoryController> _logger;
        public CartController(
            ICartLogic cartService,
            ILogger<CategoryController> logger)
        {
            _logger = logger;
            _cartService = cartService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCart([FromForm] PagingRequest pagingRequest, int userId)
        {
            var result = await _cartService.GetAllCart(userId, pagingRequest);
            return Ok(result.Result);
        }

        [HttpPost("addToCart")]
        public async Task<IActionResult> AddToCart([FromForm] CartAddDto cartAddDto)
        {
            var result = await _cartService.AddToCart(cartAddDto);
            return Ok(result.Result);
        }

        [HttpDelete("deleteFromCart")]
        public async Task<IActionResult> DeleteFromCart(int cartDetailId, int userId)
        {
            var result = await _cartService.RemoveFromCart(userId, cartDetailId);
            return Ok(result.Result);
        }

    }
}
