using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain
{
    [Table("Tags")]
    public class Tag : TrackEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}