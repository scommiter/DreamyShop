using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
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
        Task<ApiResult<PageResult<ProductAttributeDto>>> GetListProductAttribute(Guid productId);
        Task<ApiResult<bool>> CreateAtributeProduct(ProductAttributeDto productAttributeDto);
        Task<ApiResult<bool>> RemoveAtributeProduct(Guid attributeId, Guid attributeTypeId);
    }
}
