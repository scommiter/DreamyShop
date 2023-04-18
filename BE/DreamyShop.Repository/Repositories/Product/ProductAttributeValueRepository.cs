using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductAttributeValueRepository : GenericRepository<Domain.ProductAttributeValue>, IProductAttributeValueRepository
    {
        public ProductAttributeValueRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}