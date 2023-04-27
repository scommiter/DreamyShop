using DreamyShop.Domain.Shared.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("Attributes")]
    public class Attribute : AuditEntity
    {
        public Attribute() { }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public bool IsVisibility { get; set; }
        public bool IsActive { get; set; }
        public bool IsUnique { get; set; }
        public string Note { get; set; }

        [InverseProperty(nameof(ProductAttribute.Attribute))]
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }

        [InverseProperty(nameof(ProductAttributeValue.Attribute))]
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }

        [InverseProperty(nameof(ProductVariantValue.Attribute))]
        public virtual ICollection<ProductVariantValue> ProductVariantValues { get; set; }
    }
}