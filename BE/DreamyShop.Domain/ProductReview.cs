using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductReviews")]
    public class ProductReview : TrackEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public Guid? ParentId { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(250)]
        public string Content { get; set; }

        public Guid OrderId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductReviews")]
        public virtual Product Product { get; set; }
    }
}
