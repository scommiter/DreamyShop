using Dapper;
using DreamyShop.Common.Extensions;
using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Repository.Repositories.Generic;
using System.Data;
using System.Text;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductRepository : GenericRepository<Domain.Product>, IProductRepository
    {
        private readonly IDbConnection _db;
        public ProductRepository(IDbConnection db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Domain.Product>> GetAllProduct()
        {
            var d = "select a.Id, a.Name, m.Name as 'ManufacturerName', pc.Name as 'CategoryName', a.Code, a.Slug, b.Id " +
                "as 'ProductVariantId', b.SKU, b.Quantity, b.Price, c.ProductAttributeValueId, d.Value" +
                "from dbo.Products a" +
                "join dbo.Manufacturers m on a.ManufacturerId = m.Id" +
                "join dbo.ProductCategories pc on a.CategoryId = pc.Id" +
                "left join dbo.ProductVariants b on a.Id = b.ProductId" +
                "left join dbo.ProductVariantValues c on b.Id = c.ProductVariantId" +
                "left join dbo.ProductAttributeValues d on c.ProductAttributeValueId = d.Id";
            var sqlSelectColumn = new StringBuilder();
            var sqlFrom = new StringBuilder();

            sqlSelectColumn.AppendLine($"{SqlCommandExtension.CreateSelectColumnSqlCmd<Domain.Product>()}");
            sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectColumnSqlCmd<Domain.Manufacturer>()}");
            sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectColumnSqlCmd<Domain.ProductCategory>()}");
            sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectColumnSqlCmd<Domain.ProductVariant>()}");
            sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectColumnSqlCmd<Domain.ProductVariantValue>()}");
            sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectColumnSqlCmd<Domain.ProductAttributeValue>()}");

            sqlFrom.AppendLine($"FROM {SqlCommandExtension.CreateFromTableSqlCmd<Domain.Product>()}");
            sqlFrom.AppendLine($"JOIN {SqlCommandExtension.CreateJoinTableSqlCmd<Domain.Product, Domain.Manufacturer>("ManufacturerId", "Id")}");
            sqlFrom.AppendLine($"JOIN {SqlCommandExtension.CreateJoinTableSqlCmd<Domain.Product, Domain.ProductCategory>("CategoryId", "Id")}");
            sqlFrom.AppendLine($"LEFT JOIN {SqlCommandExtension.CreateJoinTableSqlCmd<Domain.Product, Domain.ProductVariant>("Id", "ProductId")}");
            sqlFrom.AppendLine($"LEFT JOIN {SqlCommandExtension.CreateJoinTableSqlCmd<Domain.ProductVariant, Domain.ProductVariantValue>("Id", "ProductVariantId")}");
            sqlFrom.AppendLine($"LEFT JOIN {SqlCommandExtension.CreateJoinTableSqlCmd<Domain.ProductVariantValue, Domain.ProductAttributeValue>("ProductAttributeValueId", "Id")}");

            var sql = $@"SELECT {sqlSelectColumn} {sqlFrom}";
            return await _db.QueryAsync<Domain.Product>(sql);
        }
    }
}