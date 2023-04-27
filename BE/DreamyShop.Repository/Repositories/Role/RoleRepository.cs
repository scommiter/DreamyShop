using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;
using System.Data;

namespace DreamyShop.Repository.Repositories.Role
{
    public class RoleRepository : GenericRepository<Domain.Role>, IRoleRepository
    {
        public RoleRepository(IDbConnection _db) : base(_db)
        {
        }
    }
}