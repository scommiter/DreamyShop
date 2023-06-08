using DreamyShop.Domain;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public interface IProductAttributeRepository : IGenericRepository<ProductAttribute>
    {
        public List<ProductAttribute> GetProductAttributesByProductId(int productId);
        public void RemoveProductAttribute(ProductAttribute productAttribute);
    }
}