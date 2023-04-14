using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductAttributeRepository : GenericRepository<Domain.Attribute>, IProductAttributeRepository
    {
        public ProductAttributeRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}