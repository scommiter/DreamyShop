using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductVariantRepository : GenericRepository<Domain.ProductVariant>, IProductVariantRepository
    {
        public ProductVariantRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}