using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain
{
    [Table("ImageProducts")]
    public class ImageProduct : TrackEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Path { get; set; }
    }
}
