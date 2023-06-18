using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Category;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Cart
{
    public class CartRepository : GenericRepository<Domain.Cart>, ICartRepository
    {
        private readonly DreamyShopDbContext _context;
        public CartRepository(DreamyShopDbContext context) : base(context)
        {
            _context = context;
        }
    }
}