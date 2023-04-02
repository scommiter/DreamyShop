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
                   IsVisibility = true,
                   IsActive = true,
                   CategoryId = new Guid("96BFF1B2-3715-4F10-90D3-AAABB332E0E9"),
                   SeoMetaDescription = "Security camera, surveillance camera, wireless camera, wifi camera, high resolution, motion sensor, remote monitoring",
                   Description = "Security camera, surveillance camera, wireless camera, wifi camera, high resolution, motion sensor, remote monitoring",
                   ThumbnailPicture = "",
                   Price = 1200
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
                   IsVisibility = true,
                   IsActive = true,
                   CategoryId = new Guid("96BFF1B2-3715-4F10-90D3-AAABB332E0E9"),
                   SeoMetaDescription = "XYZ wireless security camera with high resolution, built-in motion sensor, supports wifi connection, helps you observe your family, home, shop, office whenever and wherever.",
                   Description = "XYZ wireless security camera with high resolution.",
                   ThumbnailPicture = "",
                   Price = 1999
               },
               new Product()
               {
                   Id = new Guid("30299235-6937-41B7-A76D-14584F5F856A"),
                   ManufacturerId = new Guid("80CAD838-29C7-4A02-81C0-9EBE78A0A273"),
                   Name = "Iphone 14 XSMax",
                   Code = "IP14XSM",
                   Slug = "ip14-xsmax",
                   SortOrder = 1,
                   ProductType = ProductType.Grouped,
                   IsVisibility = true,
                   IsActive = true,
                   CategoryId = new Guid("EFD560A8-C65B-439C-AF43-765DA733F3C1"),
                   SeoMetaDescription = "Find out about Apple's latest line of iPhones at Apple Store Vietnam. Order online and get instant deals.",
                   Description = "",
                   ThumbnailPicture = "",
                   Price = 3000
               },
               new Product()
               {
                   Id = new Guid("215E9DEE-1D6C-40F4-9233-BB810509ADAA"),
                   ManufacturerId = new Guid("B9BE517B-72AA-46F1-9A98-A0B993CD2CF7"),
                   Name = "Laptop DELL DEMON",
                   Code = "DELLDEMON",
                   Slug = "dell-demon",
                   SortOrder = 1,
                   ProductType = ProductType.Grouped,
                   IsVisibility = true,
                   IsActive = true,
                   CategoryId = new Guid("2ED8E62D-2F2E-4957-AE81-8A07B0BCD443"),
                   SeoMetaDescription = "",
                   Description = "Buy genuine Laptop at our store with best quality and affordable price. We supply laptop products from reputable brands. Order now to get a free laptop backpack!",
                   ThumbnailPicture = "",
                   Price = 2500
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
                  SortOrder = 1,
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
                  SortOrder = 1,
                  CoverPicture = "",
                  IsVisibility = true,
                  IsActive = true,
                  ParentId = null,
                  SeoMetaDescription = ""
              });
        }
    }
}
