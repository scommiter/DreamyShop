using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductTags")]
    public class ProductTag : TrackEntity
    {
        [Key]
        [Column(Order = 1)]
        public Guid ProductId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string TagId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductTags")]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(TagId))]
        [InverseProperty("ProductTags")]
        public virtual Tag Tag { get; set; }
    }
}