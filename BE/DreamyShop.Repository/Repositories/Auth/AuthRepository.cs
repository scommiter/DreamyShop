using DreamyShop.Domain;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Auth
{
    public class AuthRepository : GenericRepository<User>, IAuthRepository
    {
        public AuthRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}