using DreamyShop.Domain;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Auth
{
    public interface IAuthRepository : IGenericRepository<User>
    {
    }
}