using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Bill
{
    public class BillDetailRepository : GenericRepository<Domain.BillDetail>, IBillDetailRepository
    {
        private readonly DreamyShopDbContext _context;
        public BillDetailRepository(DreamyShopDbContext context) : base(context)
        {
            _context = context;
        }
    }
}