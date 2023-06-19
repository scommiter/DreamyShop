namespace DreamyShop.Domain.Shared.Dtos
{
    public class StatisticDashboardDto
    {
        public int NumberNewOrders { get; set; }
        public int NumberCustomers { get; set; }
        public int TotalPrices { get; set; }
    }

    public class ChartWeeklySaleDtos
    {
        public List<double> PercentOfSalesByDay { get; set; }
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
