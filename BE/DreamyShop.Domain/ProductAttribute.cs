using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain
{
    [Table("ProductAttributes")]
    public class ProductAttribute : AuditEntity
    {
        [Key]
        [Column(Order = 1)]
        public Guid ProductId { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid AttributeId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductAttributes")]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty("ProductAttributes")]
        public virtual Attribute Attribute { get; set; }
    }
}
