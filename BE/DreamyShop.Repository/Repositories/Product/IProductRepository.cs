using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Product
{
    public interface IProductRepository : IGenericRepository<Domain.Product>
    {
        Task<IEnumerable<Domain.Product>> GetAllProduct();
    }
}