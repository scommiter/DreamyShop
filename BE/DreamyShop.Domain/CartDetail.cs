using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("CartDetails")]
    public class CartDetail
    {
        [Key]
        public int Id { get; set; }
        public int CartId { get; set; }
        public int VariantId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey(nameof(CartId))]
        [InverseProperty("CartDetails")]
        public virtual Cart Cart { get; set; }

        [ForeignKey(nameof(VariantId))]
        [InverseProperty("CartDetails")]
        public virtual ProductVariant ProductVariant { get; set; }
    }
}
