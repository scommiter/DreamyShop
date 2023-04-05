using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
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
        Task<ApiResult<CategoryDto>> UpdateCategory(Guid id, CategoryCreateUpdateDto categoryCreateUpdateDto);
        Task<ApiResult<bool>> RemoveCategory(Guid id);
    }
}
