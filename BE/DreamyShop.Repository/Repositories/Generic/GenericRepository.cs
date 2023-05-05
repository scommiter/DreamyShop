using Dapper;
using DreamyShop.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DreamyShop.Repository.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IDbConnection _db;
        public GenericRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task AddAsync(T entity)
        {
            var sqlCmmd = SqlCommandExtension.InsertSqlCmd<T>(entity);
            await _db.ExecuteAsync(sqlCmmd);
        }

        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _db.QueryAsync<T>(SqlCommandExtension.GetAllSqlCmd<T>());
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _db.QuerySingleOrDefaultAsync<T>(SqlCommandExtension.GetByIdSqlCmd<T>(id));
        }

        public async Task Remove(int id)
        {
            await _db.ExecuteAsync(SqlCommandExtension.DeleteSqlCmd<T>(id));
        }

        public Task RemoveMultiple(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity, int id)
        {
            await _db.ExecuteAsync(SqlCommandExtension.UpdateSqlCmd<T>(entity, id));
        }
    }
}