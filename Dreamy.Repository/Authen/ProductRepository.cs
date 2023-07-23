using Dapper;
using Dreamy.Domain;
using Dreamy.Domain.Shared.Dtos;
using Dreamy.Domain.Shared.Dtos.Product;
using Dreamy.Repository.Generic;
using Dreamy.Repository.Utilities;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Dreamy.Repository.Authen
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IDbConnection _db;
        private IConfiguration configuration;
        public ProductRepository(IDbConnection db) : base(db)
        {
            _db = db;
        }

        public async Task<List<ProductExecuteDto>> GetAllProduct(PagingRequest pagingRequest)
        {
            //var sqlParentSelectColumn = new StringBuilder();
            //var sqlSelectColumn = new StringBuilder();
            //var sqlFrom = new StringBuilder();

            //sqlParentSelectColumn.AppendLine($"{SqlCommandExtension.CreateSelectColumnSqlCmd<Product>()}");
            //sqlParentSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectSpecificColumnSqlCmd<Manufacturer>(new List<string>() { "Name" })}");
            //sqlParentSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectSpecificColumnSqlCmd<ProductCategory>(new List<string>() { "Name" })}");
            //sqlParentSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectSpecificColumnSqlCmd<ProductVariant>(new List<string>() { "SKU", "Quantity", "Price" })}");
            //sqlParentSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectSpecificColumnSqlCmd<ProductVariantValue>(new List<string>() { "ProductVariantId" })}");
            //sqlParentSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectSpecificColumnSqlCmd<ProductAttributeValue>(new List<string>() { "Value" })}");
            //sqlParentSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectSpecificColumnSqlCmd<ImageProduct>(new List<string>() { "Path" })}");

            //sqlSelectColumn.AppendLine($", {SqlCommandExtension.CreateSelectSpecificColumnSqlCmd<Product>(new List<string>() { "Id", "Name", "DateCreated", "Code", "Slug", "ManufactureId", "CategoryId" })}");
            //sqlSelectColumn.AppendLine($"{SqlCommandExtension.CreateSelectColumnSqlCmd<Product>()}");

            //sqlFrom.AppendLine($"FROM {SqlCommandExtension.CreateFromTableSqlCmd<Product>()}");
            //sqlFrom.AppendLine($"JOIN {SqlCommandExtension.CreateJoinTableSqlCmd<Product, Manufacturer>("ManufacturerId", "Id")}");
            //sqlFrom.AppendLine($"JOIN {SqlCommandExtension.CreateJoinTableSqlCmd<Product, ProductCategory>("CategoryId", "Id")}");
            //sqlFrom.AppendLine($"LEFT JOIN {SqlCommandExtension.CreateJoinTableSqlCmd<Product, ProductVariant>("Id", "ProductId")}");
            //sqlFrom.AppendLine($"LEFT JOIN {SqlCommandExtension.CreateJoinTableSqlCmd<Product, ImageProduct>("Id", "ProductId")}");
            //sqlFrom.AppendLine($"LEFT JOIN {SqlCommandExtension.CreateJoinTableSqlCmd<ProductVariant, ProductVariantValue>("Id", "ProductVariantId")}");
            //sqlFrom.AppendLine($"LEFT JOIN {SqlCommandExtension.CreateJoinTableSqlCmd<ProductVariantValue, ProductAttributeValue>("ProductAttributeValueId", "Id")}");

            var sql = "SELECT a.Id, a.Name, m.Name as 'ManufacturerName', pc.Name as 'CategoryName', a.Code, a.Slug, b.SKU, b.Quantity, b.Price, c.ProductAttributeValueId, d.Value, ip.Path FROM " +
                "(SELECT Id, Name, DateCreated, Code, Slug, ManufacturerId, CategoryId " +
                $"FROM dbo.Products ORDER BY DateCreated DESC OFFSET {(pagingRequest.Page-1) * pagingRequest.Limit} ROWS FETCH NEXT {pagingRequest.Limit} ROWS ONLY ) a " +
                "JOIN dbo.Manufacturers m ON a.ManufacturerId = m.Id " +
                "JOIN dbo.ProductCategories pc ON a.CategoryId = pc.Id " +
                "LEFT JOIN dbo.ProductVariants b ON a.Id = b.ProductId " +
                "LEFT JOIN dbo.ImageProducts ip ON a.Id = ip.ProductId " +
                "LEFT JOIN dbo.ProductVariantValues c ON b.Id = c.ProductVariantId " +
                "LEFT JOIN dbo.ProductAttributeValues d ON c.ProductAttributeValueId = d.Id";

            SqlConnection connection = new SqlConnection("Server=.;Database=DREMY;Trusted_Connection=True");
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            var products = new List<ProductExecuteDto>();
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                // Loop over the results
                while (dataReader.Read())
                {
                    var product = new ProductExecuteDto();
                    product.Id = Convert.ToInt32(dataReader["Id"]);
                    product.Name = Convert.ToString(dataReader["Name"]);
                    product.Code = Convert.ToString(dataReader["Code"]);
                    product.ThumbnailPicture = Convert.ToString(dataReader["Path"]);
                    product.CategoryName = Convert.ToString(dataReader["CategoryName"]);
                    product.ManufacturerName = Convert.ToString(dataReader["ManufacturerName"]);
                    product.Description = Convert.ToString(dataReader["ManufacturerName"]);
                    product.SKU = Convert.ToString(dataReader["SKU"]);
                    product.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                    product.Price = Convert.ToInt32(dataReader["Price"]);
                    product.AttributeValue = Convert.ToString(dataReader["Value"]);
                    products.Add(product);
                }
            }
            connection.Close();
            return products;
        }

        public dynamic GetTotalCountProduct()
        {
            return _db.Query("SELECT COUNT(*) FROM Products");
        }
    }
}
