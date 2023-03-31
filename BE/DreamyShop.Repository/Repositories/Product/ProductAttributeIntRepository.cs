using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductAttributeIntRepository : GenericRepository<Domain.ProductAttributeInt>, IProductAttributeIntRepository
    {
        public ProductAttributeIntRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}