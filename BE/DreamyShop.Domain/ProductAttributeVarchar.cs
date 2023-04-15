using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain
{
    [Table("ProductAttributeVarchars")]
    public class ProductAttributeVarchar : TrackEntity
    {
        public ProductAttributeVarchar() { }
        public ProductAttributeVarchar(
            Guid id,
            Guid attributeId,
            Guid productId,
            string value,
            Attribute attribute,
            Product product)
        {
            Id = id;
            AttributeId = attributeId;
            ProductId = productId;
            Value = value;
            Attribute = attribute;
            Product = product;
        }
        [Key]
        public Guid Id { get; set; }
        public Guid AttributeId { get; set; }
        public Guid ProductId { get; set; }
        public string Value { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty("ProductAttributeVarchars")]
        public virtual Attribute Attribute { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductAttributeVarchars")]
        public virtual Product Product { get; set; }

        [InverseProperty(nameof(ProductVariantValueVarchar.ProductAttributeVarchar))]
        public virtual ICollection<ProductVariantValueVarchar> ProductVariantValueVarchars { get; set; }
    }
}
