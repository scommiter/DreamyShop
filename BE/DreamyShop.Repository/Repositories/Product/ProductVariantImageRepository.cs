using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductVariantImageRepository : GenericRepository<Domain.ImageProductVariant>, IProductVariantImageRepository
    {
        public ProductVariantImageRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}