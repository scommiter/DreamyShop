using AutoMapper;
using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.RepositoryWrapper;
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
            var productPagings = (from p in _context.Products
                                  join m in _context.Manufacturers
                                  on p.ManufacturerId equals m.Id
                                  join c in _context.ProductCategories
                                  on p.CategoryId equals c.Id
                                  select new ProductDto
                                  {
                                      Id = p.Id,
                                      Name = p.Name,
                                      Code = p.Code,
                                      ThumbnailPicture = p.ThumbnailPicture,
                                      Price = p.Price,
                                      ProductType = p.ProductType,
                                      CategoryName = c.Name,
                                      ManufacturerName = p.Name,
                                      Description = p.Description,
                                      IsActive = p.IsActive,
                                      DateCreated = p.DateCreated,
                                      DateUpdated = p.DateUpdated
                                  })
                                  .Skip((page - 1) * limit)
                                  .Take(limit)
                                  .OrderByDescending(u => u.DateCreated)
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
