using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;
using System.Data;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductVariantImageRepository : GenericRepository<Domain.ImageProductVariant>, IProductVariantImageRepository
    {
        public ProductVariantImageRepository(IDbConnection _db) : base(_db)
        {
        }
    }
}