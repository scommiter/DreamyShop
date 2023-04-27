using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;
using System.Data;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductRepository : GenericRepository<Domain.Product>, IProductRepository
    {
        public ProductRepository(IDbConnection _db) : base(_db)
        {
        }
    }
}