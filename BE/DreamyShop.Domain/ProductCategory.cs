using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductCategories")]
    public class ProductCategory
    {
        public ProductCategory(
            Guid id,
            string name,
            string code,
            string slug,
            int sortOrder,
            string coverPicture,
            bool visibility,
            bool isActive,
            Guid? parentId,
            string seoMetaDescription)
        {
            Id = id;
            Name = name;
            Code = code;
            Slug = slug;
            SortOrder = sortOrder;
            CoverPicture = coverPicture;
            Visibility = visibility;
            IsActive = isActive;
            ParentId = parentId;
            SeoMetaDescription = seoMetaDescription;
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
        public int SortOrder { get; set; }
        public string CoverPicture { get; set; }
        public bool Visibility { get; set; }
        public bool IsActive { get; set; }
        public Guid? ParentId { get; set; }
        [StringLength(250)]
        public string SeoMetaDescription { get; set; }

        [InverseProperty(nameof(Product.ProductCategory))]
        public virtual ICollection<Product> Products { get; set; }
    }
}