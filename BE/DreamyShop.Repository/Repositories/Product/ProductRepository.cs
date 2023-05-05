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
            sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectColumnSqlCmd<Domain.Manufacturer>()}");
            sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectColumnSqlCmd<Domain.ProductCategory>()}");
            sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectColumnSqlCmd<Domain.ProductVariant>()}");
            sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectColumnSqlCmd<Domain.ProductVariantValue>()}");
            sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectColumnSqlCmd<Domain.ProductAttributeValue>()}");
            sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectColumnSqlCmd<Domain.ImageProductVariant>()}");

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