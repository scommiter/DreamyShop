using AutoMapper;
using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos.Chart;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.RepositoryWrapper;

namespace DreamyShop.Logic.Chart
{
    public class ChartLogic : IChartLogic
    {
        private readonly DreamyShopDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ChartLogic(
            DreamyShopDbContext context,
            IRepositoryWrapper repository,
            IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        //Get number for dashborad
        public Task<ApiResult<StatisticDashboardDto>> GetStatisticDashboard()
        {
            throw new NotImplementedException();
        }
        public Task<ApiResult<PricePaymentTypeDto>> GetPricePaymentType()
        {
            throw new NotImplementedException();
        }

        //Get all sales by day of the last week
        public async Task<ApiResult<ChartWeeklySaleDtos>> GetChartWeeklySale()
        {
            DateTime date = DateTime.Now.AddDays(-7);
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
            }
            DateTime startDate = date;
            DateTime endDate = date.AddDays(7);
            var totalBillLastWeek = _repository.Bill.GetAll().Where(b => b.DateCreated <= endDate && b.DateCreated >= startDate);
            var totalBillPerDayOfWeek = totalBillLastWeek.GroupBy(t => t.DateCreated)
                                           .Select(g => new { DayOfWeek = g.Key, TotalMoney = g.Select(t => t.TotalMoney).Sum() }).ToList();
            var chartWeeklySales = new ChartWeeklySaleDtos();
            totalBillPerDayOfWeek.ForEach(t => chartWeeklySales.PercentOfSalesByDay.Add(t.TotalMoney));
            return new ApiSuccessResult<ChartWeeklySaleDtos>(chartWeeklySales);
        }
        public async Task<ApiResult<ChartCategoryDtos>> GetChartCategory()
        {
            throw new NotImplementedException();
        }

        //Get all sales by week of two month
        public Task<ApiResult<ChartMonthlySaleDtos>> GetChartMonthlySale()
        {
            throw new NotImplementedException();
        }
    }
}
