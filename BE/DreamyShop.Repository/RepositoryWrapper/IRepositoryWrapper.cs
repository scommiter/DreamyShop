using DreamyShop.Repository.Repositories.Auth;
using DreamyShop.Repository.Repositories.Manufacturer;
using DreamyShop.Repository.Repositories.Product;
using DreamyShop.Repository.Repositories.User;

namespace DreamyShop.Repository.RepositoryWrapper
{
    public interface IRepositoryWrapper : IDisposable
    {
        IAuthRepository Auth { get; }
        IUserRepository User { get; }
        IProductRepository Product { get; }
        IProductAttributeRepository ProductAttribute { get; }
        IProductAttributeDateTimeRepository ProductAttributeDateTime { get; }
        IProductAttributeDecimalRepository ProductAttributeDecimal { get; }
        IProductAttributeIntRepository ProductAttributeInt { get; }
        IProductAttributeTextRepository ProductAttributeText { get; }
        IProductAttributeVarcharRepository ProductAttributeVarchar { get; }
        IManufacturerRepository Manufacturer { get; }

        void Save();
    }
}