using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Role
{
    public class RoleRepository : GenericRepository<Domain.Role>, IRoleRepository
    {
        public RoleRepository(DreamyShopDbContext context) : base(context)
        {
        }
    }
}