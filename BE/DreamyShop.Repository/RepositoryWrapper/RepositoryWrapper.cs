using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Auth;
using DreamyShop.Repository.Repositories.Category;
using DreamyShop.Repository.Repositories.Manufacturer;
using DreamyShop.Repository.Repositories.Product;
using DreamyShop.Repository.Repositories.Role;
using DreamyShop.Repository.Repositories.User;
using System.Data;

namespace DreamyShop.Repository.RepositoryWrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly IDbConnection _db;
        //private IAuthRepository _auth;
        //private IUserRepository _user;
        private IProductRepository _product;
        //private IProductVariantRepository _productVariant;
        //private IProductVariantImageRepository _productVariantImage;
        //private IProductVariantValueRepository _productVariantValue;
        //private IProductAttributeRepository _productAttribute;
        //private IAttributeRepository _attribute;
        //private IProductAttributeValueRepository _productAttributeValue;
        private IManufacturerRepository _manufacturer;
        //private ICategoryRepository _category;
        //private IRoleRepository _role;
        public RepositoryWrapper(
            IDbConnection db,
            //IAuthRepository auth,
            //IUserRepository user,
            //IRoleRepository role,
            IProductRepository product,
            //IProductAttributeRepository productAttribute,
            //IProductAttributeValueRepository productAttributeValue,
            IManufacturerRepository manufacturer
            //ICategoryRepository category,
            //IAttributeRepository attribute,
            //IProductVariantRepository productVariant,
            //IProductVariantValueRepository productVariantValue,
            //IProductVariantImageRepository productVariantImage
            )
        {
            _db = db;
            //_auth = auth;
            //_user = user;
            _product = product;
            //_productAttribute = productAttribute;
            //_productAttributeValue = productAttributeValue;
            _manufacturer = manufacturer;
            //_category = category;
            //_role = role;
            //_attribute = attribute;
            //_productVariant = productVariant;
            //_productVariantValue = productVariantValue;
            //_productVariantImage = productVariantImage;
        }

        //public IAuthRepository Auth
        //{
        //    get
        //    {
        //        if (_auth == null)
        //        {
        //            _auth = new AuthRepository(_context);
        //        }
        //        return _auth;
        //    }
        //}

        //public IUserRepository User
        //{
        //    get
        //    {
        //        if (_user == null)
        //        {
        //            _user = new UserRepository(_context);
        //        }
        //        return _user;
        //    }
        //}

        //public IRoleRepository Role
        //{
        //    get
        //    {
        //        if (_role == null)
        //        {
        //            _role = new RoleRepository(_context);
        //        }
        //        return _role;
        //    }
        //}

        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_db);
                }
                return _product;
            }
        }

        //public IProductAttributeRepository ProductAttribute
        //{
        //    get
        //    {
        //        if (_productAttribute == null)
        //        {
        //            _productAttribute = new ProductAttributeRepository(_context);
        //        }
        //        return _productAttribute;
        //    }
        //}

        //public IProductAttributeValueRepository ProductAttributeValue
        //{
        //    get
        //    {
        //        if (_productAttributeValue == null)
        //        {
        //            _productAttributeValue = new ProductAttributeValueRepository(_context);
        //        }
        //        return _productAttributeValue;
        //    }
        //}
        public IManufacturerRepository Manufacturer
        {
            get
            {
                if (_manufacturer == null)
                {
                    _manufacturer = new ManufacturerRepository(_db);
                }
                return _manufacturer;
            }
        }

        //public ICategoryRepository Category
        //{
        //    get
        //    {
        //        if (_category == null)
        //        {
        //            _category = new CategoryRepository(_context);
        //        }
        //        return _category;
        //    }
        //}

        //public IAttributeRepository Attribute
        //{
        //    get
        //    {
        //        if (_attribute == null)
        //        {
        //            _attribute = new AttributeRepository(_context);
        //        }
        //        return _attribute;
        //    }
        //}

        //public IProductVariantRepository ProductVariant
        //{
        //    get
        //    {
        //        if (_productVariant == null)
        //        {
        //            _productVariant = new ProductVariantRepository(_context);
        //        }
        //        return _productVariant;
        //    }
        //}

        //public IProductVariantValueRepository ProductVariantValue
        //{
        //    get
        //    {
        //        if (_productVariantValue == null)
        //        {
        //            _productVariantValue = new ProductVariantValueRepository(_context);
        //        }
        //        return _productVariantValue;
        //    }
        //}

        //public IProductVariantImageRepository ProductVariantImage
        //{
        //    get
        //    {
        //        if (_productVariantImage == null)
        //        {
        //            _productVariantImage = new ProductVariantImageRepository(_context);
        //        }
        //        return _productVariantImage;
        //    }
        //}

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}