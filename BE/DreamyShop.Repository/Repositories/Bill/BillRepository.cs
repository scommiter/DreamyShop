using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Bill
{
    public class BillRepository : GenericRepository<Domain.Bill>, IBillRepository
    {
        private readonly DreamyShopDbContext _context;
        public BillRepository(DreamyShopDbContext context) : base(context)
        {
            _context = context;
        }
    }
}