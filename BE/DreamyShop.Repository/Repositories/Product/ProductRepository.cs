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

        public async Task CreateProduct(ProductCreateDto productCreateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<dynamic>> GetAllProduct()
        {
            var sqlSelectColumn = new StringBuilder();
            var sqlFrom = new StringBuilder();

            sqlSelectColumn.AppendLine($"{SqlCommandExtension.CreateSelectColumnSqlCmd<Domain.Product>()}");
            sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectSpecificColumnSqlCmd<Domain.Manufacturer>(new List<string>() { "Name"}) }");
            sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectSpecificColumnSqlCmd<Domain.ProductCategory>(new List<string>() { "Name" }) }");
            sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectSpecificColumnSqlCmd<Domain.ProductVariant>(new List<string>() { "SKU", "Quantity", "Price" })}");
            sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectSpecificColumnSqlCmd<Domain.ProductVariantValue>(new List<string>() { "ProductVariantId" })}");
            sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectSpecificColumnSqlCmd<Domain.ProductAttributeValue>(new List<string>() { "Value" })}");
            sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectSpecificColumnSqlCmd<Domain.ImageProductVariant>(new List<string>() { "Path" })}");

            sqlFrom.AppendLine($"FROM {SqlCommandExtension.CreateFromTableSqlCmd<Domain.Product>()}");
            sqlFrom.AppendLine($"JOIN {SqlCommandExtension.CreateJoinTableSqlCmd<Domain.Product, Domain.Manufacturer>("ManufacturerId", "Id")}");
            sqlFrom.AppendLine($"JOIN {SqlCommandExtension.CreateJoinTableSqlCmd<Domain.Product, Domain.ProductCategory>("CategoryId", "Id")}");
            sqlFrom.AppendLine($"LEFT JOIN {SqlCommandExtension.CreateJoinTableSqlCmd<Domain.Product, Domain.ProductVariant>("Id", "ProductId")}");
            sqlFrom.AppendLine($"LEFT JOIN {SqlCommandExtension.CreateJoinTableSqlCmd<Domain.ProductVariant, Domain.ImageProductVariant>("Id", "ProductVariantId")}");
            sqlFrom.AppendLine($"LEFT JOIN {SqlCommandExtension.CreateJoinTableSqlCmd<Domain.ProductVariant, Domain.ProductVariantValue>("Id", "ProductVariantId")}");
            sqlFrom.AppendLine($"LEFT JOIN {SqlCommandExtension.CreateJoinTableSqlCmd<Domain.ProductVariantValue, Domain.ProductAttributeValue>("ProductAttributeValueId", "Id")}");

            var sql = $@"SELECT {sqlSelectColumn} {sqlFrom}";
            return await _db.QueryAsync(sql);
        }
    }
}