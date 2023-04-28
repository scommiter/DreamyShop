using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace DreamyShop.Repository.Repositories.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        //Task EndTransactionAsync();
        //Task RollbackTransactionAsync();
        //Task<IDbContextTransaction> BeginTransactionAsync();

        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAll();

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity, int id);

        Task Remove(int id);

        Task RemoveMultiple(List<T> entities);
    }
}