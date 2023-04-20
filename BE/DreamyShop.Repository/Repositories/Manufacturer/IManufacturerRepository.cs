using DreamyShop.Repository.Repositories.Generic;

namespace DreamyShop.Repository.Repositories.Manufacturer
{
    public interface IManufacturerRepository : IGenericRepository<Domain.Manufacturer>
    {
        Domain.Manufacturer GetByName(string name);
    }
}