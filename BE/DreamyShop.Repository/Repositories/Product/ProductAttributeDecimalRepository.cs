using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductAttributeDecimalRepository : GenericRepository<Domain.ProductAttributeDecimal>, IProductAttributeDecimalRepository
    {
        public ProductAttributeDecimalRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}