using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Repositories.Auth;
using DreamyShop.Repository.Repositories.Manufacturer;
using DreamyShop.Repository.Repositories.Product;
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
        private IProductAttributeDateTimeRepository _productAttributeDateTime;
        private IProductAttributeDecimalRepository _productAttributeDecimal;
        private IProductAttributeIntRepository _productAttributeInt;
        private IProductAttributeTextRepository _productAttributeText;
        private IProductAttributeVarcharRepository _productAttributeVarchar;
        private IManufacturerRepository _manufacturer;
        public RepositoryWrapper(
            DreamyShopDbContext context, 
            IAuthRepository auth,
            IUserRepository user,
            IProductRepository product,
            IProductAttributeRepository productAttribute,
            IProductAttributeDateTimeRepository productAttributeDateTime,
            IProductAttributeDecimalRepository productAttributeDecimal,
            IProductAttributeIntRepository productAttributeInt,
            IProductAttributeTextRepository productAttributeText,
            IProductAttributeVarcharRepository productAttributeVarchar,
            IManufacturerRepository manufacturer)
        {
            _context = context;
            _auth = auth;
            _user = user;
            _product = product;
            _productAttribute = productAttribute;
            _productAttributeDateTime = productAttributeDateTime;
            _productAttributeDecimal = productAttributeDecimal;
            _productAttributeInt = productAttributeInt;
            _productAttributeText = productAttributeText;
            _productAttributeVarchar = productAttributeVarchar;
            _manufacturer = manufacturer;
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

        public IProductAttributeDateTimeRepository ProductAttributeDateTime
        {
            get
            {
                if (_productAttributeDateTime == null)
                {
                    _productAttributeDateTime = new ProductAttributeDateTimeRepository(_context);
                }
                return _productAttributeDateTime;
            }
        }

        public IProductAttributeDecimalRepository ProductAttributeDecimal
        {
            get
            {
                if (_productAttributeDecimal == null)
                {
                    _productAttributeDecimal = new ProductAttributeDecimalRepository(_context);
                }
                return _productAttributeDecimal;
            }
        }

        public IProductAttributeIntRepository ProductAttributeInt
        {
            get
            {
                if (_productAttributeInt == null)
                {
                    _productAttributeInt = new ProductAttributeIntRepository(_context);
                }
                return _productAttributeInt;
            }
        }

        public IProductAttributeTextRepository ProductAttributeText
        {
            get
            {
                if (_productAttributeText == null)
                {
                    _productAttributeText = new ProductAttributeTextRepository(_context);
                }
                return _productAttributeText;
            }
        }

        public IProductAttributeVarcharRepository ProductAttributeVarchar
        {
            get
            {
                if (_productAttributeVarchar == null)
                {
                    _productAttributeVarchar = new ProductAttributeVarcharRepository(_context);
                }
                return _productAttributeVarchar;
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