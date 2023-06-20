using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Domain.Shared.Dtos.Product;
using DreamyShop.Logic.Conditions;
using Microsoft.AspNetCore.Http;

namespace DreamyShop.Logic.Product
{
    public interface IProductLogic
    {
        Task<ApiResult<PageResult<ProductDto>>> GetAllProductPaging(PagingRequest pagingRequest);
        Task<ApiResult<PageResult<ProductDto>>> GetAllProduct();
        Task<ApiResult<bool>> CreateProduct(ProductCreateDto productCreateUpdateDto);
        Task<ApiResult<bool>> UpdateProduct(int id, ProductUpdateDto productCreateUpdateDto);
        Task<ApiResult<bool>> RemoveProduct(int id);
        Task<ApiResult<IList<ProductDto>>> SearchProduct(SearchProductCondition condition, PagingRequest pagingRequest);
        Task<ApiResult<bool>> UploadImage(IFormFile file, int productId);
        Task<ApiResult<bool>> UploadMultipleImage(List<IFormFile> files, int productId);
        Task<ApiResult<bool>> ImportProducts(List<ProductCreateDto> productCreateDto);
    }
}
