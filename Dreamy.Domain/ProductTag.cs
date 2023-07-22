using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain
{
    [Table("ProductTags")]
    public class ProductTag : TrackEntity
    {
        public int ProductId { get; set; }
        public string TagId { get; set; }
    }
}