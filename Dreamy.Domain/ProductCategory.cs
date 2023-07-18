using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain
{
    public class ProductCategory : AuditEntity
    {
        public ProductCategory() { }
        public ProductCategory(
            int id,
            string name,
            string code,
            string slug,
            int sortOrder,
            string coverPicture,
            bool isVisibility,
            bool isActive,
            int? parentId,
            string seoMetaDescription)
        {
            Id = id;
            Name = name;
            Code = code;
            Slug = slug;
            SortOrder = sortOrder;
            CoverPicture = coverPicture;
            IsVisibility = isVisibility;
            IsActive = isActive;
            ParentId = parentId;
            SeoMetaDescription = seoMetaDescription;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public int SortOrder { get; set; }
        public string CoverPicture { get; set; }
        public bool IsVisibility { get; set; }
        public bool IsActive { get; set; }
        public int? ParentId { get; set; }
        public string SeoMetaDescription { get; set; }
    }
}