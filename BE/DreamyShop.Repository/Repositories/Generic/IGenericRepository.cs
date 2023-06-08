using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace DreamyShop.Repository.Repositories.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task EndTransactionAsync();
        Task RollbackTransactionAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();

        Task<T> GetByIdAsync(int id);

        IQueryable<T> GetAll();

        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);

        void Remove(int id);

        void RemoveMultiple(List<T> entities);
    }
}