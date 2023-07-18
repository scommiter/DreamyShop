using Dreamy.Domain.Shared.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dreamy.Domain
{
    public class Product : AuditEntity
    {
        public Product() { }
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Slug { get; set; }
        public int? SortOrder { get; set; }
        public ProductType ProductType { get; set; }
        public int CategoryId { get; set; }
        public string? SeoMetaDescription { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisibility { get; set; }
    }
}
