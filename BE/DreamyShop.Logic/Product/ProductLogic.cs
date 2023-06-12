using AutoMapper;
using System.Net.Http.Headers;
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

        public async Task<ApiResult<PageResult<ProductDto>>> GetAllProductPaging(PagingRequest pagingRequest)
        {
            var productDtos = GetAllProductDto().Result;

            var pageResult = new PageResult<ProductDto>()
            {
                Items = productDtos.OrderByDescending(p => p.DateCreated)
                    .Skip((pagingRequest.Page - 1) * pagingRequest.Limit)
                                .Take(pagingRequest.Limit)
                                .ToList(),
                Totals = productDtos.Count()
            };
            return new ApiSuccessResult<PageResult<ProductDto>>(pageResult);
        }

        public async Task<ApiResult<PageResult<ProductDto>>> GetAllProduct()
        {
            var productDtos = GetAllProductDto().Result;

            var pageResult = new PageResult<ProductDto>()
            {
                Items = productDtos,
                Totals = productDtos.Count()
            };
            return new ApiSuccessResult<PageResult<ProductDto>>(pageResult);
        }

        private async Task<List<ProductDto>> GetAllProductDto()
        {
            var attributes = _repository.Attribute.GetAll().ToList();
            var attributeValues = _repository.ProductAttribute.GetAll().ToList();
            var attributeProducts = _context.Attributes.Join(_context.ProductAttributes,
                                        a => a.Id,
                                        b => b.AttributeId,
                                        (a, b) => new
                                        {
                                            AttributeName = a.Name,
                                            ProductId = b.ProductId
                                        });
            var query = await(from p in _context.Products
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
                                  ProductVariantId = pvv.ProductVariantId == null ? new int() : pvv.ProductVariantId,
                                  ManufacturerName = m.Name,
                                  CategoryName = c.Name,
                                  pv,
                                  pav,
                                  ip
                              }).ToListAsync();

            var productDtos =  query.GroupBy(r => new { r.Product })
                                .Select(x => new ProductDto
                                {
                                    Id = x.Key.Product.Id,
                                    Name = x.Key.Product.Name,
                                    Code = x.Key.Product.Code,
                                    ThumbnailPictures = x.GroupBy(p => p.ip).Select(pt => pt.Select(ptt => ptt.ip?.Path ?? "").FirstOrDefault()).ToList(),
                                    ProductType = x.Key.Product?.ProductType ?? ProductType.Single,
                                    CategoryName = x.FirstOrDefault()?.CategoryName ?? "",
                                    ManufacturerName = x.FirstOrDefault()?.ManufacturerName ?? "",
                                    Description = x.Key.Product?.Description ?? "",
                                    IsActive = x.Key.Product?.IsActive ?? true,
                                    IsVisibility = x.Key.Product?.IsVisibility ?? true,
                                    DateCreated = x.Key.Product?.DateCreated ?? DateTime.Now,
                                    DateUpdated = x.Key.Product?.DateUpdated ?? DateTime.Now,
                                    OptionNames = attributeProducts.Where(a => a.ProductId == x.Key.Product.Id)?.Select(a => a.AttributeName).ToList(),
                                    ProductAttributeDisplayDtos = x.GroupBy(p => p.ProductVariantId)
                                                                .Select(pAttr => new ProductAttributeDisplayDto
                                                                {
                                                                    AttributeNames = pAttr.GroupBy(p => p.pav).Select(x => x.Select(px => px.pav?.Value ?? "").FirstOrDefault()).ToList(),
                                                                    SKU = pAttr.Select(x => x.pv?.SKU ?? "").FirstOrDefault(),
                                                                    Quantity = pAttr.Select(x => x.pv?.Quantity ?? 0).FirstOrDefault(),
                                                                    Price = pAttr.Select(x => x.pv?.Price ?? 0).FirstOrDefault(),
                                                                    Image = pAttr.Select(p => p.pv?.ThumbnailPicture ?? "").FirstOrDefault()
                                                                }).ToList()
                                }).ToList();
            foreach (var product in productDtos)
            {
                product.ProductAttributeDisplayDtos = product.ProductAttributeDisplayDtos.OrderByDescending(a => string.Join(", ", a.AttributeNames)).ToList();
            }
            return productDtos;
        }

        public async Task<ApiResult<bool>> CreateProduct(ProductCreateDto productCreateDto)
        {
            AddManufacturer(productCreateDto.ManufacturerName);
            AddCategory(productCreateDto.CategoryName);

            var newProduct = new Domain.Product
            {
                ManufacturerId = _repository.Manufacturer.GetByName(productCreateDto.ManufacturerName).Id,
                Name = productCreateDto.Name,
                Code = productCreateDto.Code,
                Slug = productCreateDto.Name.ToLower(),
                SortOrder = 1,
                ProductType = (ProductType)Enum.Parse(typeof(ProductType), productCreateDto.ProductType),
                CategoryId = _repository.Category.GetByName(productCreateDto.CategoryName).Id,
                SeoMetaDescription = null,
                Description = productCreateDto.Description,
                IsActive = productCreateDto.IsActive,
                IsVisibility = productCreateDto.IsVisibility,
                DateCreated = DateTime.Now
            };
            await _repository.Product.AddAsync(newProduct);
            _repository.Save();

            if (productCreateDto.ProductOptions.Count > 0 && productCreateDto.VariantProducts.Count > 1)
            {
                AddAttribute(productCreateDto.VariantProducts, productCreateDto.ProductOptions, newProduct.Id);
            }

            AddOrUpdateProductVariant(productCreateDto.VariantProducts, newProduct.Id, false);
            if (productCreateDto.ProductOptions.Count > 0 && productCreateDto.VariantProducts.Count > 1)
            {
                AddOrUpdateProductAttributeValue(productCreateDto.ProductOptions, newProduct.Id, false);
                AddOrUpdateProductVariantValue(newProduct.Id, productCreateDto.VariantProducts, productCreateDto.ProductOptions);
            }
            if(productCreateDto.Images != null && productCreateDto.Images.Count > 1)
            {
                UploadProductImages(productCreateDto.Images, newProduct.Id);
            }
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
            int productId, 
            List<VariantProduct> productVariantValues, 
            Dictionary<string, List<string>> productOptions)
        {
            var attributes = _repository.Attribute.GetAll().ToList();
            productVariantValues.RemoveAll(product => product.SKU == "");

            var productVariants = _repository.ProductVariant.GetAll().Where(p => p.ProductId == productId).ToList();
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
        private async void AddOrUpdateProductVariant(List<VariantProduct> variantProducts, int productId, bool isUpdate)
        {
            variantProducts.RemoveAll(product => product.SKU == "");
            if (isUpdate == true)
            {
                var variantProductByProductIds = _context.ProductVariants.Where(pv => pv.ProductId == productId);
                variantProducts = variantProducts.Where(v => !variantProductByProductIds.Any(vp => vp.SKU == v.SKU && vp.ProductId == productId)).ToList();
            }
            if(variantProducts != null)
            {
                foreach (var variant in variantProducts)
                {
                    var newVariantProduct = new ProductVariant
                    {
                        ProductId = productId,
                        SKU = variant.SKU,
                        IsVisibility = true,
                        IsActive = true,
                        Description = "",
                        Quantity = variant.Quantity,
                        Price = variant.Price,
                        ThumbnailPicture = "",
                        DateCreated = DateTime.Now
                    };
                    await _repository.ProductVariant.AddAsync(newVariantProduct);
                    _repository.Save();

                    if(variant.ThumbnailPicture != null && variant.ThumbnailPicture != "")
                    {
                        AddImageVariantProduct(variant.ThumbnailPicture, newVariantProduct.Id, $"{variant.SKU}Product.png");
                    }
                }
            }
        }

        private async void UploadProductImages(List<string> fileContexts, int productId, bool isProductUpdate = false)
        {
            if(isProductUpdate == true)
            {
                var imgExistingProduct = _repository.ProductImage.GetAll().Where(p => p.ProductId == productId).ToList();
                _repository.ProductImage.RemoveMultiple(imgExistingProduct);
                _repository.Save();
            }
            var imageProducts = new List<ImageProduct>();
            var product = await _repository.Product.GetByIdAsync(productId);
            var count = 0;
            if(product != null)
            {
                foreach (var fileContext in fileContexts)
                {
                    var fileName = product.Name.RemoveAllWhiteSpace();
                    fileName = fileName + count++ + ".png";
                    string base64Data = fileContext.Split(',')[1];
                    byte[] imageBytes = Convert.FromBase64String(base64Data);
                    //Tạo MemoryStream từ mảng byte
                    using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                    {
                        IFormFile file = new FormFile(memoryStream, 0, memoryStream.Length, Path.GetFileNameWithoutExtension(fileName), fileName);

                        var currentProduct = await _repository.Product.GetByIdAsync(productId);
                        var pathToSave = Directory.GetCurrentDirectory().Replace("BE\\DreamyShop.Api", "FE\\src\\assets\\ImageProducts");
                        if (!Directory.Exists(pathToSave))
                        {
                            Directory.CreateDirectory(pathToSave);
                        }
                        var fullPath = Path.Combine(pathToSave, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        imageProducts.Add(new ImageProduct
                        {
                            ProductId = productId,
                            Path = fileName,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now
                        });
                    }
                }
                await _repository.ProductImage.AddRangeAsync(imageProducts);
                _repository.Save();
            }         
        }
        private async void AddImageVariantProduct(string fileContext, int variantId, string fileName)
        {
            string base64Data = fileContext.Split(',')[1];
            byte[] imageBytes = Convert.FromBase64String(base64Data);
            //Tạo MemoryStream từ mảng byte
            using (MemoryStream memoryStream = new MemoryStream(imageBytes))
            {
                IFormFile file = new FormFile(memoryStream, 0, memoryStream.Length, Path.GetFileNameWithoutExtension(fileName), $"{fileName}.png");

                var productVariant = await _repository.ProductVariant.GetByIdAsync(variantId);
                var pathToSave = Directory.GetCurrentDirectory().Replace("BE\\DreamyShop.Api", "FE\\src\\assets\\ImageProducts");
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                if (file.Length > 0)
                {
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = fileName;
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    productVariant.ThumbnailPicture = dbPath;
                    _repository.Save();
                }
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
            int productId,
            bool isUpdate)
        {
            var attributes = _repository.Attribute.GetAll().ToList();
            if (isUpdate == true)
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
        private async void AddAttribute(
            List<VariantProduct> variantProducts, 
            Dictionary<string, 
            List<string>> productOptions, 
            int productId,
            bool isUpdate = false)
        {
            var attributes = _repository.Attribute.GetAll().ToList();
            if (variantProducts.Any(pAttr => pAttr.AttributeNames != null) && variantProducts.Count > 1)
            {
                productOptions.Values.ToList().ForEach(list => list.RemoveAll(item => item == ""));
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

                attributes = _repository.Attribute.GetAll().ToList();
                var attributeCurrentProduct = attributes.Where(a => attributeNames.Contains(a.Name)).ToList();
                if (!isUpdate)
                {
                    var newProductAttributes = attributeCurrentProduct.Select(na => new Domain.ProductAttribute
                    {
                        ProductId = productId,
                        AttributeId = na.Id,
                        StatusID = 1,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    }).ToList();
                    await _repository.ProductAttribute.AddRangeAsync(newProductAttributes);
                    _repository.Save();
                }
                else
                {
                    var existingProductAttributes = _repository.ProductAttribute.GetProductAttributesByProductId(productId);
                    var paUpdates = new List<ProductAttribute>();
                    foreach (var attribute in attributeCurrentProduct)
                    {
                        var productAttributeUpdate = existingProductAttributes.Where(e => e.AttributeId == attribute.Id).FirstOrDefault();
                        if(productAttributeUpdate == null)
                        {
                            var paUpdate = new ProductAttribute();
                            paUpdate.ProductId = productId;
                            paUpdate.AttributeId = attribute.Id;
                            paUpdate.DateUpdated = DateTime.Now;
                            paUpdates.Add(paUpdate);
                        }
                    }
                    var deleteProductAttributes = existingProductAttributes.Where(e => !attributeCurrentProduct.Any(a => a.Id == e.AttributeId)).ToList();
                    _repository.ProductAttribute.RemoveMultiple(deleteProductAttributes);
                    await _repository.ProductAttribute.UpdateRangeAsync(paUpdates);
                    _repository.Save();
                }
            }
        }

        /// <summary>
        /// Add product images
        /// </summary>
        /// <param name="imageProducts"></param>
        //private async void AddProductImage(List<string> imageProducts, int newProductId)
        //{
        //    var existingImageProducts = _repository.ProductImage.GetAll()
        //        .Where(p => p.ProductId == newProductId).Select(pi => pi.Path).ToList();
        //    imageProducts = imageProducts.Where(i => !existingImageProducts.Contains(i)).ToList();
        //    var newImageProducts = imageProducts.Select(i => new ImageProduct
        //    {
        //        ProductId = newProductId,
        //        Path = i
        //    }).ToList();
        //    await _repository.ProductImage.AddRangeAsync(newImageProducts);
        //    _repository.Save();
        //}

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

        public async Task<ApiResult<bool>> UpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            var product = await _repository.Product.GetByIdAsync(id);
            if (product == null)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }

            if(!_repository.ProductAttributeValue.GetAll().Any(p => p.ProductId == id))
            {
                var productVariantvalueDelete = _repository.ProductVariantValue.GetAll().Where(pv => pv.ProductVariant.ProductId == id).ToList();
                _repository.ProductVariantValue.RemoveMultiple(productVariantvalueDelete);
                _repository.ProductVariant.RemoveMultiple(_context.ProductVariants.Where(p => p.ProductId == id).ToList());
                _repository.Save();
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
                product.ProductType = (ProductType)Enum.Parse(typeof(ProductType), productUpdateDto.ProductType);
            }

            if (productUpdateDto.IsActive != null)
            {
                product.IsActive = productUpdateDto.IsActive ?? true;
            }

            if (productUpdateDto.IsVisibility != null)
            {
                product.IsVisibility = productUpdateDto.IsVisibility ?? true;
            }

            if (productUpdateDto.Images != null)
            {
                UploadProductImages(productUpdateDto.Images, id, true);
            }

            _repository.Product.Update(product);
            _repository.Save();

            var attributes = _repository.Attribute.GetAll().ToList();
            if(productUpdateDto.ProductOptions.Count != 0 && productUpdateDto.VariantProducts.Count != 0)
            {
                AddAttribute(productUpdateDto.VariantProducts, productUpdateDto.ProductOptions, id, true);
                AddOrUpdateProductVariant(productUpdateDto.VariantProducts, id, true);
                AddOrUpdateProductAttributeValue(productUpdateDto.ProductOptions, id, true);
                AddOrUpdateProductVariantValue(id, productUpdateDto.VariantProducts, productUpdateDto.ProductOptions);
            }
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> RemoveProduct(int id)
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
                _repository.ProductImage.RemoveMultiple(_context.ImageProducts.Where(p => p.ProductId == id).ToList());
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

        public async Task<ApiResult<bool>> UploadImage(IFormFile file, int productId)
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
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine("DreamyShop.Infrastructure", folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    await _repository.ProductImage.AddAsync(new ImageProduct
                    {
                        ProductId = productId,
                        Path = dbPath,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    });
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

        public async Task<ApiResult<bool>> UploadMultipleImage(List<IFormFile> files, int productId)
        {
            try
            {
                var product = await _repository.Product.GetByIdAsync(productId);
                if (product == null)
                {
                    return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
                }
                var pathToSave = Directory.GetCurrentDirectory().Replace("BE\\DreamyShop.Api", "FE\\src\\assets\\ImageProducts");
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        await _repository.ProductImage.AddAsync(new ImageProduct
                        {
                            ProductId = productId,
                            Path = fileName,
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