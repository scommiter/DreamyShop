using DreamyShop.Common.Extensions;
using DreamyShop.Domain;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;
using System.Data;

namespace DreamyShop.Repository.Repositories.Category
{
    public class CategoryRepository : GenericRepository<Domain.ProductCategory>, ICategoryRepository
    {
        private readonly IDbConnection _db;
        public CategoryRepository(IDbConnection db) : base(db)
        {
            _db = db;
        }

        //public ProductCategory GetByName(string name)
        //{
        //    if (name == null)
        //    {
        //        return null;
        //    }
        //    return _context.ProductCategories.Where(p => p.Name == name).FirstOrDefault();
        //}
    }
}