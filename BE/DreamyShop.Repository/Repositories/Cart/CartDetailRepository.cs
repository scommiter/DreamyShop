using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Cart
{
    public class CartDetailRepository : GenericRepository<Domain.CartDetail>, ICartDetailRepository
    {
        private readonly DreamyShopDbContext _context;
        public CartDetailRepository(DreamyShopDbContext context) : base(context)
        {
            _context = context;
        }
    }
}