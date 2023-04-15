using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain.Shared.Dtos
{
    public class ProductVariantDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string SKU { get; set; }
        public bool IsVisibility { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public string ThumbnailPicture { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
