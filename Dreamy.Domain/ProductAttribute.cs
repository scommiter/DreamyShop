using Dreamy.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain
{
    [Table("ProductAttributes")]
    public class ProductAttribute : AuditEntity
    {
        public int ProductId { get; set; }
        public int AttributeId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Attribute Attribute { get; set; }
    }
}
