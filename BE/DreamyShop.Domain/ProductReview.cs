using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductReviews")]
    public class ProductReview
    {
        public ProductReview(
            Guid id,
            Guid productId,
            Guid? parentId,
            string title,
            double rating,
            DateTime? createdAt,
            string content,
            Guid orderId)
        {
            Id = id;
            ProductId = productId;
            ParentId = parentId;
            Title = title;
            Rating = rating;
            CreatedAt = createdAt;
            Content = content;
            OrderId = orderId;
        }

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