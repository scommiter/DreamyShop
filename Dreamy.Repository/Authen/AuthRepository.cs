using Dreamy.Domain;
using Dreamy.Repository.Generic;
using Dreamy.Repository.Utilities;
using System.Data;
using System.Reflection;
using System.Text;
using static Dapper.SqlMapper;

namespace Dreamy.Repository.Authen
{
    public class AuthRepository : GenericRepository<User>, IAuthRepository
    {
        private readonly IDbConnection db;
        public AuthRepository(IDbConnection _db) : base(_db)
        {
            db = _db;
        }

        public async Task Register(User userRegister)
        {
            var sqlCmmd = SqlCommandExtension.RegisterUser<User>(userRegister);
            await db.ExecuteAsync(sqlCmmd);
        }
    }
}
