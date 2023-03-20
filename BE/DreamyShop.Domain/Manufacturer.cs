using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("Manufacturers")]
    public class Manufacturer
    {
        public Manufacturer(
            Guid id,
            string name,
            string code,
            string slug,
            string coverPicture,
            bool visibility,
            bool isActive,
            string country)
        {
            Id = id;
            Name = name;
            Code = code;
            Slug = slug;
            CoverPicture = coverPicture;
            Visibility = visibility;
            IsActive = isActive;
            Country = country;
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        public string Slug { get; set; }
        public string CoverPicture { get; set; }
        public bool Visibility { get; set; }
        public bool IsActive { get; set; }
        public string Country { get; set; }

        [InverseProperty(nameof(Product.Manufacturer))]
        public virtual ICollection<Product> Products { get; set; }
    }
}