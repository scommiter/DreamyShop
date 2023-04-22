using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Logic.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Logic.Product
{
    public interface IProductLogic
    {
        Task<ApiResult<PageResult<ProductDto>>> GetAllProduct(PagingRequest pagingRequest);
        Task<ApiResult<bool>> CreateProduct(ProductCreateDto productCreateUpdateDto);
        Task<ApiResult<bool>> UpdateProduct(Guid id, ProductUpdateDto productCreateUpdateDto);
        Task<ApiResult<bool>> RemoveProduct(Guid id);
        Task<ApiResult<IList<ProductDto>>> SearchProduct(SearchProductCondition condition, PagingRequest pagingRequest);
    }
}
