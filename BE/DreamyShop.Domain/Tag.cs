using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("Tags")]
    public class Tag : TrackEntity
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty(nameof(ProductTag.Tag))]
        public virtual ICollection<ProductTag> ProductTags { get; set; }
    }
}