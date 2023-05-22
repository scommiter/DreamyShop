using DreamyShop.Domain;
using DreamyShop.Domain.Shared.Types;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DreamyShop.EntityFrameworkCore
{
    public static class DataDbContext
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
               new Product()
               {
                   Id = 1,
                   ManufacturerId = 1,
                   Name = "Camera-SKS",
                   Code = "CMRSKS",
                   Slug = "camera-sks",
                   SortOrder = 1,
                   ProductType = ProductType.Single,
                   CategoryId = 1,
                   SeoMetaDescription = "Security camera, surveillance camera, wireless camera, wifi camera, high resolution, motion sensor, remote monitoring",
                   Description = "Security camera, surveillance camera, wireless camera, wifi camera, high resolution, motion sensor, remote monitoring",
                   IsActive = true,
                   IsVisibility = true,
                   DateCreated = DateTime.Now,
                   DateUpdated = DateTime.Now
               },
               new Product()
               {
                   Id = 2,
                   ManufacturerId = 4,
                   Name = "Camera-UFG",
                   Code = "CMRUFG",
                   Slug = "camera-ufg",
                   SortOrder = 2,
                   ProductType = ProductType.Single,
                   CategoryId = 1,
                   SeoMetaDescription = "XYZ wireless security camera with high resolution, built-in motion sensor, supports wifi connection, helps you observe your family, home, shop, office whenever and wherever.",
                   Description = "XYZ wireless security camera with high resolution.",
                   IsActive = true,
                   IsVisibility = true,
                   DateCreated = DateTime.Now,
                   DateUpdated = DateTime.Now
               },
               new Product()
               {
                   Id = 3,
                   ManufacturerId = 3,
                   Name = "Iphone 14 XSMax",
                   Code = "IP14XSM",
                   Slug = "ip14-xsmax",
                   SortOrder = 3,
                   ProductType = ProductType.Grouped,
                   CategoryId = 2,
                   SeoMetaDescription = "Find out about Apple's latest line of iPhones at Apple Store Vietnam. Order online and get instant deals.",
                   Description = "",
                   IsActive = true,
                   IsVisibility = true,
                   DateCreated = DateTime.Now,
                   DateUpdated = DateTime.Now
               },
               new Product()
               {
                   Id = 4,
                   ManufacturerId = 4,
                   Name = "Laptop DELL DEMON",
                   Code = "DELLDEMON",
                   Slug = "dell-demon",
                   SortOrder = 4,
                   ProductType = ProductType.Grouped,
                   CategoryId = 3,
                   SeoMetaDescription = "",
                   Description = "Buy genuine Laptop at our store with best quality and affordable price. We supply laptop products from reputable brands. Order now to get a free laptop backpack!",
                   IsActive = true,
                   IsVisibility = true,
                   DateCreated = DateTime.Now,
                   DateUpdated = DateTime.Now
               },
               new Product()
               {
                   Id = 5,
                   ManufacturerId = 2,
                   Name = "Crocodile leather bag",
                   Code = "CLBGCCI",
                   Slug = "clbcci",
                   SortOrder = 4,
                   ProductType = ProductType.Grouped,
                   CategoryId = 4,
                   SeoMetaDescription = "",
                   Description = "",
                   IsActive = true,
                   IsVisibility = true,
                   DateCreated = DateTime.Now,
                   DateUpdated = DateTime.Now
               });

            modelBuilder.Entity<Manufacturer>().HasData(
               new Manufacturer()
               {
                   Id = 1,
                   Name = "Sony",
                   Code = "SN",
                   Slug = "sony",
                   CoverPicture = "",
                   IsVisibility = true,
                   IsActive = true,
                   Country = "Japan"
               },
               new Manufacturer()
               {
                   Id = 2,
                   Name = "Gucci",
                   Code = "GCCI",
                   Slug = "gucci",
                   CoverPicture = "",
                   IsVisibility = true,
                   IsActive = true,
                   Country = "Italy"
               },
               new Manufacturer()
               {
                   Id = 3,
                   Name = "Apple",
                   Code = "IPAPL",
                   Slug = "apple",
                   CoverPicture = "",
                   IsVisibility = true,
                   IsActive = true,
                   Country = "US"
               },
               new Manufacturer()
               {
                   Id = 4,
                   Name = "DELL",
                   Code = "dell",
                   Slug = "dell",
                   CoverPicture = "",
                   IsVisibility = true,
                   IsActive = true,
                   Country = "Texas-USA"
               },
               new Manufacturer()
               {
                   Id = 5,
                   Name = "ASUS",
                   Code = "asus",
                   Slug = "asus",
                   CoverPicture = "",
                   IsVisibility = true,
                   IsActive = true,
                   Country = "Taiwan"
               });

            modelBuilder.Entity<ProductCategory>().HasData(
              new ProductCategory()
              {
                  Id = 1,
                  Name = "Camera",
                  Code = "CMR",
                  Slug = "camera",
                  SortOrder = 1,
                  CoverPicture = "",
                  IsVisibility = true,
                  IsActive = true,
                  ParentId = null,
                  SeoMetaDescription = ""
              },
              new ProductCategory()
              {
                  Id = 2,
                  Name = "Iphone",
                  Code = "IP",
                  Slug = "iphone",
                  SortOrder = 2,
                  CoverPicture = "",
                  IsVisibility = true,
                  IsActive = true,
                  ParentId = null,
                  SeoMetaDescription = ""
              },
              new ProductCategory()
              {
                  Id = 3,
                  Name = "Laptop",
                  Code = "LP",
                  Slug = "laptop",
                  SortOrder = 3,
                  CoverPicture = "",
                  IsVisibility = true,
                  IsActive = true,
                  ParentId = null,
                  SeoMetaDescription = ""
              },
              new ProductCategory()
              {
                  Id = 4,
                  Name = "Jewelry",
                  Code = "JLY",
                  Slug = "jly",
                  SortOrder = 4,
                  CoverPicture = "",
                  IsVisibility = true,
                  IsActive = true,
                  ParentId = null,
                  SeoMetaDescription = ""
              });

            modelBuilder.Entity<Domain.Attribute>().HasData(
                new Domain.Attribute()
                {
                    Id = 1,
                    Code = "COLOR",
                    Name = "COLOR",
                    SortOrder = 1,
                    IsVisibility = true,
                    IsActive = true,
                    IsUnique = true,
                    Note = "Product color"
                },
                new Domain.Attribute()
                {
                    Id = 2,
                    Code = "SIZE",
                    Name = "SIZE",
                    SortOrder = 2,
                    IsVisibility = true,
                    IsActive = true,
                    IsUnique = true,
                    Note = ""
                },
                new Domain.Attribute()
                {
                    Id = 3,
                    Code = "MATERIAL",
                    Name = "MATERIAL",
                    SortOrder = 3,
                    IsVisibility = true,
                    IsActive = true,
                    IsUnique = true,
                    Note = ""
                });

            modelBuilder.Entity<ProductAttribute>().HasData(
                //Bag -  Color
                new ProductAttribute()
                {
                    ProductId = 5,
                    AttributeId = 1
                },
                //Bag - SIZE
                new ProductAttribute()
                {
                    ProductId = 5, 
                    AttributeId = 2
                },
                //Bag - Material
                new ProductAttribute()
                {
                    ProductId = 5,
                    AttributeId = 3
                });

            modelBuilder.Entity<ProductVariant>().HasData(
                 new ProductVariant()
                 {
                     Id = 1,
                     ProductId = 3,
                     SKU = "IP14-A",
                     IsVisibility = true,
                     IsActive = true,
                     Description = "",
                     Quantity = 12,
                     Price = 1200,
                     ThumbnailPicture = ""
                 },
                 new ProductVariant()
                 {
                     Id = 2,
                     ProductId = 3,
                     SKU = "IP14-B",
                     IsVisibility = true,
                     IsActive = true,
                     Description = "",
                     Quantity = 20,
                     Price = 1250,
                     ThumbnailPicture = ""
                 },
                 new ProductVariant()
                 {
                     Id = 3,
                     ProductId = 5,
                     SKU = "BAGGUCCI-A",
                     IsVisibility = true,
                     IsActive = true,
                     Description = "",
                     Quantity = 10,
                     Price = 5000,
                     ThumbnailPicture = ""
                 },
                 new ProductVariant()
                 {
                     Id =4,
                     ProductId = 5,
                     SKU = "BAGGUCCI-B",
                     IsVisibility = true,
                     IsActive = true,
                     Description = "",
                     Quantity = 15,
                     Price = 4500,
                     ThumbnailPicture = ""
                 },
                 new ProductVariant()
                 {
                     Id = 5,
                     ProductId = 5,
                     SKU = "BAGGUCCI-VIP",
                     IsVisibility = true,
                     IsActive = true,
                     Description = "",
                     Quantity = 5,
                     Price = 9500,
                     ThumbnailPicture = ""
                 });

            modelBuilder.Entity<ProductAttributeValue>().HasData(
              new ProductAttributeValue()
              {
                  Id = 1,
                  AttributeId = 1,
                  ProductId = 5,
                  Value = "Red"
              },
              new ProductAttributeValue()
              {
                  Id = 2,
                  AttributeId = 1,
                  ProductId = 5,
                  Value = "Blue"
              },
              new ProductAttributeValue()
              {
                  Id = 3,
                  AttributeId = 2,
                  ProductId = 5,
                  Value = "M"
              },
              new ProductAttributeValue()
              {
                  Id = 4,
                  AttributeId = 2,
                  ProductId = 5,
                  Value = "L"
              },
              new ProductAttributeValue()
              {
                  Id = 5,
                  AttributeId = 3,
                  ProductId = 5,
                  Value = "Leather"
              },
              new ProductAttributeValue()
              {
                  Id = 6,
                  AttributeId = 3,
                  ProductId = 5,
                  Value = "Cotton"
              },
              new ProductAttributeValue()
              {
                  Id = 7,
                  AttributeId = 3,
                  ProductId = 5,
                  Value = "Crocodile skin"
              },
              new ProductAttributeValue()
              {
                  Id = 8,
                  AttributeId = 1,
                  ProductId = 3,
                  Value = "White"
              },
              new ProductAttributeValue()
              {
                  Id = 9,
                  AttributeId = 1,
                  ProductId = 3,
                  Value = "Black"
              });

            modelBuilder.Entity<ProductVariantValue>().HasData(
            #region  Crocodile leather bag - Red - M - Leather - Quantity: 10 - Price: 5000
                 new ProductVariantValue()
                 {
                     ProductVariantId = 3,
                     ProductId = 5,
                     AttributeId = 1,    //color
                     ProductAttributeValueId = 1,     //Red
                 },
                 new ProductVariantValue()
                 {
                     ProductVariantId = 3,
                     ProductId = 5,
                     AttributeId = 2,    //Size 
                     ProductAttributeValueId = 3,     //M
                 },
                 new ProductVariantValue()
                 {
                     ProductVariantId = 3,
                     ProductId = 5,
                     AttributeId = 3,    //Material
                     ProductAttributeValueId = 5,     //Leather
                 },
            #endregion
            #region  Crocodile leather bag - Blue - L - Cotton - Quantity: 15 - Price: 4500
                 new ProductVariantValue()
                 {
                     ProductVariantId = 4,
                     ProductId = 5,
                     AttributeId = 1,    //color
                     ProductAttributeValueId = 2,     //Blue
                 },
                 new ProductVariantValue()
                 {
                     ProductVariantId = 4,
                     ProductId = 5,
                     AttributeId = 2,    //Size 
                     ProductAttributeValueId = 4,     //L
                 },
                 new ProductVariantValue()
                 {
                     ProductVariantId = 4,
                     ProductId = 5,
                     AttributeId = 3,     //Material
                     ProductAttributeValueId = 6,     //Cotton
                 },
            #endregion
            #region  Crocodile leather bag - Blue - Crocodile skin - Quantity: 5 - Price: 9500
                 new ProductVariantValue()
                 {
                     ProductVariantId = 5,
                     ProductId = 5,
                     AttributeId = 1,    //color
                     ProductAttributeValueId = 2,     //Blue
                 },
                 new ProductVariantValue()
                 {
                     ProductVariantId = 5,
                     ProductId = 5,
                     AttributeId = 2,    //Size 
                     ProductAttributeValueId = 7,     //Crocodile skin
                 },
             #endregion
            #region  IP14-A - White - Quantity: 12 - Price: 1200
                 new ProductVariantValue()
                 {
                     ProductVariantId = 1,
                     ProductId = 3,
                     AttributeId = 1,    //color
                     ProductAttributeValueId = 8,     //White
                 },
            #endregion
            #region  IP14-B - Black - Quantity: 20 - Price: 1250
                 new ProductVariantValue()
                 {
                     ProductVariantId = 2,
                     ProductId = 3,
                     AttributeId = 1,    //color
                     ProductAttributeValueId = 9,     //Black
                 }
           #endregion
                 );
        }
    }
}
