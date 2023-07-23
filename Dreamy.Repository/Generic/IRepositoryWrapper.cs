using Dreamy.Repository.Authen;

namespace Dreamy.Repository.Generic
{
    public interface IRepositoryWrapper : IDisposable
    {
        IAuthRepository Auth { get; }
        //IUserRepository User { get; }
        IRoleRepository Role { get; }
        IProductRepository Product { get; }
        //IProductAttributeRepository ProductAttribute { get; }
        //IProductVariantRepository ProductVariant { get; }
        //IProductVariantImageRepository ProductVariantImage { get; }
        //IProductVariantValueRepository ProductVariantValue { get; }
        //IAttributeRepository Attribute { get; }
        //IProductAttributeValueRepository ProductAttributeValue { get; }
        //IManufacturerRepository Manufacturer { get; }
        //ICategoryRepository Category { get; }
    }
}
