using DreamyShop.Domain.Shared.Types;

namespace DreamyShop.Domain.Shared.Dtos
{
    public class ProductAttributeValueDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AttributeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public DateTime? DateTimeValue { get; set; }
        public decimal? DecimalValue { get; set; }
        public int? IntValue { get; set; }
        public string? TextValue { get; set; }
        public string? VarcharValue { get; set; }

        public int? DateTimeId { get; set; }
        public int? DecimalId { get; set; }
        public int? IntId { get; set; }
        public int? TextId { get; set; }
        public int? VarcharId { get; set; }
    }
}