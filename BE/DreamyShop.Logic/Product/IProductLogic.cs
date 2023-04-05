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
        #region Product
        Task<ApiResult<PageResult<ProductDto>>> GetAllProduct(int page, int limit);
        Task<ApiResult<ProductDto>> CreateProduct(ProductCreateUpdateDto productCreateUpdateDto);
        Task<ApiResult<ProductDto>> UpdateProduct(Guid id, ProductCreateUpdateDto productCreateUpdateDto);
        Task<ApiResult<bool>> RemoveProduct(Guid id);
        Task<ApiResult<IList<ProductDto>>> SearchProduct(SearchProductCondition condition);
        #endregion

        #region ProductAttributeValue
        Task<ApiResult<PageResult<ProductAttributeValueDto>>> GetListProductAttributeValue(Guid productId);
        Task<ApiResult<bool>> CreateAtributeValueProduct(CreateProductAttributeValueDto productAttributeDto);
        Task<ApiResult<ProductAttributeValueDto>> UpdateProductAttributeValue(Guid id, CreateProductAttributeValueDto updateProductAttributeDto);
        Task<ApiResult<bool>> RemoveProductAttributeValue(Guid attributeId, Guid attributeTypeId);
        #endregion
    }
}
