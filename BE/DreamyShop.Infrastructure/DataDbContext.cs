using DreamyShop.Domain;
using DreamyShop.Domain.Shared.Types;
using Microsoft.EntityFrameworkCore;

namespace DreamyShop.EntityFrameworkCore
{
    public static class DataDbContext
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
               new Product()
               {
                   Id = new Guid("1747CDF9-3ACB-4001-8F52-EE7F387F8EFB"),
                   ManufacturerId = new Guid("57A5F678-43F0-4648-92D8-16BD09D7143E"),
                   Name = "Camera-SKS",
                   Code = "CMRSKS",
                   Slug = "camera-sks",
                   SortOrder = 1,
                   ProductType = ProductType.Single,
                   CategoryId = new Guid("96BFF1B2-3715-4F10-90D3-AAABB332E0E9"),
                   SeoMetaDescription = "Security camera, surveillance camera, wireless camera, wifi camera, high resolution, motion sensor, remote monitoring",
                   Description = "Security camera, surveillance camera, wireless camera, wifi camera, high resolution, motion sensor, remote monitoring",
                   ThumbnailPicture = ""
               },
               new Product()
               {
                   Id = new Guid("85F8B0C3-CB8D-4CCB-9544-19DAAD6EF352"),
                   ManufacturerId = new Guid("57A5F678-43F0-4648-92D8-16BD09D7143E"),
                   Name = "Camera-UFG",
                   Code = "CMRUFG",
                   Slug = "camera-ufg",
                   SortOrder = 2,
                   ProductType = ProductType.Single,
                   CategoryId = new Guid("96BFF1B2-3715-4F10-90D3-AAABB332E0E9"),
                   SeoMetaDescription = "XYZ wireless security camera with high resolution, built-in motion sensor, supports wifi connection, helps you observe your family, home, shop, office whenever and wherever.",
                   Description = "XYZ wireless security camera with high resolution.",
                   ThumbnailPicture = ""
               },
               new Product()
               {
                   Id = new Guid("30299235-6937-41B7-A76D-14584F5F856A"),
                   ManufacturerId = new Guid("80CAD838-29C7-4A02-81C0-9EBE78A0A273"),
                   Name = "Iphone 14 XSMax",
                   Code = "IP14XSM",
                   Slug = "ip14-xsmax",
                   SortOrder = 3,
                   ProductType = ProductType.Grouped,
                   CategoryId = new Guid("EFD560A8-C65B-439C-AF43-765DA733F3C1"),
                   SeoMetaDescription = "Find out about Apple's latest line of iPhones at Apple Store Vietnam. Order online and get instant deals.",
                   Description = "",
                   ThumbnailPicture = ""
               },
               new Product()
               {
                   Id = new Guid("215E9DEE-1D6C-40F4-9233-BB810509ADAA"),
                   ManufacturerId = new Guid("B9BE517B-72AA-46F1-9A98-A0B993CD2CF7"),
                   Name = "Laptop DELL DEMON",
                   Code = "DELLDEMON",
                   Slug = "dell-demon",
                   SortOrder = 4,
                   ProductType = ProductType.Grouped,
                   CategoryId = new Guid("2ED8E62D-2F2E-4957-AE81-8A07B0BCD443"),
                   SeoMetaDescription = "",
                   Description = "Buy genuine Laptop at our store with best quality and affordable price. We supply laptop products from reputable brands. Order now to get a free laptop backpack!",
                   ThumbnailPicture = ""
               },
               new Product()
               {
                   Id = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                   ManufacturerId = new Guid("41C2C299-EA5F-4C23-992D-E6F043F1B26F"),
                   Name = "Crocodile leather bag",
                   Code = "CLBGCCI",
                   Slug = "clbcci",
                   SortOrder = 4,
                   ProductType = ProductType.Grouped,
                   CategoryId = new Guid("7375FAB5-4FF3-43D0-A707-A56062E161BE"),
                   SeoMetaDescription = "",
                   Description = "",
                   ThumbnailPicture = ""
               });

            modelBuilder.Entity<Manufacturer>().HasData(
               new Manufacturer()
               {
                   Id = new Guid("57A5F678-43F0-4648-92D8-16BD09D7143E"),
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
                   Id = new Guid("41C2C299-EA5F-4C23-992D-E6F043F1B26F"),
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
                   Id = new Guid("80CAD838-29C7-4A02-81C0-9EBE78A0A273"),
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
                   Id = new Guid("B9BE517B-72AA-46F1-9A98-A0B993CD2CF7"),
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
                   Id = new Guid("69D0372B-DBF5-4B70-9BEB-0E4EA77F243A"),
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
                  Id = new Guid("96BFF1B2-3715-4F10-90D3-AAABB332E0E9"),
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
                  Id = new Guid("EFD560A8-C65B-439C-AF43-765DA733F3C1"),
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
                  Id = new Guid("2ED8E62D-2F2E-4957-AE81-8A07B0BCD443"),
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
                  Id = new Guid("7375FAB5-4FF3-43D0-A707-A56062E161BE"),
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
                    Id = new Guid("03B9545D-09BD-4B83-808D-DE2208E9D26A"),
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
                    Id = new Guid("F9885DFB-02A8-4065-A4AA-18B29E48EE89"),
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
                    Id = new Guid("827CA5B7-0087-4256-BEC0-399199A518D9"),
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
                    ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                    AttributeId = new Guid("03B9545D-09BD-4B83-808D-DE2208E9D26A")
                },
                //Bag - SIZE
                new ProductAttribute()
                {
                    ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"), 
                    AttributeId = new Guid("F9885DFB-02A8-4065-A4AA-18B29E48EE89")
                },
                //Bag - Material
                new ProductAttribute()
                {
                    ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                    AttributeId = new Guid("827CA5B7-0087-4256-BEC0-399199A518D9")
                });

            modelBuilder.Entity<ProductVariant>().HasData(
                 new ProductVariant()
                 {
                     Id = new Guid("fc364f29-fcb9-44b7-8854-dfce09824c35"),
                     ProductId = new Guid("30299235-6937-41B7-A76D-14584F5F856A"),
                     SKU = "IP14-A",
                     IsVisibility = true,
                     IsActive = true,
                     Description = "",
                     Quantity = 12,
                     Price = 1200
                 },
                 new ProductVariant()
                 {
                     Id = new Guid("0A134C80-0493-458A-9F02-16361F0DF5C7"),
                     ProductId = new Guid("30299235-6937-41B7-A76D-14584F5F856A"),
                     SKU = "IP14-B",
                     IsVisibility = true,
                     IsActive = true,
                     Description = "",
                     Quantity = 20,
                     Price = 1250
                 },
                 new ProductVariant()
                 {
                     Id = new Guid("AFD19304-3A45-4304-B2BB-1040F000C369"),
                     ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                     SKU = "BAGGUCCI-A",
                     IsVisibility = true,
                     IsActive = true,
                     Description = "",
                     Quantity = 10,
                     Price = 5000
                 },
                 new ProductVariant()
                 {
                     Id = new Guid("35B39A36-5970-42AA-B996-55170555F85B"),
                     ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                     SKU = "BAGGUCCI-B",
                     IsVisibility = true,
                     IsActive = true,
                     Description = "",
                     Quantity = 15,
                     Price = 4500
                 },
                 new ProductVariant()
                 {
                     Id = new Guid("F2BA4AB5-46C4-4CE9-BAAF-2C98972D45B0"),
                     ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                     SKU = "BAGGUCCI-VIP",
                     IsVisibility = true,
                     IsActive = true,
                     Description = "",
                     Quantity = 5,
                     Price = 9500
                 });

            modelBuilder.Entity<ProductAttributeValue>().HasData(
              new ProductAttributeValue()
              {
                  Id = new Guid("3D44B2AC-0BD6-433B-9858-ABB84D74EA2E"),
                  AttributeId = new Guid("03B9545D-09BD-4B83-808D-DE2208E9D26A"),
                  ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                  Value = "Red"
              },
              new ProductAttributeValue()
              {
                  Id = new Guid("3D369A2C-6BCB-468C-B296-64D96A84258A"),
                  AttributeId = new Guid("03B9545D-09BD-4B83-808D-DE2208E9D26A"),
                  ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                  Value = "Blue"
              },
              new ProductAttributeValue()
              {
                  Id = new Guid("DFDBE11D-C978-4CC4-9A9E-CAFC19805AC8"),
                  AttributeId = new Guid("F9885DFB-02A8-4065-A4AA-18B29E48EE89"),
                  ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                  Value = "M"
              },
              new ProductAttributeValue()
              {
                  Id = new Guid("6BB0A02B-D294-496A-B496-994D3DFAA6F2"),
                  AttributeId = new Guid("F9885DFB-02A8-4065-A4AA-18B29E48EE89"),
                  ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                  Value = "L"
              },
              new ProductAttributeValue()
              {
                  Id = new Guid("A2EE0AC5-663D-4D97-B449-ED8FE48FADA3"),
                  AttributeId = new Guid("827CA5B7-0087-4256-BEC0-399199A518D9"),
                  ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                  Value = "Leather"
              },
              new ProductAttributeValue()
              {
                  Id = new Guid("2C2FCF7C-FA5A-4B6F-AC30-48A2A6BDB4B5"),
                  AttributeId = new Guid("827CA5B7-0087-4256-BEC0-399199A518D9"),
                  ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                  Value = "Cotton"
              },
              new ProductAttributeValue()
              {
                  Id = new Guid("4267FEEE-9A5B-4156-90B0-9E3585A8AF22"),
                  AttributeId = new Guid("827CA5B7-0087-4256-BEC0-399199A518D9"),
                  ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                  Value = "Crocodile skin"
              },
              new ProductAttributeValue()
              {
                  Id = new Guid("59327BBA-3A1D-40AD-82C2-99A019E9D3F6"),
                  AttributeId = new Guid("03B9545D-09BD-4B83-808D-DE2208E9D26A"),
                  ProductId = new Guid("30299235-6937-41B7-A76D-14584F5F856A"),
                  Value = "White"
              },
              new ProductAttributeValue()
              {
                  Id = new Guid("64E5D43E-A34C-4EAB-8672-F7162DD63803"),
                  AttributeId = new Guid("03B9545D-09BD-4B83-808D-DE2208E9D26A"),
                  ProductId = new Guid("30299235-6937-41B7-A76D-14584F5F856A"),
                  Value = "Black"
              });

            modelBuilder.Entity<ProductVariantValue>().HasData(
            #region  Crocodile leather bag - Red - M - Leather - Quantity: 10 - Price: 5000
                 new ProductVariantValue()
                 {
                     ProductVariantId = new Guid("AFD19304-3A45-4304-B2BB-1040F000C369"),
                     ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                     AttributeId = new Guid("03B9545D-09BD-4B83-808D-DE2208E9D26A"),    //color
                     ProductAttributeValueId = new Guid("3D44B2AC-0BD6-433B-9858-ABB84D74EA2E"),     //Red
                 },
                 new ProductVariantValue()
                 {
                     ProductVariantId = new Guid("AFD19304-3A45-4304-B2BB-1040F000C369"),
                     ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                     AttributeId = new Guid("F9885DFB-02A8-4065-A4AA-18B29E48EE89"),    //Size 
                     ProductAttributeValueId = new Guid("DFDBE11D-C978-4CC4-9A9E-CAFC19805AC8"),     //M
                 },
                 new ProductVariantValue()
                 {
                     ProductVariantId = new Guid("AFD19304-3A45-4304-B2BB-1040F000C369"),
                     ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                     AttributeId = new Guid("827CA5B7-0087-4256-BEC0-399199A518D9"),    //Material
                     ProductAttributeValueId = new Guid("A2EE0AC5-663D-4D97-B449-ED8FE48FADA3"),     //Leather
                 },
            #endregion
            #region  Crocodile leather bag - Blue - L - Cotton - Quantity: 15 - Price: 4500
                 new ProductVariantValue()
                 {
                     ProductVariantId = new Guid("35B39A36-5970-42AA-B996-55170555F85B"),
                     ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                     AttributeId = new Guid("03B9545D-09BD-4B83-808D-DE2208E9D26A"),    //color
                     ProductAttributeValueId = new Guid("3D369A2C-6BCB-468C-B296-64D96A84258A"),     //Blue
                 },
                 new ProductVariantValue()
                 {
                     ProductVariantId = new Guid("35B39A36-5970-42AA-B996-55170555F85B"),
                     ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                     AttributeId = new Guid("F9885DFB-02A8-4065-A4AA-18B29E48EE89"),    //Size 
                     ProductAttributeValueId = new Guid("6BB0A02B-D294-496A-B496-994D3DFAA6F2"),     //L
                 },
                 new ProductVariantValue()
                 {
                     ProductVariantId = new Guid("35B39A36-5970-42AA-B996-55170555F85B"),
                     ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                     AttributeId = new Guid("827CA5B7-0087-4256-BEC0-399199A518D9"),    //Material
                     ProductAttributeValueId = new Guid("2C2FCF7C-FA5A-4B6F-AC30-48A2A6BDB4B5"),     //Cotton
                 },
            #endregion
            #region  Crocodile leather bag - Blue - Crocodile skin - Quantity: 5 - Price: 9500
                 new ProductVariantValue()
                 {
                     ProductVariantId = new Guid("F2BA4AB5-46C4-4CE9-BAAF-2C98972D45B0"),
                     ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                     AttributeId = new Guid("03B9545D-09BD-4B83-808D-DE2208E9D26A"),    //color
                     ProductAttributeValueId = new Guid("3D369A2C-6BCB-468C-B296-64D96A84258A"),     //Blue
                 },
                 new ProductVariantValue()
                 {
                     ProductVariantId = new Guid("F2BA4AB5-46C4-4CE9-BAAF-2C98972D45B0"),
                     ProductId = new Guid("E914FD7B-9AF8-403E-9F32-803346659264"),
                     AttributeId = new Guid("827CA5B7-0087-4256-BEC0-399199A518D9"),    //Size 
                     ProductAttributeValueId = new Guid("4267FEEE-9A5B-4156-90B0-9E3585A8AF22"),     //Crocodile skin
                 },
             #endregion
            #region  IP14-A - White - Quantity: 12 - Price: 1200
                 new ProductVariantValue()
                 {
                     ProductVariantId = new Guid("fc364f29-fcb9-44b7-8854-dfce09824c35"),
                     ProductId = new Guid("30299235-6937-41B7-A76D-14584F5F856A"),
                     AttributeId = new Guid("03B9545D-09BD-4B83-808D-DE2208E9D26A"),    //color
                     ProductAttributeValueId = new Guid("59327BBA-3A1D-40AD-82C2-99A019E9D3F6"),     //White
                 },
            #endregion
            #region  IP14-B - Black - Quantity: 20 - Price: 1250
                 new ProductVariantValue()
                 {
                     ProductVariantId = new Guid("0A134C80-0493-458A-9F02-16361F0DF5C7"),
                     ProductId = new Guid("30299235-6937-41B7-A76D-14584F5F856A"),
                     AttributeId = new Guid("03B9545D-09BD-4B83-808D-DE2208E9D26A"),    //color
                     ProductAttributeValueId = new Guid("64E5D43E-A34C-4EAB-8672-F7162DD63803"),     //Black
                 }
           #endregion
                 );
        }
    }
}
