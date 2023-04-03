using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Manufacturer
{
    public class ManufacturerRepository : GenericRepository<Domain.Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}