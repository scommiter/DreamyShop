using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Logic.Cart
{
    public interface ICartLogic
    {
        Task<ApiResult<PageResult<CartItemsDto>>> GetAllCart(int userId, PagingRequest pagingRequest);
        Task<ApiResult<bool>> AddToCart(CartAddDto cartAddDto);
        Task<ApiResult<bool>> RemoveFromCart(int userId, int cartDetailId);
    }
}
