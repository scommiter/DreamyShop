using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace DreamyShop.Repository.Repositories.Manufacturer
{
    public class ManufacturerRepository : GenericRepository<Domain.Manufacturer>, IManufacturerRepository
    {
        private readonly DreamyShopDbContext _context;

        public ManufacturerRepository(DreamyShopDbContext context) : base(context)
        {
            _context = context;
        }

        public Domain.Manufacturer GetByName(string name)
        {
            if (name == null)
            {
                return null;
            }
            return _context.Manufacturers.Where(p => p.Name.Contains(name)).FirstOrDefault();
        }
    }
}