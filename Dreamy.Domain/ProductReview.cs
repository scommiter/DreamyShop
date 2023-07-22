using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain
{
    [Table("ProductReviews")]
    public class ProductReview : TrackEntity
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string Content { get; set; }

        public int OrderId { get; set; }

    }
}
