using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Logic.Conditions;
using Microsoft.AspNetCore.Http;

namespace DreamyShop.Logic.Product
{
    public interface IProductLogic
    {
        Task<ApiResult<PageResult<ProductDto>>> GetAllProduct(PagingRequest pagingRequest);
        Task<ApiResult<bool>> CreateProduct(ProductCreateDto productCreateUpdateDto);
        Task<ApiResult<bool>> UpdateProduct(Guid id, ProductUpdateDto productCreateUpdateDto);
        Task<ApiResult<bool>> RemoveProduct(Guid id);
        Task<ApiResult<IList<ProductDto>>> SearchProduct(SearchProductCondition condition, PagingRequest pagingRequest);
        Task<ApiResult<bool>> UploadImage(IFormFile file, Guid productId);
        Task<ApiResult<bool>> UploadMultipleImage(List<IFormFile> files, Guid productId);
    }
}
