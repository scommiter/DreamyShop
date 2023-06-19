namespace DreamyShop.Domain.Shared.Dtos.Chart
{
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
