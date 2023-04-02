using DreamyShop.Domain.Shared.Types;

namespace DreamyShop.Domain.Shared.Dtos
{
    public class ProductCreateUpdateDto
    {
        public Guid ManufacturerId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public ProductType ProductType { get; set; }
        public int SortOrder { get; set; }
        public bool IsVisibility { get; set; }
        public bool IsActive { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
        public string SeoMetaDescription { get; set; }
        public string Description { get; set; }
        public string ThumbnailPicture { get; set; }
    }
}