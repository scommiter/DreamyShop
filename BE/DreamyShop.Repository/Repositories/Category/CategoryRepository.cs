using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Category
{
    public class CategoryRepository : GenericRepository<Domain.ProductCategory>, ICategoryRepository
    {
        public CategoryRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}