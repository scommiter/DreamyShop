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
        Task<ApiResult<PageResult<CartItemsDto>>> GetAllCart(PagingRequest pagingRequest);
        //Task<ApiResult<CategoryDto>> CreateCategory(CategoryCreateUpdateDto categoryCreateUpdateDto);
        //Task<ApiResult<CategoryDto>> UpdateCategory(int id, CategoryCreateUpdateDto categoryCreateUpdateDto);
        //Task<ApiResult<bool>> RemoveCategory(int id);
    }
}
