using DreamyShop.Domain.Shared.Types;
using System.ComponentModel.DataAnnotations;

namespace DreamyShop.Domain.Shared.Dtos
{
    public class BillDto
    {
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public List<CartItemsDto> ItemCarts { get; set; }
        public double ShippingFee { get; set; }
        public double Discount { get; set; }
        public double TotalMoney { get; set; }
        public PaymentType PaymentType { get; set; }  
        public string ZipCode { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class BillCreateDto
    {
        public int UserId { get; set; }
        public string Phone { get; set; }
        public string? Note { get; set; }
        public string Address { get; set; }
        public string? ZipCode { get; set; }
        public double TotalMoney { get; set; }
        public double ShippingFee { get; set; }
        public int Discount { get; set; }
        public PaymentType PaymentType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public List<BillItem> ItemCarts { get; set; }
    }

    public class BillItem
    {
        public int ProductVariantId { get; set; }
        public string ProductVariantSKU { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
