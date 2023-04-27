using Dapper;
using DreamyShop.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
    }
}