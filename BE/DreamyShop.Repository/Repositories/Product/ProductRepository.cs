using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductRepository : GenericRepository<Domain.Product>, IProductRepository
    {
        public ProductRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}