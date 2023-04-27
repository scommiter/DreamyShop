using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("Carts")]
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool Status { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("Carts")]
        public virtual User User { get; set; }


        [InverseProperty(nameof(CartDetail.Cart))]
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
