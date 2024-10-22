﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductAttributeValues")]
    public class ProductAttributeValue : TrackEntity
    {
        public ProductAttributeValue() { }

        [Key]
        public int Id { get; set; }

        public int AttributeId { get; set; }
        public int ProductId { get; set; }
        public string Value { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty("ProductAttributeValues")]
        public virtual Attribute Attribute { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductAttributeValues")]
        public virtual Product Product { get; set; }

        [InverseProperty(nameof(ProductVariantValue.ProductAttributeValues))]
        public virtual ICollection<ProductVariantValue> ProductVariantValues { get; set; }
    }
}