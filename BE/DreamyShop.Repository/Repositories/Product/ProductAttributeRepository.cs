using DreamyShop.Domain;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public class ProductAttributeRepository : GenericRepository<Domain.ProductAttribute>, IProductAttributeRepository
    {
        private readonly DreamyShopDbContext _context;
        public ProductAttributeRepository(DreamyShopDbContext context) : base(context)
        {
            _context = context;
        }

        public List<ProductAttribute> GetProductAttributesByProductId(int productId)
        {
            return _context.ProductAttributes.Where(p => p.ProductId == productId).ToList();
        }

        public void RemoveProductAttribute(ProductAttribute productAttribute)
        {
            if (productAttribute != null)
            {
                _context.ProductAttributes.Remove(productAttribute);
            }
            _context.SaveChanges();
        }
    }
}