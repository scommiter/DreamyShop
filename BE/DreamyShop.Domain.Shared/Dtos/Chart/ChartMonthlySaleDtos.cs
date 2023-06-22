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

    public class ChartYearSaleDtos
    {
        public List<DataChartYear> DataChartPerMonthOfYear { get; set; }
    }
    public class TargetMonthDtos
    {
        public List<double> TargetOfMonths { get; set; }
    }

    public class DataChartYear
    {
        public double Target { get; set; }
        public double TotalPrice { get; set; }
    }

}
