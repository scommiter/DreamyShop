using DreamyShop.Domain;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Category
{
    public class CategoryRepository : GenericRepository<Domain.ProductCategory>, ICategoryRepository
    {
        private readonly DreamyShopDbContext _context;
        public CategoryRepository(DreamyShopDbContext context) : base(context)
        {
            _context = context;
        }

        public ProductCategory GetByName(string name)
        {
            if (name == null)
            {
                return null;
            }
            return _context.ProductCategories.Where(p => p.Name.Contains(name)).FirstOrDefault();
        }
    }
}