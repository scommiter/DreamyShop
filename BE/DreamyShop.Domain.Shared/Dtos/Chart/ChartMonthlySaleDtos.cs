namespace DreamyShop.Domain.Shared.Dtos.Chart
{
    public class ChartWeeklySaleDtos
    {
        public List<double> PercentOfSalesByDay { get; set; }
    }
    public class ChartMonthlySaleDtos
    {
        public List<double> PercentOfSalesCurrentMonth { get; set; }
        public List<double> PercentOfSalesLastMonth { get; set; }
    } 
}
