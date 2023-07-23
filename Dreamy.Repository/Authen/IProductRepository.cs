using Dreamy.Domain;
using Dreamy.Domain.Shared.Dtos;
using Dreamy.Domain.Shared.Dtos.Product;
using Dreamy.Repository.Generic;

namespace Dreamy.Repository.Authen
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<ProductExecuteDto>> GetAllProduct(PagingRequest pagingRequest);
        dynamic GetTotalCountProduct();
    }
}
