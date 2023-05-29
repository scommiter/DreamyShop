using DreamyShop.Domain.Shared.Types;
using Microsoft.AspNetCore.Http;

namespace DreamyShop.Domain.Shared.Dtos
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ProductType ProductType { get; set; }
        public string CategoryName { get; set; }
        public string? ManufacturerName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisibility { get; set; }
        //public List<string> Images { get; set; }
        public Dictionary<string, List<string>> ProductOptions { get; set; }
        public List<VariantProduct> VariantProducts { get; set; }
    }

    public class ProductUpdateDto
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        //public List<string>? ThumbnailPicture { get; set; }
        public ProductType? ProductType { get; set; }
        public string? CategoryName { get; set; }
        public string? ManufacturerName { get; set; }
        public string? Description { get; set; }
        public string? SeoMetaDescription { get; set; }
        public int? SortOrder { get; set; }
        public string? Slug { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsVisibility { get; set; }
        public Dictionary<string, List<string>>? ProductOptions { get; set; }
        public List<VariantProduct>? VariantProducts { get; set; }
    }

    public class Option
    {
        public List<string> OptionNames { get; set; }
    }
    public class VariantProduct
    {
        public List<string>? AttributeNames { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public IFormFile ThumbnailPicture { get; set; }
    }
}