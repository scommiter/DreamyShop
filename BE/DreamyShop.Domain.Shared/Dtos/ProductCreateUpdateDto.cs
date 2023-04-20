using DreamyShop.Domain.Shared.Types;

namespace DreamyShop.Domain.Shared.Dtos
{
    public class ProductCreateUpdateDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ThumbnailPicture { get; set; }
        public ProductType ProductType { get; set; }
        public string CategoryName { get; set; }
        public string? ManufacturerName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisibility { get; set; }
        public Dictionary<string, List<string>> ProductOptions { get; set; }
        public List<ProductAttributeDisplayDto> VariantProducts { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
    public class Option
    {
        public List<string> OptionNames { get; set; }
    }
}