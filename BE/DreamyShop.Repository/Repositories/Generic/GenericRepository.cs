using DreamyShop.EntityFrameworkCore;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq.Expressions;

namespace DreamyShop.Repository.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DreamyShopDbContext _context;

        private readonly DbSet<T> _dbSet;

        public GenericRepository(DreamyShopDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();

        public Task RollbackTransactionAsync() => _context.Database.RollbackTransactionAsync();

        public async Task EndTransactionAsync() => await _context.Database.CommitTransactionAsync();

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if (id == null)
            {
                return null;
            }
            return await _dbSet.FindAsync(id);
        }

        public void Remove(int id)
        {
            var result = _dbSet.Find(id);
            try
            {
                _dbSet.Remove(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RemoveMultiple(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                await _dbSet.AddRangeAsync(entities);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        //BULK CRUD
        public async Task BulkRangeInsertAsync(IList<T> entities)
        {
            await _context.BulkInsertAsync<T>(entities);
        }

        public void BulkRangeInsert(IList<T> entities)
        {
            _context.BulkInsert<T>(entities, new BulkConfig { PreserveInsertOrder = true, SetOutputIdentity = true });
        }

        public async Task BulkRangeUpdateAsync(IList<T> entities)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                await _context.BulkUpdateAsync<T>(entities);
                transaction.Commit();
            }
        }

        public async Task BulkRangeDeleteAsync(IList<T> entities)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                await _context.BulkDeleteAsync<T>(entities);
                transaction.Commit();
            }
        }

        public async Task BulkInsertDivideData(IList<T> entities, int batchSize)
        {
            int totalRecords = entities.Count;
            int batches = (int)Math.Ceiling((double)totalRecords / batchSize);
            for (int i = 0; i < batches; i++)
            {
                var batchEntities = entities.Skip(i * batchSize).Take(batchSize).ToList();
                _context.BulkInsert<T>(batchEntities, new BulkConfig { PreserveInsertOrder = true, SetOutputIdentity = true });
            }
        }
    }
}