using DreamyShop.Domain.Shared.Types;

namespace DreamyShop.Domain.Shared.Dtos
{
    public class BillDto
    {
        public string UserName { get; set; }
        public List<CartItemsDto> ItemCarts { get; set; }
        public double ShippingFee { get; set; }
        public double Discount { get; set; }
        public double TotalMoney { get; set; }
        public PaymentType PaymentType { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ZipCode { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
