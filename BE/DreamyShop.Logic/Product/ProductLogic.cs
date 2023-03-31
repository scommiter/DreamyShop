using AutoMapper;
using AutoMapper.QueryableExtensions;
using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.EntityFrameworkCore;
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

        public async Task<ApiResult<ProductDto>> CreateProduct(ProductCreateUpdateDto productCreateUpdateDto)
        {
            throw new NotImplementedException();
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
