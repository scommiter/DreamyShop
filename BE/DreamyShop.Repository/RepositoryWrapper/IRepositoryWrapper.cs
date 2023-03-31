using DreamyShop.Repository.Repositories.Auth;
using DreamyShop.Repository.Repositories.Product;
using DreamyShop.Repository.Repositories.User;

namespace DreamyShop.Repository.RepositoryWrapper
{
    public interface IRepositoryWrapper : IDisposable
    {
        IAuthRepository Auth { get; }
        IUserRepository User { get; }
        IProductRepository Product { get; }
        void Save();
    }
}