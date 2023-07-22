using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain
{
    [Table("CartDetails")]
    public class CartDetail
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int VariantId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
