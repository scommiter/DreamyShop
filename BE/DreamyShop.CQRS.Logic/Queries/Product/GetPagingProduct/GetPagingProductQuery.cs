using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos.Product;
using MediatR;

namespace DreamyShop.CQRS.Logic.Queries.Product.GetPagingProduct
{
    public class GetPagingProductQuery : IRequest<ApiResult<PageResult<ProductDto>>>
    {
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
