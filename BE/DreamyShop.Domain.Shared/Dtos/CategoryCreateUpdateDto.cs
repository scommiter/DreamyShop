namespace DreamyShop.Domain.Shared.Dtos
{
    public class CategoryCreateUpdateDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public int SortOrder { get; set; }
        public string CoverPicture { get; set; }
        public bool IsVisibility { get; set; }
        public bool IsActive { get; set; }
        public int? ParentId { get; set; }
        public string? SeoMetaDescription { get; set; }
    }
}
