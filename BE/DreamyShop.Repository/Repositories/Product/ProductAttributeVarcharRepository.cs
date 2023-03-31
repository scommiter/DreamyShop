using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductAttributeVarcharRepository : GenericRepository<Domain.ProductAttributeVarchar>, IProductAttributeVarcharRepository
    {
        public ProductAttributeVarcharRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}