using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Repository.Repositories.Generic;
using System.Data;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductRepository : GenericRepository<Domain.Product>, IProductRepository
    {
        public ProductRepository(IDbConnection _db) : base(_db)
        {
        }

        public Task<ApiResult<PageResult<ProductDto>>> GetAllProduct()
        {
            var sqlCmmd = "select a.Id, a.Name, m.Name as 'ManufacturerName', pc.Name as 'CategoryName', a.Code, a.Slug, b.Id " +
                "as 'ProductVariantId', b.SKU, b.Quantity, b.Price, c.ProductAttributeValueId, d.Value" +
                "from dbo.Products a" +
                "join dbo.Manufacturers m on a.ManufacturerId = m.Id" +
                "join dbo.ProductCategories pc on a.CategoryId = pc.Id" +
                "left join dbo.ProductVariants b on a.Id = b.ProductId" +
                "left join dbo.ProductVariantValues c on b.Id = c.ProductVariantId" +
                "left join dbo.ProductAttributeValues d on c.ProductAttributeValueId = d.Id";
            throw new NotImplementedException();
        }
    }
}