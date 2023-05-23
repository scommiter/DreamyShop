using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductImageRepository : GenericRepository<Domain.ImageProduct>, IProductImageRepository
    {
        public ProductImageRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}