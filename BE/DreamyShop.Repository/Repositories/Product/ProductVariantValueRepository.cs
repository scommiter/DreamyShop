using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductVariantValueRepository : GenericRepository<Domain.ProductVariantValue>, IProductVariantValueRepository
    {
        public ProductVariantValueRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}