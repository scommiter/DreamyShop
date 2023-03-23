using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Auth;

namespace DreamyShop.Repository.RepositoryWrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly DreamyShopDbContext _context;
        private IAuthRepository _auth;
        public RepositoryWrapper(DreamyShopDbContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }

        public IAuthRepository Auth
        {
            get
            {
                if (_auth == null)
                {
                    _auth = new AuthRepository(_context);
                }
                return _auth;
            }
        }
    }
}