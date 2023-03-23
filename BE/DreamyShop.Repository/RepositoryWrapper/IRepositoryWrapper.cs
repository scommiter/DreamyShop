using DreamyShop.Repository.Repositories.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Repository.RepositoryWrapper
{
    public interface IRepositoryWrapper
    {
        IAuthRepository Auth { get; }
    }
}
