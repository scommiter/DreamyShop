using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DreamyShop.Repository.Repositories.Manufacturer
{
    public class ManufacturerRepository : GenericRepository<Domain.Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(IDbConnection _db) : base(_db)
        {
        }

        //public Domain.Manufacturer GetByName(string name)
        //{
        //    if (name == null)
        //    {
        //        return null;
        //    }
        //    return _context.Manufacturers.Where(p => p.Name.Contains(name)).FirstOrDefault();
        //}
    }
}