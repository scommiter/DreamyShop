using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductAttributes")]
    public class ProductAttribute
    {
        public ProductAttribute(
            Guid id, 
            string code, 
            string label, 
            int sortOrder, 
            bool isVisibility, 
            bool isActive, 
            bool isUnique, 
            string note)
        {
            Id = id;
            Code = code;
            Label = label;
            SortOrder = sortOrder;
            IsVisibility = isVisibility;
            IsActive = isActive;
            IsUnique = isUnique;
            Note = note;
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(50)]
        public string Label { get; set; }
        public int SortOrder { get; set; }
        public bool IsVisibility { get; set; }
        public bool IsActive { get; set; }
        public bool IsUnique { get; set; }
        public string Note { get; set; }

        [InverseProperty("ProductAttributes")]
        public virtual Product Product { get; set; }

        [InverseProperty(nameof(ProductAttributeDateTime.ProductAttribute))]
        public virtual ICollection<ProductAttributeDateTime> ProductAttributeDateTimes { get; set; }

        [InverseProperty(nameof(ProductAttributeDecimal.ProductAttribute))]
        public virtual ICollection<ProductAttributeDecimal> ProductAttributeDecimals { get; set; }

        [InverseProperty(nameof(ProductAttributeInt.ProductAttribute))]
        public virtual ICollection<ProductAttributeInt> ProductAttributeInts { get; set; }

        [InverseProperty(nameof(ProductAttributeText.ProductAttribute))]
        public virtual ICollection<ProductAttributeText> ProductAttributeTexts { get; set; }

        [InverseProperty(nameof(ProductAttributeVarchar.ProductAttribute))]
        public virtual ICollection<ProductAttributeVarchar> ProductAttributeVarchars { get; set; }
    }
}