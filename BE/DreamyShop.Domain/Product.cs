using DreamyShop.Domain.Shared.Types;
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
    public class Product : AuditEntity
    {
        public Product() { }
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
        public ProductType ProductType { get; set; }
        public Guid CategoryId { get; set; }
        [StringLength(250)]
        public string SeoMetaDescription { get; set; }
        public string Description { get; set; }
        [StringLength(250)]
        public string ThumbnailPicture { get; set; }


        [ForeignKey(nameof(ManufacturerId))]
        [InverseProperty("Products")]
        public virtual Manufacturer Manufacturer { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Products")]
        public virtual ProductCategory ProductCategory { get; set; }

        [InverseProperty(nameof(ProductVariant.Product))]
        public virtual ICollection<ProductVariant> ProductVariants { get; set; }

        [InverseProperty(nameof(ProductAttributeDateTime.Product))]
        public virtual ICollection<ProductAttributeDateTime> ProductAttributeDateTimes { get; set; }

        [InverseProperty(nameof(ProductAttributeDecimal.Product))]
        public virtual ICollection<ProductAttributeDecimal> ProductAttributeDecimals { get; set; }

        [InverseProperty(nameof(ProductAttributeInt.Product))]
        public virtual ICollection<ProductAttributeInt> ProductAttributeInts { get; set; }

        [InverseProperty(nameof(ProductAttributeText.Product))]
        public virtual ICollection<ProductAttributeText> ProductAttributeTexts { get; set; }

        [InverseProperty(nameof(ProductAttributeVarchar.Product))]
        public virtual ICollection<ProductAttributeVarchar> ProductAttributeVarchars { get; set; }

        [InverseProperty(nameof(ProductReview.Product))]
        public virtual ICollection<ProductReview> ProductReviews { get; set; }

        [InverseProperty(nameof(ProductAttribute.Product))]
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }

        [InverseProperty(nameof(ProductVariantValue.Product))]
        public virtual ICollection<ProductVariantValue> ProductVariantValues { get; set; }

        [InverseProperty(nameof(ProductTag.Product))]
        public virtual ICollection<ProductTag> ProductTags { get; set; }
    }
}
