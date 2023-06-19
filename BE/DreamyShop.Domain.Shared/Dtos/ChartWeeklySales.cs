namespace DreamyShop.Domain.Shared.Dtos
{
    public class StatisticDashboardDto
    {
        public int NumberNewOrders { get; set; }
        public int NumberCustomers { get; set; }
        public double TotalPrices { get; set; }
    }
    public class PricePaymentTypeDto
    {
        public double TotalPrices { get; set; }
        public double Cash { get; set; }
        public double Banking { get; set; }
        public double Debt { get; set; }
    }

    public class ChartWeeklySaleDtos
    {
        public List<double> PercentOfSalesByDay { get; set; }
    }

    public class ChartMonthlySaleDtos
    {
        public List<double> PercentOfSalesCurrentMonth { get; set; }
        public List<double> PercentOfSalesLastMonth { get; set; }
    }

    public class ChartCategoryDtos
    {
        public List<PercentCategory> PercentOfCategories { get; set; }
    }
    public class PercentCategory
    {
        public string CategoryName { get; set; }
        public double Percent { get; set; }
    }
}
