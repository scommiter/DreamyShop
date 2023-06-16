using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("Carts")]
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
        public User User { get; set; }

        [InverseProperty(nameof(CartDetail.Cart))]
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
