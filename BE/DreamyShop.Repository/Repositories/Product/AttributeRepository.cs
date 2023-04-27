using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;
using System.Data;

namespace DreamyShop.Repository.Repositories.Product
{
    public class AttributeRepository : GenericRepository<Domain.Attribute>, IAttributeRepository
    {
        public AttributeRepository(IDbConnection _db) : base(_db)
        {
        }
    }
}