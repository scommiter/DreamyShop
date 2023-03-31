using AutoMapper;
using AutoMapper.QueryableExtensions;
using DreamyShop.Common.Exceptions;
using DreamyShop.Common.Results;
using DreamyShop.Domain;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Domain.Shared.Types;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Logic.Auth.Result;
using DreamyShop.Repository.RepositoryWrapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Logic.Product
{
    public class ProductLogic : IProductLogic
    {
        private readonly DreamyShopDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ProductLogic(
            DreamyShopDbContext context,
            IRepositoryWrapper repository,
            IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<ProductDto>> CreateAtributeProduct(ProductAttributeDto productAttributeDto)
        {
            var product = _repository.Product.GetByIdAsync(productAttributeDto.ProductId);
            if (product == null)
            {
                return new ApiErrorResult<ProductDto>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var attribute = await _repository.ProductAttribute.GetByIdAsync(productAttributeDto.AttributeId);
            if (attribute == null)
                return new ApiErrorResult<ProductDto>((int)ErrorCodes.DataEntryIsNotExisted);
            var attributeDto = _mapper.Map<ProductAttributeDto>(attribute);
            var newAttributeId = Guid.NewGuid();
            switch (attributeDto.DataType)
            {
                case AttributeType.Date:
                    if (productAttributeDto.DateTimeValue == null)
                    {
                        return new ApiErrorResult<ProductDto>((int)ErrorCodes.DataEntryIsNotValid);
                    }
                    var productAttributeDateTime = new ProductAttributeDateTime(newAttributeId, productAttributeDto.AttributeId, productAttributeDto.ProductId, productAttributeDto.DateTimeValue);
                    await _repository.ProductAttribute.AddAsync(productAttributeDateTime);
                    break;
                case AttributeType.Int:
                    
                case AttributeType.Decimal:
                    
                case AttributeType.Varchar:
                    
                case AttributeType.Text:
                    
            }
        }

        public async Task<ApiResult<ProductDto>> CreateProduct(ProductCreateUpdateDto productCreateUpdateDto)
        {
            var newProduct = _mapper.Map<Domain.Product>(productCreateUpdateDto);
            await _repository.Product.AddAsync(newProduct);
            return new ApiSuccessResult<ProductDto>(_mapper.Map<ProductDto>(newProduct));
        }

        public async Task<ApiResult<PageResult<ProductDto>>> GetAllProduct(int page, int limit)
        {
            var productPagings = _context.Products
                                .Include(opt => opt.Manufacturer)
                                .Include(opt => opt.ProductCategory)
                                .OrderByDescending(p => p.DateCreated)
                                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                .Skip((page - 1) * limit)
                                .Take(limit)
                                .ToList();
            var pageResult = new PageResult<ProductDto>()
            {
                Items = productPagings,
                Totals = productPagings.Count()
            };
            return new ApiSuccessResult<PageResult<ProductDto>>(pageResult);
        }
    }
}
