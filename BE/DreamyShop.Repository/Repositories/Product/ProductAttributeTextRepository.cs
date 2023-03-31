using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductAttributeTextRepository : GenericRepository<Domain.ProductAttributeText>, IProductAttributeTextRepository
    {
        public ProductAttributeTextRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}