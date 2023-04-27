using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;
using System.Data;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductVariantRepository : GenericRepository<Domain.ProductVariant>, IProductVariantRepository
    {
        public ProductVariantRepository(IDbConnection _db) : base(_db)
        {
        }
    }
}