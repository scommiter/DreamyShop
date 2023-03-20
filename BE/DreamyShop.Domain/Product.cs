using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain
{
    [Table("Products")]
    public class Product
    {
        public Product(
            Guid id, 
            Guid manufacturerId, 
            string name, string code, 
            string slug, 
            int sortOrder, 
            bool isVisibility, 
            bool isActive, 
            Guid categoryId, 
            string seoMetaDescription, 
            string description, 
            string thumbnailPicture, 
            double price, 
            string categoryName, 
            string categorySlug)
        {
            Id = id;
            ManufacturerId = manufacturerId;
            Name = name;
            Code = code;
            Slug = slug;
            SortOrder = sortOrder;
            IsVisibility = isVisibility;
            IsActive = isActive;
            CategoryId = categoryId;
            SeoMetaDescription = seoMetaDescription;
            Description = description;
            ThumbnailPicture = thumbnailPicture;
            Price = price;
            CategoryName = categoryName;
            CategorySlug = categorySlug;
        }
        [Key]
        public Guid Id { get; set; }
        public Guid ManufacturerId { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        public string Slug { get; set; }
        public int SortOrder { get; set; }
        public bool IsVisibility { get; set; }
        public bool IsActive { get; set; }
        public Guid CategoryId { get; set; }
        [StringLength(250)]
        public string SeoMetaDescription { get; set; }
        public string Description { get; set; }
        [StringLength(250)]
        public string ThumbnailPicture { get; set; }
        public double Price { get; set; }
        [StringLength(250)]
        public string CategoryName { get; set; }
        [StringLength(250)]
        public string CategorySlug { get; set; }

        [ForeignKey(nameof(ManufacturerId))]
        [InverseProperty("Products")]
        public virtual Manufacturer Manufacturer { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Products")]
        public virtual ProductCategory ProductCategory { get; set; }

        [InverseProperty(nameof(ProductAttribute.Product))]
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }

        [InverseProperty(nameof(ProductReview.Product))]
        public virtual ICollection<ProductReview> ProductReviews { get; set; }

        [InverseProperty(nameof(ProductTag.Product))]
        public virtual ICollection<ProductTag> ProductTags { get; set; }
    }
}
