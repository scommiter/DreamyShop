using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.User
{
    public class UserRepository : GenericRepository<Domain.User>, IUserRepository
    {
        public UserRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}