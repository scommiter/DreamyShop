namespace DreamyShop.Domain.Shared.Dtos.Chart
{
    public class PricePaymentTypeDto
    {
        public double TotalPrices { get; set; }
        public double Cash { get; set; }
        public double Banking { get; set; }
        public double Debt { get; set; }
    }
}
