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
        Task<ApiResult<PageResult<ProductDto>>> GetAllProduct(int page, int limit);
        Task<ApiResult<ProductDto>> CreateProduct(ProductCreateUpdateDto productCreateUpdateDto);
        Task<ApiResult<ProductDto>> UpdateProduct(Guid id, ProductCreateUpdateDto productCreateUpdateDto);
        Task<ApiResult<bool>> RemoveProduct(Guid id);

        Task<ApiResult<PageResult<ProductAttributeDto>>> GetListProductAttribute(Guid productId);
        Task<ApiResult<bool>> CreateAtributeProduct(CreateProductAttributeDto productAttributeDto);
        Task<ApiResult<ProductAttributeDto>> UpdateProductAttributeAsync(Guid id, CreateProductAttributeDto updateProductAttributeDto);
        Task<ApiResult<bool>> RemoveAtributeProduct(Guid attributeId, Guid attributeTypeId);

        Task<ApiResult<IList<ProductDto>>> SearchProduct(SearchProductCondition condition);
    }
}
