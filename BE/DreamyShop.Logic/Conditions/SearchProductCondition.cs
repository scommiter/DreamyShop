using DreamyShop.Domain.Shared.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Logic.Conditions
{
    public class SearchProductCondition
    {
        public string? ProductName { get; set; }
        public string? Code { get; set; }
        public ProductType? ProductType { get; set; }
        public string? CategoryName { get; set; }
        public string? ManufacturerName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsVisibility { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
