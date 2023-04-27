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

        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillDetail> BillDetails { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartDetail> CartDetails { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<InventoryTicket> InventoryTickets { get; set; }
        public virtual DbSet<InventoryTicketItem> InventoryTicketItems { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
        public virtual DbSet<ProductVariant> ProductVariants { get; set; }
        public virtual DbSet<ImageProductVariant> ImageProductVariants { get; set; }
        public virtual DbSet<ProductVariantValue> ProductVariantValues { get; set; }
        public virtual DbSet<Domain.Attribute> Attributes { get; set; }
        public virtual DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
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
            modelBuilder.Entity<ProductVariantValue>()
                .HasKey(pvv => new { 
                    pvv.ProductVariantId, 
                    pvv.ProductId, 
                    pvv.AttributeId,
                    pvv.ProductAttributeValueId
                });
            modelBuilder.Entity<ProductVariantValue>()
               .HasKey(pvv => new {
                   pvv.ProductVariantId,
                   pvv.ProductId,
                   pvv.AttributeId,
                   pvv.ProductAttributeValueId
               });
           
            modelBuilder.Entity<ProductTag>().HasKey(x => new { x.ProductId, x.TagId });
            modelBuilder.Entity<ProductAttribute>().HasKey(x => new { x.ProductId, x.AttributeId });

            modelBuilder.Entity<ProductVariantValue>()
               .HasOne(pvv => pvv.ProductAttributeValues)
               .WithMany(pad => pad.ProductVariantValues)
               .HasForeignKey(pvv => pvv.ProductAttributeValueId)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProductVariantValue>()
               .HasOne(pv => pv.ProductVariant)
               .WithMany(p => p.ProductVariantValues)
               .HasForeignKey(pv => pv.ProductVariantId)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Seed();
        }
    }
}
