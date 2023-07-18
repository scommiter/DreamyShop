using Dapper;
using Dreamy.Repository.Utilities;
using System.Data;

namespace Dreamy.Repository.Generic
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
