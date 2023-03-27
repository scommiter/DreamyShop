using DreamyShop.Repository.Repositories.Auth;
using DreamyShop.Repository.Repositories.User;

namespace DreamyShop.Repository.RepositoryWrapper
{
    public interface IRepositoryWrapper
    {
        IAuthRepository Auth { get; }
        IUserRepository User { get; }
    }
}