using AutoMapper;
using AutoMapper.QueryableExtensions;
using DreamyShop.Common.Exceptions;
using DreamyShop.Common.Extensions;
using DreamyShop.Common.Results;
using DreamyShop.Domain;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Domain.Shared.Types;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Logic.Conditions;
using DreamyShop.Repository.RepositoryWrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;

namespace DreamyShop.Logic.Product
{
    public class ProductLogic : IProductLogic
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ProductLogic(
            IRepositoryWrapper repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<ApiResult<bool>> CreateProduct(ProductCreateDto productCreateUpdateDto)
        {

            throw new NotImplementedException();
        }

        public async Task<ApiResult<PageResult<ProductDto>>> GetAllProductPaging(PagingRequest pagingRequest)
        {
            var productDynamics = _repository.Product.GetAllProduct().Result;
            var products = productDynamics.GroupBy(
                                    p => p.Id_products,
                                    (key, g) => new { ProductId = key, ProductDTOs = g.ToList() 
                                    });
            var productDtos = productDynamics.GroupBy(p => p.Id_products)
                               .Select(p => new ProductDto
                               {
                                   Id = p.Key,
                                   Name = p.Select(n => n.Name_products).FirstOrDefault() ?? "",
                                   Code = p.Select(n => n.Code_products).FirstOrDefault() ?? "",
                                   ThumbnailPicture = p.Select(n => n.ThumbnailPicture_products).FirstOrDefault() ?? "" ,
                                   ProductType = (ProductType)(p.Select(n => n.ProductType_products).FirstOrDefault()),
                                   CategoryName = p.Select(n => n.Name_productcategories).FirstOrDefault() ?? "",
                                   ManufacturerName = p.Select(n => n.Name_manufacturers).FirstOrDefault() ?? "",
                                   Description = p.Select(n => n.Description_products).FirstOrDefault() ?? "",
                                   IsActive = p.Select(n => n.IsActive_products).FirstOrDefault() ?? false,
                                   IsVisibility = p.Select(n => n.IsVisibility_products).FirstOrDefault() ?? false,
                                   ProductAttributeDisplayDtos = p.GroupBy(pv => pv.ProductVariantId_productvariantvalues).Where(pvi => pvi.Key != null)
                                                               .Select(pAttr => new ProductAttributeDisplayDto
                                                               {
                                                                   AttributeNames = pAttr.Select(x => x.Value_productattributevalues.ToString() ?? "").Cast<string>().ToList(),
                                                                   SKU = pAttr.Select(x => x.SKU_productvariants ?? "").FirstOrDefault(),
                                                                   Quantity = pAttr.Select(x => x.Quantity_productvariants ?? 0).FirstOrDefault(),
                                                                   Price = pAttr.Select(x => x.Price_productvariants ?? 0).FirstOrDefault(),
                                                                   Images = pAttr.Where(x => x.Path_imageproductvariants != null).Select(x => x.Path_imageproductvariants).Cast<string>().ToList()
                                                               }).ToList()
                               }).ToList();
            var pageResult = new PageResult<ProductDto>()
            {
                Items = productDtos.Skip((pagingRequest.Page - 1) * pagingRequest.Limit)
                                .Take(pagingRequest.Limit)
                                .ToList(),
                Totals = productDtos.Count()
            };
            return new ApiSuccessResult<PageResult<ProductDto>>(pageResult);
        }

        public Task<ApiResult<bool>> RemoveProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<IList<ProductDto>>> SearchProduct(SearchProductCondition condition, PagingRequest pagingRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> UpdateProduct(int id, ProductUpdateDto productCreateUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> UploadImage(IFormFile file, int productId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> UploadMultipleImage(List<IFormFile> files, int productId)
        {
            throw new NotImplementedException();
        }
    }
}   