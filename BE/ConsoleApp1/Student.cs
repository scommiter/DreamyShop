using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ConsoleApp1
{
    public class Student
    {
        public int Stu_ID { get; set; }
        public string Stu_Name { get; set; }
        public int Age { get; set; }
        public string Subject { get; set; }
    }

    public class Grade
    {
        public int Grade_ID { get; set; }
        public string Grade_Name { get; set; }
    }

    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Quantity { get; set; }
        public int Price { get; set; }
        public string AttributeValue { get; set; }
    }

    public class ProductVariant
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public List<ProductVariant> GetProductVariants()
        {
            return new List<ProductVariant>()
            {
                new ProductVariant(){Id = 1, ProductId = 2, Quantity = 100, Price = 1500},  //Red - Sắt - 35
                new ProductVariant(){Id = 2, ProductId = 2, Quantity = 200, Price = 1500},  //Black - Gỗ - 37
            };
        }
    }

    public class ProductVariantValueText
    {
        public int ProductVariantId { get; set; }
        public int ProductId { get; set; }
        public int AttributeId { get; set; }
        public int ProductAttributeTextId { get; set; }

        public List<ProductVariantValueText> GetProductVariantValueTexts()
        {
            return new List<ProductVariantValueText>()
            {
                // Red - Sắt
                new ProductVariantValueText(){ProductVariantId = 1, ProductId = 2, AttributeId = 1, ProductAttributeTextId = 1},
                new ProductVariantValueText(){ProductVariantId = 1, ProductId = 2, AttributeId = 3, ProductAttributeTextId = 4},
                 // Black - Gỗ
                new ProductVariantValueText(){ProductVariantId = 2, ProductId = 2, AttributeId = 1, ProductAttributeTextId = 2},
                new ProductVariantValueText(){ProductVariantId = 2, ProductId = 2, AttributeId = 3, ProductAttributeTextId = 3},
            };
        }
    }
    public class ProductVariantValueInt
    {
        public int ProductVariantId { get; set; }
        public int ProductId { get; set; }
        public int AttributeId { get; set; }
        public int ProductAttributeIntId { get; set; }

        public List<ProductVariantValueInt> GetProductVariantValueInts()
        {
            return new List<ProductVariantValueInt>()
            {
                // Red - Sắt - 35
                new ProductVariantValueInt(){ProductVariantId = 1, ProductId = 2, AttributeId = 1, ProductAttributeIntId = 1},
                 // Black - Gỗ - 37
                new ProductVariantValueInt(){ProductVariantId = 2, ProductId = 2, AttributeId = 1, ProductAttributeIntId = 2},
            };
        }
    }

    public class ProductAttributeText
    {
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public int ProductId { get; set; }
        public string Value { get; set; }

        public List<ProductAttributeText> GetProductAttributeTexts()
        {
            return new List<ProductAttributeText>()
            {
                new ProductAttributeText(){Id = 1, AttributeId = 1, ProductId = 2, Value = "Red"},
                new ProductAttributeText(){Id = 2, AttributeId = 1, ProductId = 2, Value = "Black"},
                new ProductAttributeText(){Id = 3, AttributeId = 3, ProductId = 2, Value = "Gỗ"},
                new ProductAttributeText(){Id = 4, AttributeId = 3, ProductId = 2, Value = "Sắt"},
            };
        }
    }

    public class ProductAttributeInt
    {
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public int ProductId { get; set; }
        public int Value { get; set; }

        public List<ProductAttributeInt> GetProductAttributeInts()
        {
            return new List<ProductAttributeInt>()
            {
                new ProductAttributeInt(){Id = 1, AttributeId = 2, ProductId = 2, Value = 35},
                new ProductAttributeInt(){Id = 2, AttributeId = 2, ProductId = 2, Value = 37},
            };
        }
    }
}
