using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
