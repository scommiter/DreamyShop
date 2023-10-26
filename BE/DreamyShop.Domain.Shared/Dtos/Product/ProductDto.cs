using DreamyShop.Domain.Shared.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain.Shared.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<string> ThumbnailPictures { get; set; }
        public ProductType ProductType { get; set; }
        public string CategoryName { get; set; }
        public string ManufacturerName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisibility { get; set; }
        public List<string>? OptionNames { get; set; }
        public List<ProductAttributeDisplayDto>? ProductAttributeDisplayDtos { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }

    public class ProductProcDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string ManufacturerName { get; set; }
        public string CategoryName { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public int ProductVariantId { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int ProductAttributeValueId { get; set; }
        public string Value { get; set; }
    }

    public class ProductAttributeDisplayDto
    {
        public List<string>? AttributeNames { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
    }

    public class ProductDisplayDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ThumbnailPictures { get; set; }
        public string RangePrice { get; set; }
        public int Quantity { get; set; }
        public int Star { get; set; }
    }

    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<string> ThumbnailPictures { get; set; }
        public ProductType ProductType { get; set; }
        public string CategoryName { get; set; }
        public string ManufacturerName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisibility { get; set; }
        public Dictionary<string, List<string>>? Options { get; set; }
        public List<ProductAttributeDisplayDto>? ProductAttributeDisplayDtos { get; set; }
    }
}
