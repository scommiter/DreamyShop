using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductAttributeDateTimeRepository : GenericRepository<Domain.ProductAttributeDateTime>, IProductAttributeDateTimeRepository
    {
        public ProductAttributeDateTimeRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}