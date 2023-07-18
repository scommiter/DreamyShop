using Dreamy.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain
{
    public class ProductAttributeValue : TrackEntity
    {
        public ProductAttributeValue() { }

        public int Id { get; set; }

        public int AttributeId { get; set; }
        public int ProductId { get; set; }
        public string Value { get; set; }
    }
}