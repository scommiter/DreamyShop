using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain
{
    [Table("ImageProducts")]
    public class ImageProduct : TrackEntity
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Path { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ImageProducts")]
        public virtual Product Product { get; set; }
    }
}
