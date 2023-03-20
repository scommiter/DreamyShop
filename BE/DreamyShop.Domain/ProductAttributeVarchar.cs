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
    public class ProductAttributeVarchar
    {
        public ProductAttributeVarchar(Guid id, Guid attributeId, Guid productId, string value)
        {
            Id = id;
            AttributeId = attributeId;
            ProductId = productId;
            Value = value;
        }
        [Key]
        public Guid Id { get; set; }
        public Guid AttributeId { get; set; }
        public Guid ProductId { get; set; }
        public string Value { get; set; }

        [InverseProperty("ProductAttributeVarchars")]
        public virtual ProductAttribute ProductAttribute { get; set; }
    }
}
