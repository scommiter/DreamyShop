using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public interface IProductRepository : IGenericRepository<Domain.Product>
    {
        Task<IEnumerable<dynamic>> GetAllProduct();
        Task CreateProduct(ProductCreateDto productCreateDto);
        Task AddAttributeProducts(List<VariantProduct> variantProducts, Dictionary<string, List<string>> productOptions, List<Domain.Attribute> attributes);
    }
}