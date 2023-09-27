using DreamyShop.Common.Caches;
using DreamyShop.Common.Constants;
using DreamyShop.Common.Results;
using DreamyShop.Domain;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Domain.Shared.Dtos.Category;
using DreamyShop.Domain.Shared.Dtos.Product;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.RepositoryWrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DreamyShop.CQRS.Logic.Queries.Product.GetPagingProduct
{
    public class GetPagingProductQueryHandler : IRequestHandler<GetPagingProductQuery, ApiResult<PageResult<ProductDto>>>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IRedisCacheService _productCaches;
        private readonly DreamyShopDbContext _context;
        public GetPagingProductQueryHandler(IRepositoryWrapper repository, IRedisCacheService productCaches, DreamyShopDbContext context)
        {
            _repository = repository;
            _productCaches = productCaches;
            _context = context;
        }

        public async Task<ApiResult<PageResult<ProductDto>>> Handle(GetPagingProductQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await _context.Products.CountAsync();

            var keyCache = ConstantCaches.PRODUCTCACHES.ToString() + "p" + request.Page + "l" + request.Limit;
            var productCaches = _productCaches.GetCachedData<List<ProductDto>>(keyCache);
            if (productCaches == null)
            {
                var attributeProducts = _context.Attributes.Join(_context.ProductAttributes,
                                        a => a.Id,
                                        b => b.AttributeId,
                                        (a, b) => new
                                        {
                                            AttributeName = a.Name,
                                            ProductId = b.ProductId
                                        });

                //var queryLambda = _context.Products.Join(_context.Manufacturers,
                //                          a => a.ManufacturerId,
                //                          m => m.Id,
                //                          (a, m) => new { a, m })
                //                    .Join(_context.ProductCategories,
                //                          am => am.a.CategoryId,
                //                          pc => pc.Id,
                //                          (am, pc) => new { am.a, am.m, pc })
                //                    .GroupJoin(_context.ProductVariants,
                //                               ampc => ampc.a.Id,
                //                               b => b.ProductId,
                //                               (ampc, b) => new { ampc.a, ampc.m, ampc.pc, b })
                //                    .SelectMany(ampcb => ampcb.b.DefaultIfEmpty(),
                //                                (ampcb, b) => new { ampcb.a, ampcb.m, ampcb.pc, b })
                //                    .GroupJoin(_context.ProductVariantValues,
                //                               ampcb => ampcb.b!.Id,
                //                               c => c.ProductVariantId,
                //                               (ampcb, c) => new { ampcb.a, ampcb.m, ampcb.pc, ampcb.b, c })
                //                    .SelectMany(ampcbc => ampcbc.c.DefaultIfEmpty(),
                //                                (ampcbc, c) => new { ampcbc.a, ampcbc.m, ampcbc.pc, ampcbc.b, c })
                //                    .GroupJoin(_context.ProductAttributeValues,
                //                               ampcbc => ampcbc.c!.ProductAttributeValueId,
                //                               d => d.Id,
                //                               (ampcbc, d) => new { ampcbc.a, ampcbc.m, ampcbc.pc, ampcbc.b, ampcbc.c, d })
                //                    .SelectMany(ampcbcd => ampcbcd.d.DefaultIfEmpty(),
                //                                (ampcbcd, d) => new
                //                                {
                //                                    ampcbcd.a.Id,
                //                                    ampcbcd.a.Name,
                //                                    ManufacturerName = ampcbcd.m.Name,
                //                                    CategoryName = ampcbcd.pc.Name,
                //                                    ampcbcd.a.Code,
                //                                    ampcbcd.a.Slug,
                //                                    ProductVariantId = ampcbcd.b!.Id,
                //                                    ampcbcd.b.SKU,
                //                                    ampcbcd.b.Quantity,
                //                                    ampcbcd.b.Price,
                //                                    ampcbcd.c!.ProductAttributeValueId,
                //                                    d!.Value
                //                                });


                var query = (from p in _context.Products
                             join m in _context.Manufacturers on p.ManufacturerId equals m.Id
                             join c in _context.ProductCategories on p.CategoryId equals c.Id
                             join pv in _context.ProductVariants on p.Id equals pv.ProductId into pvN
                             from pv in pvN.DefaultIfEmpty()
                             join ip in _context.ImageProducts on p.Id equals ip.ProductId into ipvN
                             from ip in ipvN.DefaultIfEmpty()
                             join pvv in _context.ProductVariantValues on pv.Id equals pvv.ProductVariantId into pvvN
                             from pvv in pvvN.DefaultIfEmpty()
                             join pav in _context.ProductAttributeValues on pvv.ProductAttributeValueId equals pav.Id into pavN
                             from pav in pavN.DefaultIfEmpty()
                             select new
                             {
                                 Product = p,
                                 ManufacturerName = m.Name,
                                 CategoryName = c.Name,
                                 pvv,
                                 pv,
                                 pav,
                                 ip
                             });

                var groupedQuery = query.OrderByDescending(p => p.Product.DateCreated)
                .GroupBy(item => item.Product.Id)
                .Skip((request.Page - 1) * request.Limit).Take(request.Limit)
                .Select(group => new
                {
                                            ProductId = group.Key,
                                            Product = group.FirstOrDefault().Product,
                                            ManufacturerName = group.Select(item => item.ManufacturerName).FirstOrDefault(),
                                            CategoryName = group.Select(item => item.CategoryName).FirstOrDefault(),
                                            pvv = group.Where(item => item.pvv != null).Select(item => item.pvv).Distinct(),
                                            pv = group.Where(item => item.pv != null).Select(item => item.pv).Distinct(),
                                            pav = group.Where(item => item.pav != null).Select(item => item.pav).Distinct(),
                                            ip = group.Where(item => item.ip != null).Select(item => item.ip).Distinct()
                                        });

                var productsPaging = await groupedQuery.ToListAsync();

                productCaches = productsPaging.Select(x => new ProductDto
                {
                    Id = x.ProductId,
                    Name = x.Product.Name,
                    Code = x.Product.Code,
                    ThumbnailPictures = x.ip.GroupBy(p => p.ProductId).Select(pt => pt.Select(ptt => ptt.Path ?? "").FirstOrDefault()).ToList(),
                    ProductType = x.Product.ProductType,
                    CategoryName = x.CategoryName ?? "",
                    ManufacturerName = x.ManufacturerName ?? "",
                    Description = x.Product.Description ?? "",
                    IsActive = x.Product.IsActive,
                    IsVisibility = x.Product.IsVisibility,
                    DateCreated = x.Product.DateCreated,
                    DateUpdated = x.Product.DateUpdated,
                    OptionNames = attributeProducts.Where(a => a.ProductId == x.ProductId)?.Select(a => a.AttributeName).ToList(),
                    ProductAttributeDisplayDtos = x.pv.Select(pv => new ProductAttributeDisplayDto
                    {
                        AttributeNames = x.pav.Where(e => e.ProductId == x.ProductId)
                                                                         .Where(p => (x.pvv.GroupBy(pvv => pvv.ProductVariantId).Where(pc => pc.Key == pv.Id).FirstOrDefault().Select(pi => pi.ProductAttributeValueId)).Contains(p.Id))
                                                                         .Select(e => e.Value).ToList(),
                        SKU = pv.SKU,
                        Quantity = pv.Quantity,
                        Price = pv.Price,
                        Image = pv.ThumbnailPicture == null ? "" : pv.ThumbnailPicture
                    }).OrderByDescending(a => a.AttributeNames.FirstOrDefault()).ToList()
                }).OrderByDescending(p => p.DateCreated).ToList();

                _productCaches.SetCachedData<List<ProductDto>>(keyCache, productCaches, TimeSpan.FromMinutes(30));
            }

            var pageResult = new PageResult<ProductDto>()
            {
                Items = productCaches,
                Totals = totalCount
            };

            return new ApiSuccessResult<PageResult<ProductDto>> (pageResult);
        }
    }
}
