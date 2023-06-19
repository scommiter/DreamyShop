using DreamyShop.Repository.Repositories.Auth;
using DreamyShop.Repository.Repositories.Bill;
using DreamyShop.Repository.Repositories.Cart;
using DreamyShop.Repository.Repositories.Category;
using DreamyShop.Repository.Repositories.Manufacturer;
using DreamyShop.Repository.Repositories.Product;
using DreamyShop.Repository.Repositories.Role;
using DreamyShop.Repository.Repositories.User;

namespace DreamyShop.Repository.RepositoryWrapper
{
    public interface IRepositoryWrapper : IDisposable
    {
        IAuthRepository Auth { get; }
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        IProductRepository Product { get; }
        IProductAttributeRepository ProductAttribute { get; }
        IProductVariantRepository ProductVariant { get; }
        IProductImageRepository ProductImage { get; }
        IProductVariantValueRepository ProductVariantValue { get; }
        IAttributeRepository Attribute { get; }
        IProductAttributeValueRepository ProductAttributeValue { get; }
        IManufacturerRepository Manufacturer { get; }
        ICategoryRepository Category { get; }
        ICartRepository Cart { get; }
        ICartDetailRepository CartDetail { get; }
        IBillRepository Bill { get; }
        IBillDetailRepository BillDetail { get; }

        void Save();
    }
}