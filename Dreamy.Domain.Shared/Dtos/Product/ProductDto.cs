using Dreamy.Domain.Shared.Types;

namespace Dreamy.Domain.Shared.Dtos.Product
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ThumbnailPicture { get; set; }
        public string Description { get; set; }
        public string RangPrice { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductExecuteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ThumbnailPicture { get; set; }
        public string CategoryName { get; set; }
        public string ManufacturerName { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string AttributeValue { get; set; }
        public string Description { get; set; }
    }
}
