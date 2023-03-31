using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductAttributeRepository : GenericRepository<Domain.ProductAttribute>, IProductAttributeRepository
    {
        public ProductAttributeRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}