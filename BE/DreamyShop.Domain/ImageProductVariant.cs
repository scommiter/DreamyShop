using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain
{
    [Table("ImageProductVariants")]
    public class ImageProductVariant : TrackEntity
    {
        [Key]
        public int Id { get; set; }
        public int ProductVariantId { get; set; }
        public string Path { get; set; }

        [ForeignKey(nameof(ProductVariantId))]
        [InverseProperty("ImageProductVariants")]
        public virtual ProductVariant ProductVariant { get; set; }
    }
}
