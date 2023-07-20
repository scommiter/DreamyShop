using Dreamy.Domain;
using Dreamy.Repository.Generic;
using System.Data;

namespace Dreamy.Repository.Authen
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(IDbConnection _db) : base(_db)
        {
        }
    }
}
