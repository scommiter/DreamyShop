using DreamyShop.Repository.Repositories.Generic;
using System.Data;

namespace DreamyShop.Repository.Repositories.Auth
{
    public class AuthRepository : GenericRepository<Domain.User>, IAuthRepository
    {
        public AuthRepository(IDbConnection _db) : base(_db)
        {
        }
    }
}