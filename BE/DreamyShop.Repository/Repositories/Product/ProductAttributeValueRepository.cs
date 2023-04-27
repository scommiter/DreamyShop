using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;
using System.Data;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductAttributeValueRepository : GenericRepository<Domain.ProductAttributeValue>, IProductAttributeValueRepository
    {
        public ProductAttributeValueRepository(IDbConnection _db) : base(_db)
        {
        }
    }
}