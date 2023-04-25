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
using System.Net.Http.Headers;

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

        public async Task<ApiResult<PageResult<ProductDto>>> GetAllProduct(PagingRequest pagingRequest)
        {
            var productDtos = GetAllProductDto().Result;

            var pageResult = new PageResult<ProductDto>()
            {
                Items = productDtos.Skip((pagingRequest.Page - 1) * pagingRequest.Limit)
                                .Take(pagingRequest.Limit)
                                .ToList(),
                Totals = productDtos.Count()
            };
            return new ApiSuccessResult<PageResult<ProductDto>>(pageResult);
        }

        private async Task<List<ProductDto>> GetAllProductDto()
        {
            var query = await(from p in _context.Products
                              join m in _context.Manufacturers on p.ManufacturerId equals m.Id
                              join c in _context.ProductCategories on p.CategoryId equals c.Id
                              join pv in _context.ProductVariants on p.Id equals pv.ProductId into pvN
                              from pv in pvN.DefaultIfEmpty()
                              join ipv in _context.ImageProductVariants on pv.Id equals ipv.ProductVariantId into ipvN
                              from ipv in ipvN.DefaultIfEmpty()
                              join pvv in _context.ProductVariantValues on pv.Id equals pvv.ProductVariantId into pvvN
                              from pvv in pvvN.DefaultIfEmpty()
                              join pav in _context.ProductAttributeValues on pvv.ProductAttributeValueId equals pav.Id into pavN
                              from pav in pavN.DefaultIfEmpty()
                              select new
                              {
                                  Product = p,
                                  ProductVariantId = pvv.ProductVariantId == null ? Guid.Empty : pvv.ProductVariantId,
                                  ManufacturerName = m.Name,
                                  CategoryName = c.Name,
                                  pv,
                                  pav,
                                  ipv
                              }).ToListAsync();

            return query.GroupBy(r => new { r.Product })
                                .Select(x => new ProductDto
                                {
                                    Id = x.Key.Product.Id,
                                    Name = x.Key.Product.Name,
                                    Code = x.Key.Product.Code,
                                    ThumbnailPicture = x.Key.Product.ThumbnailPicture ?? "",
                                    ProductType = x.Key.Product?.ProductType ?? ProductType.Single,
                                    CategoryName = x.FirstOrDefault()?.CategoryName ?? "",
                                    ManufacturerName = x.FirstOrDefault()?.ManufacturerName ?? "",
                                    Description = x.Key.Product?.Description ?? "",
                                    IsActive = x.Key.Product?.IsActive ?? true,
                                    IsVisibility = x.Key.Product?.IsVisibility ?? true,
                                    DateCreated = x.Key.Product?.DateCreated ?? DateTime.Now,
                                    DateUpdated = x.Key.Product?.DateUpdated ?? DateTime.Now,
                                    ProductAttributeDisplayDtos = x.GroupBy(p => p.ProductVariantId)
                                                                .Select(pAttr => new ProductAttributeDisplayDto
                                                                {
                                                                    AttributeNames = pAttr.Select(x => x.pav?.Value ?? "").ToList(),
                                                                    SKU = pAttr.Select(x => x.pv?.SKU ?? "").FirstOrDefault(),
                                                                    Quantity = pAttr.Select(x => x.pv?.Quantity ?? 0).FirstOrDefault(),
                                                                    Price = pAttr.Select(x => x.pv?.Price ?? 0).FirstOrDefault(),
                                                                    Images = pAttr.Where(x => x.ipv?.Path != null).Select(x => x.ipv.Path).ToList()
                                                                }).ToList()
                                }).ToList();
        }

        public async Task<ApiResult<bool>> CreateProduct(ProductCreateDto productCreateDto)
        {
            var attributes =  _repository.Attribute.GetAll().ToList();
            AddAttribute(productCreateDto.VariantProducts, productCreateDto.ProductOptions, attributes);
            AddManufacturer(productCreateDto.ManufacturerName);
            AddCategory(productCreateDto.CategoryName);

            var newProduct = new Domain.Product
            {
                ManufacturerId = _repository.Manufacturer.GetByName(productCreateDto.ManufacturerName).Id,
                Name = productCreateDto.Name,
                Code = productCreateDto.Code,
                Slug = null,
                SortOrder = 1,
                ProductType = productCreateDto.ProductType,
                CategoryId = _repository.Category.GetByName(productCreateDto.CategoryName).Id,
                SeoMetaDescription = null,
                Description = productCreateDto.Description,
                ThumbnailPicture = productCreateDto.ThumbnailPicture,
                IsActive = productCreateDto.IsActive,
                IsVisibility = productCreateDto.IsVisibility,
                DateCreated = DateTime.Now
            };
            await _repository.Product.AddAsync(newProduct);
            _repository.Save();

            AddOrUpdateProductVariant(productCreateDto.VariantProducts, newProduct.Id, false);
            AddOrUpdateProductAttributeValue(productCreateDto.ProductOptions, attributes, newProduct.Id, false);
            AddOrUpdateProductVariantValue(newProduct.Id, productCreateDto.VariantProducts, productCreateDto.ProductOptions, attributes);
            return new ApiSuccessResult<bool>(true);
        }

        /// <summary>
        /// Add or Update product variant value relate to product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productVariantValues"></param>
        /// <param name="productOptions"></param>
        /// <param name="attributes"></param>
        /// <param name="isUpdate"></param>
        private async void AddOrUpdateProductVariantValue(
            Guid productId, 
            List<VariantProduct> productVariantValues, 
            Dictionary<string, List<string>> productOptions,
            List<Domain.Attribute> attributes)
        {
            var productVariants = _repository.ProductVariant.GetAll().ToList();
            var productAttributeValues = _repository.ProductAttributeValue.GetAll().ToList();
            foreach (var productVariantValue in productVariantValues)
            {
                var newProductVariantValues = productVariantValue.AttributeNames.Select(
                    p => new ProductVariantValue{
                        ProductVariantId = productVariants.Where(pV => pV.SKU == productVariantValue.SKU).FirstOrDefault().Id,
                        ProductId = productId,
                        AttributeId = attributes.Where(a => a.Name.Standard()
                                == productOptions.Where(po => po.Value.Select(poo => poo.Standard()).Contains(p.Standard())).Select(po => po.Key.Standard()).FirstOrDefault()).FirstOrDefault().Id,
                        ProductAttributeValueId = productAttributeValues.Where(a => a.Value.Standard() == p.Standard() && a.ProductId == productId).FirstOrDefault().Id
                    });
                await _repository.ProductVariantValue.AddRangeAsync(newProductVariantValues);
                _repository.Save();
            }
        }

        /// <summary>
        /// Add or Update product variant relate to product
        /// </summary>
        /// <param name="variantProducts"></param>
        /// <param name="newProductId"></param>
        private async void AddOrUpdateProductVariant(List<VariantProduct> variantProducts, Guid productId, bool isUpdate)
        {
            if(isUpdate == true)
            {
                var variantProductByProductIds = _context.ProductVariants.Where(pv => pv.ProductId == productId);
                variantProducts = variantProducts.Where(v => !variantProductByProductIds.Any(vp => vp.SKU == v.SKU && vp.ProductId == productId)).ToList();
            }
            if(variantProducts != null)
            {
                var newVariantProducts = variantProducts.Select(v => new ProductVariant
                {
                    ProductId = productId,
                    SKU = v.SKU,
                    IsVisibility = true,
                    IsActive = true,
                    Description = "",
                    Quantity = v.Quantity,
                    Price = v.Price,
                    DateCreated = DateTime.Now
                });
                await _repository.ProductVariant.AddRangeAsync(newVariantProducts);
                _repository.Save();
            }
        }

        /// <summary>
        /// Add or Update ProductAttributeValue relate to new product
        /// </summary>
        /// <param name="productCreateUpdateDto"></param>
        /// <param name="attributes"></param>
        /// <param name="newProduct"></param>
        private async void AddOrUpdateProductAttributeValue(
            Dictionary<string, List<string>> productOptions,
            List<Domain.Attribute> attributes, 
            Guid productId,
            bool isUpdate)
        {   
            if(isUpdate == true)
            {
                var productVariantValueByProductId = _context.ProductVariantValues.Where(p => p.ProductId == productId).ToList();
                _repository.ProductVariantValue.RemoveMultiple(productVariantValueByProductId);
                var productAttributeValues = _context.ProductAttributeValues.Where(pav => pav.ProductId == productId).ToList();
                _repository.ProductAttributeValue.RemoveMultiple(productAttributeValues);
                _repository.Save();
            }
            var attributeCreates = productOptions.ToList();
            foreach (var p in productOptions)
            {
                var newProductAttributes = p.Value.Select(an => new ProductAttributeValue
                {
                    AttributeId = attributes.Where(attr => attr.Name.Standard() ==
                                    attributeCreates.Where(a => a.Value.Select(p => p.Standard()).Contains(an.Standard())).FirstOrDefault().Key.Standard())
                                    .FirstOrDefault().Id,
                    ProductId = productId,
                    Value = an,
                    DateCreated = DateTime.Now
                });
                await _repository.ProductAttributeValue.AddRangeAsync(newProductAttributes);
                _repository.Save();
            }
        }

        /// <summary>
        /// Add if there are many variant products
        /// </summary>
        /// <param name="productCreateUpdateDto"></param>
        /// <param name="attributes"></param>
        private async void AddAttribute(List<VariantProduct> variantProducts, Dictionary<string, List<string>> productOptions, List<Domain.Attribute> attributes)
        {
            if (variantProducts.Any(pAttr => pAttr.AttributeNames != null) && variantProducts.Count > 1)
            {
                var attributeNames = productOptions.Select(pad => pad.Key.Standard()).Distinct().ToList();
                var newAttributeNames = attributeNames.Where(a => !attributes.Select(attr => attr.Name.Standard()).Contains(a.Standard())).ToList();
                var newAttributes = newAttributeNames.Select(an => new Domain.Attribute
                {
                    Name = an,
                    Code = an.ToUpper(),
                    IsActive = true,
                    IsVisibility = true,
                    IsUnique = true,
                    Note = "",
                    DateCreated = DateTime.Now
                }).ToList();
                await _repository.Attribute.AddRangeAsync(newAttributes);
                _repository.Save();
            }
        }

        /// <summary>
        /// Add Manufacturer relate to Product
        /// </summary>
        /// <param name="manufacturerName"></param>
        private async void AddManufacturer(string manufacturerName)
        {
            var manufacturers = _repository.Manufacturer.GetAll();
            if (!manufacturers.Select(m => m.Name.Standard()).ToList().Any(m => m == manufacturerName.Standard()))
            {
                await _repository.Manufacturer.AddAsync(new Domain.Manufacturer
                {
                    Name = manufacturerName,
                    Code = manufacturerName.ToUpper(),
                    Slug = manufacturerName.ToLower(),
                    CoverPicture = "",
                    IsVisibility = true,
                    IsActive = true,
                    Country = ""
                });
                _repository.Save();
            }
        }

        /// <summary>
        /// Add Category relate to Product
        /// </summary>
        /// <param name="categoryName"></param>
        private async void AddCategory(string categoryName)
        {
            var categories = _repository.Category.GetAll();
            if (!categories.Select(m => m.Name.Standard()).ToList().Any(m => m == categoryName.Standard()))
            {
                await _repository.Category.AddAsync(new Domain.ProductCategory
                {
                    Name = categoryName,
                    Code = categoryName.ToUpper(),
                    Slug = categoryName.ToLower(),
                    CoverPicture = "none",
                    IsVisibility = true,
                    IsActive = true,
                    SortOrder = 1,
                    ParentId = null,
                    SeoMetaDescription = "None",
                    DateCreated = DateTime.Now,
                });
                _repository.Save();
            }
        }



        public async Task<ApiResult<bool>> UpdateProduct(Guid id, ProductUpdateDto productUpdateDto)
        {
            var product = await _repository.Product.GetByIdAsync(id);

            if (product == null)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }

            if (productUpdateDto.Name != null)
            {
                product.Name = productUpdateDto.Name;
            }

            if (productUpdateDto.CategoryName != null)
            {
                var categoryUpdate = _repository.Category.GetByName(productUpdateDto.CategoryName);
                if (categoryUpdate == null)
                {
                    AddCategory(productUpdateDto.CategoryName);
                }
                product.CategoryId = _repository.Category.GetByName(productUpdateDto.CategoryName).Id;
            }

            if (productUpdateDto.ManufacturerName != null)
            {
                var manufacturerUpdate = _repository.Manufacturer.GetByName(productUpdateDto.ManufacturerName);
                if (manufacturerUpdate == null)
                {
                    AddManufacturer(productUpdateDto.ManufacturerName);
                }
                product.ManufacturerId = _repository.Manufacturer.GetByName(productUpdateDto.ManufacturerName).Id;
            }

            if (productUpdateDto.Code != null)
            {
                product.Code = productUpdateDto.Code;
            }

            if (productUpdateDto.ProductType != null)
            {
                product.ProductType = productUpdateDto.ProductType ?? ProductType.Single;
            }

            if (productUpdateDto.SeoMetaDescription != null)
            {
                product.SeoMetaDescription = productUpdateDto.SeoMetaDescription;
            }

            if (productUpdateDto.Slug != null)
            {
                product.Slug = productUpdateDto.Slug;
            }

            if (productUpdateDto.SortOrder != null)
            {
                product.SortOrder = productUpdateDto.SortOrder;
            }

            if (productUpdateDto.ThumbnailPicture != null)
            {
                product.ThumbnailPicture = productUpdateDto.ThumbnailPicture;
            }

            if (productUpdateDto.IsActive != null)
            {
                product.IsActive = productUpdateDto.IsActive ?? true;
            }

            if (productUpdateDto.IsVisibility != null)
            {
                product.IsVisibility = productUpdateDto.IsVisibility ?? true;
            }

            _repository.Product.Update(product);
            _repository.Save();

            var attributes = _repository.Attribute.GetAll().ToList();
            if(productUpdateDto.ProductOptions != null && productUpdateDto.VariantProducts != null)
            {
                AddAttribute(productUpdateDto.VariantProducts, productUpdateDto.ProductOptions, attributes);
                AddOrUpdateProductVariant(productUpdateDto.VariantProducts, id, true);
                AddOrUpdateProductAttributeValue(productUpdateDto.ProductOptions, attributes, id, true);
                AddOrUpdateProductVariantValue(id, productUpdateDto.VariantProducts, productUpdateDto.ProductOptions, attributes);
            }
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> RemoveProduct(Guid id)
        {
            var product = await _repository.Product.GetByIdAsync(id);
            if (product == null)
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            try
            {
                await _repository.Product.BeginTransactionAsync();
                var productVariantvalueDelete = _repository.ProductVariantValue.GetAll().Where(pv => pv.ProductVariant.ProductId == id).ToList();
                _repository.ProductVariantValue.RemoveMultiple(productVariantvalueDelete);
                _repository.ProductAttributeValue.RemoveMultiple(_context.ProductAttributeValues.Where(p => p.ProductId == id).ToList());
                _repository.ProductVariant.RemoveMultiple(_context.ProductVariants.Where(p => p.ProductId == id).ToList());
                _repository.ProductAttribute.RemoveMultiple(_context.ProductAttributes.Where(p => p.ProductId == id).ToList());
                _repository.Product.Remove(id);
                _repository.Save();
                await _repository.Product.EndTransactionAsync();
            }
            catch
            {
                await _repository.Product.RollbackTransactionAsync();
                return new ApiErrorResult<bool>((int)ErrorCodes.DeleteFailed);
            }
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<IList<ProductDto>>> SearchProduct(SearchProductCondition condition, PagingRequest pagingRequest)
        {
            var productDtos = GetAllProductDto().Result;
            if (condition == null)
            {
                return new ApiSuccessResult<IList<ProductDto>>(productDtos);
            }
            if (condition.ProductName != null)
            {
                productDtos = productDtos.Where(p => p.Name.ToLower() == condition.ProductName.ToLower()).ToList();
            }
            if (condition.Code != null)
            {
                productDtos = productDtos.Where(p => p.Code == condition.Code).ToList();
            }
            if (condition.ProductType != null)
            {
                productDtos = productDtos.Where(p => p.ProductType == condition.ProductType).ToList();
            }
            if (condition.CategoryName != null)
            {
                productDtos = productDtos.Where(p => p.CategoryName == condition.CategoryName).ToList();
            }
            if (condition.ManufacturerName != null)
            {
                productDtos = productDtos.Where(p => p.ManufacturerName == condition.ManufacturerName).ToList();
            }
            if (condition.IsActive != null)
            {
                productDtos = productDtos.Where(p => p.IsActive == condition.IsActive).ToList();
            }
            if (condition.IsVisibility != null)
            {
                productDtos = productDtos.Where(p => p.IsVisibility == condition.IsVisibility).ToList();
            }
            if (condition.DateCreated != null)
            {
                productDtos = productDtos.Where(p => p.DateCreated == condition.DateCreated).ToList();
            }
            if (condition.DateUpdated != null)
            {
                productDtos = productDtos.Where(p => p.DateUpdated == condition.DateUpdated).ToList();
            }
            //if(condition.PriceRange != null)
            //{
            //    var prices = productDtos.Select(p => p.ProductAttributeDisplayDtos?.Select(pad => pad.Price));
            //    foreach (var price in prices.Where(p => p != null))
            //    {
            //        var minPrice = price.Count() == 1 ? 0 : price.Min();
            //        var maxPrice = price.Max();
            //        if(condition.PriceRange.Min == 0)
            //        {

            //        }
            //    }
            //}
            var productDtoPagingsResult = productDtos.Skip((pagingRequest.Page - 1) * pagingRequest.Limit)
                                    .Take(pagingRequest.Limit)
                                    .ToList(); ;
            return new ApiSuccessResult<IList<ProductDto>>(productDtoPagingsResult);
        }

        public async Task<ApiResult<bool>> UploadImage(IFormFile file, Guid productId)
        {
            try
            {
                var product = await _repository.Product.GetByIdAsync(productId);
                if (product == null)
                {
                    return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
                }
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory().Replace("DreamyShop.Api", "DreamyShop.Infrastructure"), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine("DreamyShop.Infrastructure", folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    product.ThumbnailPicture = dbPath;
                    _repository.Product.Update(product);
                    _repository.Save();
                    return new ApiSuccessResult<bool>(true);
                }
                else
                {
                    return new ApiErrorResult<bool>((int)ErrorCodes.UploadFailed);
                }
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.UploadFailed);
            }
        }

        public async Task<ApiResult<bool>> UploadMultipleImage(List<IFormFile> files, Guid productVariantId)
        {
            try
            {
                var productVariant = await _repository.ProductVariant.GetByIdAsync(productVariantId);
                if (productVariant == null)
                {
                    return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
                }
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory().Replace("DreamyShop.Api", "DreamyShop.Infrastructure"), folderName);
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine("DreamyShop.Infrastructure", folderName, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        await _repository.ProductVariantImage.AddAsync(new ImageProductVariant
                        {
                            ProductVariantId = productVariantId,
                            Path = dbPath,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now
                        });
                        _repository.Save();
                    }
                    else
                    {
                        return new ApiErrorResult<bool>((int)ErrorCodes.UploadFailed);
                    }
                }
                return new ApiSuccessResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.UploadFailed);
            }
        }
    }
}   