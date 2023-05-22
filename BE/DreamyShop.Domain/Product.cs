using DreamyShop.Domain.Shared.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("Products")]
    public class Product : AuditEntity
    {
        public Product() { }
        [Key]
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        public string? Slug { get; set; }
        public int? SortOrder { get; set; }
        public ProductType ProductType { get; set; }
        public int CategoryId { get; set; }
        [StringLength(250)]
        public string? SeoMetaDescription { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisibility { get; set; }


        [ForeignKey(nameof(ManufacturerId))]
        [InverseProperty("Products")]
        public virtual Manufacturer Manufacturer { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Products")]
        public virtual ProductCategory ProductCategory { get; set; }

        [InverseProperty(nameof(ProductVariant.Product))]
        public virtual ICollection<ProductVariant> ProductVariants { get; set; }

        [InverseProperty(nameof(ProductAttributeValue.Product))]
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }

        [InverseProperty(nameof(ProductReview.Product))]
        public virtual ICollection<ProductReview> ProductReviews { get; set; }

        [InverseProperty(nameof(ProductAttribute.Product))]
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }

        [InverseProperty(nameof(ProductVariantValue.Product))]
        public virtual ICollection<ProductVariantValue> ProductVariantValues { get; set; }

        [InverseProperty(nameof(ProductTag.Product))]
        public virtual ICollection<ProductTag> ProductTags { get; set; }

        [InverseProperty(nameof(ImageProduct.Product))]
        public virtual ICollection<ImageProduct> ImageProducts { get; set; }
    }
}
