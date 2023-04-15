using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamyShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace DreamyShop.EntityFrameworkCore
{
    public class DreamyShopDbContext : DbContext
    {
        public DreamyShopDbContext(DbContextOptions<DreamyShopDbContext> options)
           : base(options)
        {
        }

        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<InventoryTicket> InventoryTickets { get; set; }
        public virtual DbSet<InventoryTicketItem> InventoryTicketItems { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
        public virtual DbSet<ProductVariant> ProductVariants { get; set; }
        public virtual DbSet<ProductVariantValueDecimal> ProductVariantValueDecimals { get; set; }
        public virtual DbSet<ProductVariantValueDateTime> ProductVariantValueDateTimes { get; set; }
        public virtual DbSet<ProductVariantValueInt> ProductVariantValueInts { get; set; }
        public virtual DbSet<ProductVariantValueText> ProductVariantValueTexts { get; set; }
        public virtual DbSet<ProductVariantValueVarchar> ProductVariantValueVarchars { get; set; }
        public virtual DbSet<Domain.Attribute> Attributes { get; set; }
        public virtual DbSet<ProductAttributeDateTime> ProductAttributeDateTimes { get; set; }
        public virtual DbSet<ProductAttributeDecimal> ProductAttributeDecimals { get; set; }
        public virtual DbSet<ProductAttributeInt> ProductAttributeInts { get; set; }
        public virtual DbSet<ProductAttributeText> ProductAttributeTexts { get; set; }
        public virtual DbSet<ProductAttributeVarchar> ProductAttributeVarchars { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductReview> ProductReviews { get; set; }
        public virtual DbSet<ProductTag> ProductTags { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<PromotionCategory> PromotionCategories { get; set; }
        public virtual DbSet<PromotionManufacturer> PromotionManufacturers { get; set; }
        public virtual DbSet<PromotionProduct> PromotionProducts { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductVariantValueDecimal>()
                .HasKey(pvv => new { 
                    pvv.ProductVariantId, 
                    pvv.ProductId, 
                    pvv.AttributeId,
                    pvv.ProductAttributeDecimalId});
            modelBuilder.Entity<ProductVariantValueDateTime>()
               .HasKey(pvv => new {
                   pvv.ProductVariantId,
                   pvv.ProductId,
                   pvv.AttributeId,
                   pvv.ProductAttributeDateTimeId
               });
            modelBuilder.Entity<ProductVariantValueText>()
               .HasKey(pvv => new {
                   pvv.ProductVariantId,
                   pvv.ProductId,
                   pvv.AttributeId,
                   pvv.ProductAttributeTextId
               });
            modelBuilder.Entity<ProductVariantValueVarchar>()
               .HasKey(pvv => new {
                   pvv.ProductVariantId,
                   pvv.ProductId,
                   pvv.AttributeId,
                   pvv.ProductAttributeVarcharId
               });
            modelBuilder.Entity<ProductVariantValueInt>()
               .HasKey(pvv => new {
                   pvv.ProductVariantId,
                   pvv.ProductId,
                   pvv.AttributeId,
                   pvv.ProductAttributeIntId
               });
            modelBuilder.Entity<ProductTag>().HasKey(x => new { x.ProductId, x.TagId });
            modelBuilder.Entity<ProductAttribute>().HasKey(x => new { x.ProductId, x.AttributeId });

            modelBuilder.Entity<ProductVariantValueDecimal>()
               .HasOne(pvv => pvv.ProductAttributeDecimal)
               .WithMany(pad => pad.ProductVariantValueDecimals)
               .HasForeignKey(pvv => pvv.ProductAttributeDecimalId)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProductVariantValueDecimal>()
               .HasOne(pv => pv.ProductVariant)
               .WithMany(p => p.ProductVariantValueDecimals)
               .HasForeignKey(pv => pv.ProductVariantId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductVariantValueDateTime>()
               .HasOne(pvv => pvv.ProductAttributeDateTime)
               .WithMany(pad => pad.ProductVariantValueDateTimes)
               .HasForeignKey(pvv => pvv.ProductAttributeDateTimeId)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProductVariantValueDateTime>()
                .HasOne(pv => pv.ProductVariant)
                .WithMany(p => p.ProductVariantValueDateTimes)
                .HasForeignKey(pv => pv.ProductVariantId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductVariantValueText>()
              .HasOne(pvv => pvv.ProductAttributeText)
              .WithMany(pad => pad.ProductVariantValueTexts)
              .HasForeignKey(pvv => pvv.ProductAttributeTextId)
              .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProductVariantValueText>()
              .HasOne(pv => pv.ProductVariant)
              .WithMany(p => p.ProductVariantValueTexts)
              .HasForeignKey(pv => pv.ProductVariantId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductVariantValueInt>()
               .HasOne(pvv => pvv.ProductAttributeInt)
               .WithMany(pad => pad.ProductVariantValueInts)
               .HasForeignKey(pvv => pvv.ProductAttributeIntId)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProductVariantValueInt>()
             .HasOne(pv => pv.ProductVariant)
             .WithMany(p => p.ProductVariantValueInts)
             .HasForeignKey(pv => pv.ProductVariantId)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductVariantValueVarchar>()
              .HasOne(pvv => pvv.ProductAttributeVarchar)
              .WithMany(pad => pad.ProductVariantValueVarchars)
              .HasForeignKey(pvv => pvv.ProductAttributeVarcharId)
              .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProductVariantValueVarchar>()
             .HasOne(pv => pv.ProductVariant)
             .WithMany(p => p.ProductVariantValueVarchars)
             .HasForeignKey(pv => pv.ProductVariantId)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Seed();
        }
    }
}
