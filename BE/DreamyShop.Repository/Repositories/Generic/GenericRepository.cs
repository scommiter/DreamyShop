using Dapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Xml.Linq;

namespace DreamyShop.Repository.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IDbConnection _db;
        private string tableName;
        public GenericRepository(IDbConnection db)
        {
            _db = db;
            var tableAttribute = typeof(T).GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() as TableAttribute;
            if (tableAttribute != null)
            {
                tableName = tableAttribute.Name;
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Query<T>($"SELECT * FROM {tableName}");
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _db.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {tableName} WHERE Id = {id}");
        }

        public async Task UpdateAsync(T entity, int id)
        {
            var props = entity.GetType().GetProperties();
            string phay;
            string query = $"UPDATE {tableName} SET ";
            foreach (var prop in props)
            {
                if (prop.Name == "Id") continue;
                if (prop.PropertyType.IsGenericType)
                {
                    query = query.Substring(0, query.Length - 1);
                    continue;
                }
                phay = "";
                query += $"{phay}{prop.Name}={phay}'{prop.GetValue(entity)}'";
                if (prop != props.Last())
                {
                    query += ",";
                }
            }
            await _db.ExecuteAsync(query + $" WHERE Id = {id}");
        }
    }
}