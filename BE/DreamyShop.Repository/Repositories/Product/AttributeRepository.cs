using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public class AttributeRepository : GenericRepository<Domain.Attribute>, IAttributeRepository
    {
        public AttributeRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}