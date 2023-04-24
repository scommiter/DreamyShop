using DreamyShop.Domain.Shared.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain.Shared.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ThumbnailPicture { get; set; }
        public ProductType ProductType { get; set; }
        public string CategoryName { get; set; }
        public string ManufacturerName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisibility { get; set; }
        public List<ProductAttributeDisplayDto>? ProductAttributeDisplayDtos { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }

    public class ProductAttributeDisplayDto
    {
        public List<string>? AttributeNames { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public List<string> Images { get; set; }
    }
}
