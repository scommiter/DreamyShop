using DreamyShop.Domain.Shared.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain.Shared.Dtos
{
    public class ProductAttributeDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public AttributeType DataType { get; set; }
        public int SortOrder { get; set; }
        public bool IsVisibility { get; set; }
        public bool IsActive { get; set; }
        public bool IsUnique { get; set; }
        public string Note { get; set; }
    }
}
