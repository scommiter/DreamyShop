using Dreamy.Common.Results;
using Dreamy.Domain.Shared.Dtos;
using Dreamy.Domain.Shared.Dtos.Product;
using Dreamy.Logic.Auth.Result;
using Dreamy.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dreamy.Logic.Product
{
    public class ProductLogic : IProductLogic
    {
        private readonly IRepositoryWrapper _repository;

        public ProductLogic(
            IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<ApiResult<PageResult<ProductDto>>> GetAllProductPaging(PagingRequest pagingRequest)
        {
            var productExecuteDtos = await _repository.Product.GetAllProduct(pagingRequest);
            var count = _repository.Product.GetTotalCountProduct();
            var productDtos = ToConvertProductDto(productExecuteDtos.ToList());
            var pageResult = new PageResult<ProductDto>()
            {
                Items = productDtos,
                Totals = 0
            };
            return new ApiSuccessResult<PageResult<ProductDto>>(pageResult);
        }

        public List<ProductDto> ToConvertProductDto(List<ProductExecuteDto> productExecuteDtos)
        {
            var productDtos = new List<ProductDto>();  
            var groupProducts = productExecuteDtos.GroupBy(p => p.Id).ToList();
            foreach (var product in groupProducts)
            {
                var productDto = new ProductDto();
                productDto.Name = product.Select(p => p.Name).FirstOrDefault();
                productDto.Code = product.Select(p => p.Code).FirstOrDefault();
                productDto.ThumbnailPicture = product.Select(p => p.ThumbnailPicture).FirstOrDefault();
                productDto.Description = product.Select(p => p.Description).FirstOrDefault();
                productDto.Quantity = product.Select(p => p.Quantity).Sum();
                var prices = product.Select(p => p.Price);
                productDto.RangPrice = prices.Count() > 1 ? prices.Min() + "-" + prices.Max() + "đ" : prices.FirstOrDefault().ToString() + "đ";
                productDtos.Add(productDto);
            }
            return productDtos;
        }
    }
}
