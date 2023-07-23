using Dreamy.Common.Results;
using Dreamy.Domain.Shared.Dtos;
using Dreamy.Domain.Shared.Dtos.Product;

namespace Dreamy.Logic.Product
{
    public interface IProductLogic
    {
        Task<ApiResult<PageResult<ProductDto>>> GetAllProductPaging(PagingRequest pagingRequest);
    }
}
