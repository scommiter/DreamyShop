using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;
using System.Data;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductVariantValueRepository : GenericRepository<Domain.ProductVariantValue>, IProductVariantValueRepository
    {
        public ProductVariantValueRepository(IDbConnection _db) : base(_db)
        {
        }
    }
}