using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;
using System.Data;

namespace DreamyShop.Repository.Repositories.User
{
    public class UserRepository : GenericRepository<Domain.User>, IUserRepository
    {
        public UserRepository(IDbConnection _db) : base(_db)
        {
        }
    }
}