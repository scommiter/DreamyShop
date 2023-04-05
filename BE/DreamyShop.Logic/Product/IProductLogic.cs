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
        Task<ApiResult<PageResult<ProductDto>>> GetAllProduct(PagingRequest pagingRequest);
        Task<ApiResult<ProductDto>> CreateProduct(ProductCreateUpdateDto productCreateUpdateDto);
        Task<ApiResult<ProductDto>> UpdateProduct(Guid id, ProductCreateUpdateDto productCreateUpdateDto);
        Task<ApiResult<bool>> RemoveProduct(Guid id);
        Task<ApiResult<IList<ProductDto>>> SearchProduct(SearchProductCondition condition);
        #endregion

        #region ProductAttribute
        Task<ApiResult<PageResult<ProductAttributeDto>>> GetListProductAttribute(PagingRequest pagingRequest);
        Task<ApiResult<ProductAttributeDto>> CreateAtributeProduct(CreateProductAttributeDto productAttributeDto);
        Task<ApiResult<ProductAttributeDto>> UpdateProductAttribute(Guid id, CreateProductAttributeDto updateProductAttributeDto);
        Task<ApiResult<bool>> RemoveProductAttribute(Guid attributeId);
        #endregion

        #region ProductAttributeValue
        Task<ApiResult<PageResult<ProductAttributeValueDto>>> GetListProductAttributeValue(Guid productId, PagingRequest pagingRequest);
        Task<ApiResult<bool>> CreateAtributeValueProduct(CreateProductAttributeValueDto productAttributeDto);
        Task<ApiResult<ProductAttributeValueDto>> UpdateProductAttributeValue(Guid id, CreateProductAttributeValueDto updateProductAttributeDto);
        Task<ApiResult<bool>> RemoveProductAttributeValue(Guid attributeId, Guid attributeTypeId);
        #endregion
    }
}
