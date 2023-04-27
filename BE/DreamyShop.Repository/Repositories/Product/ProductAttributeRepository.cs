using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;
using System.Data;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductAttributeRepository : GenericRepository<Domain.ProductAttribute>, IProductAttributeRepository
    {
        public ProductAttributeRepository(IDbConnection _db) : base(_db)
        {
        }
    }
}