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
        Task<ApiResult<ProductDto>> CreateAtributeProduct(ProductAttributeDto productAttributeDto);
    }
}
