using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Domain.Shared.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Logic.Category
{
    public interface ICategoryLogic
    {
        Task<ApiResult<PageResult<CategoryDto>>> GetAllCategory(PagingRequest pagingRequest);
        Task<ApiResult<CategoryDto>> CreateCategory(CategoryCreateUpdateDto categoryCreateUpdateDto);
        Task<ApiResult<CategoryDto>> UpdateCategory(int id, CategoryCreateUpdateDto categoryCreateUpdateDto);
        Task<ApiResult<bool>> RemoveCategory(int id);
    }
}
