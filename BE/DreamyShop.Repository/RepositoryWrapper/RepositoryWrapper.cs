using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Auth;
using DreamyShop.Repository.Repositories.Category;
using DreamyShop.Repository.Repositories.Manufacturer;
using DreamyShop.Repository.Repositories.Product;
using DreamyShop.Repository.Repositories.Role;
using DreamyShop.Repository.Repositories.User;

namespace DreamyShop.Repository.RepositoryWrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly DreamyShopDbContext _context;
        private IAuthRepository _auth;
        private IUserRepository _user;
        private IProductRepository _product;
        private IProductAttributeRepository _productAttribute;
        private IProductAttributeValueRepository _productAttributeValue;
        private IManufacturerRepository _manufacturer;
        private ICategoryRepository _category;
        private IRoleRepository _role;
        public RepositoryWrapper(
            DreamyShopDbContext context, 
            IAuthRepository auth,
            IUserRepository user,
            IRoleRepository role,
            IProductRepository product,
            IProductAttributeRepository productAttribute,
            IProductAttributeValueRepository productAttributeValue,
            IManufacturerRepository manufacturer,
            ICategoryRepository category)
        {
            _context = context;
            _auth = auth;
            _user = user;
            _product = product;
            _productAttribute = productAttribute;
            _productAttributeValue = productAttributeValue;
            _manufacturer = manufacturer;
            _category = category;
            _role = role;
        }

        public IAuthRepository Auth
        {
            get
            {
                if (_auth == null)
                {
                    _auth = new AuthRepository(_context);
                }
                return _auth;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_context);
                }
                return _user;
            }
        }

        public IRoleRepository Role
        {
            get
            {
                if (_role == null)
                {
                    _role = new RoleRepository(_context);
                }
                return _role;
            }
        }

        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_context);
                }
                return _product;
            }
        }

        public IProductAttributeRepository ProductAttribute
        {
            get
            {
                if (_productAttribute == null)
                {
                    _productAttribute = new ProductAttributeRepository(_context);
                }
                return _productAttribute;
            }
        }

        public IProductAttributeValueRepository ProductAttributeValue
        {
            get
            {
                if (_productAttributeValue == null)
                {
                    _productAttributeValue = new ProductAttributeValueRepository(_context);
                }
                return _productAttributeValue;
            }
        }
        public IManufacturerRepository Manufacturer
        {
            get
            {
                if (_manufacturer == null)
                {
                    _manufacturer = new ManufacturerRepository(_context);
                }
                return _manufacturer;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_context);
                }
                return _category;
            }
        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}