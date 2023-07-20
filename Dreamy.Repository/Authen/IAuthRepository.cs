﻿using Dreamy.Domain;
using Dreamy.Repository.Generic;

namespace Dreamy.Repository.Authen
{
    public interface IAuthRepository : IGenericRepository<User>
    {
        Task Register(User userRegister);
    }
}