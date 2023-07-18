using Dreamy.Domain;
using Dreamy.Repository.Generic;
using System.Data;

namespace Dreamy.Repository.Authen
{
    public class AuthRepository : GenericRepository<User>, IAuthRepository
    {
        public AuthRepository(IDbConnection _db) : base(_db)
        {
        }
    }
}
