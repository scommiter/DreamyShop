using AutoMapper;
using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos.Chart;
using DreamyShop.Domain.Shared.Types;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.RepositoryWrapper;
using OfficeOpenXml.ConditionalFormatting;

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
        public async Task<ApiResult<StatisticDashboardDto>> GetStatisticDashboard()
        {
            DateTime date = DateTime.Now.AddDays(-7);
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
            }
            DateTime startDate = date;
            DateTime endDate = date.AddDays(7);
            var totalBillLastWeek = _repository.Bill.GetAll().Where(b => b.DateCreated < endDate && b.DateCreated >= startDate);
            var result = new StatisticDashboardDto
            {
                NumberCustomers = _repository.User.GetAll().Count(),
                NumberNewOrders = totalBillLastWeek.Count(),
                TotalPrices = _repository.Bill.GetAll().Select(b => b.TotalMoney).Sum()
            };
            return new ApiSuccessResult<StatisticDashboardDto>(result);
        }
        public async Task<ApiResult<PricePaymentTypeDto>> GetPricePaymentType()
        {
            var bills = _repository.Bill.GetAll().ToList();
            var totalPrices = bills.Select(p => p.TotalMoney).Sum();
            var groupPaymentTypes = bills.GroupBy(p => p.PaymentType).ToList();
            var pricePaymentType = new PricePaymentTypeDto
            {
                TotalPrices = totalPrices,
                Banking = groupPaymentTypes.Where(g => g.Key == PaymentType.BANK).Select(p => p.Select(t => t.TotalMoney).Sum()).FirstOrDefault(),
                Cash = groupPaymentTypes.Where(g => g.Key == PaymentType.CASH).Select(p => p.Select(t => t.TotalMoney).Sum()).FirstOrDefault(),
                VisaMasterCard = groupPaymentTypes.Where(g => g.Key == PaymentType.VISA || g.Key == PaymentType.MASTERCARD).Select(p => p.Select(t => t.TotalMoney).Sum()).Sum(),
            };
            return new ApiSuccessResult<PricePaymentTypeDto>(pricePaymentType);
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
            var test = startDate.Date;
            var totalBillLastWeek = _repository.Bill.GetAll().Where(b => b.DateCreated.Date <= endDate.Date && b.DateCreated.Date >= startDate.Date);
            var totalMoney = totalBillLastWeek.Select(t => t.TotalMoney).Sum();
            var totalBillPerDayOfWeek = totalBillLastWeek.GroupBy(t => t.DateCreated)
                                           .Select(g => new { 
                                               Day = g.Key, 
                                               PercentMoney = Math.Round(((g.Select(t => t.TotalMoney).Sum() / totalMoney) * 100), 2) 
                                           }).ToList();
            var chartWeeklySales = new ChartWeeklySaleDtos();
            var percentsOfDay = new List<double>();
            var indexBill = 0;
            for (int i = 0; i < 7; i++)
            {
                var dayInWeek = startDate.DayOfWeek;
                if(indexBill >= totalBillPerDayOfWeek.Count())
                {
                    percentsOfDay.Add(0);
                    startDate = startDate.AddDays(1);
                    continue;
                }
                if (dayInWeek != totalBillPerDayOfWeek[indexBill].Day.DayOfWeek)
                {
                    percentsOfDay.Add(0);
                    startDate = startDate.AddDays(1);
                    continue;
                }
                percentsOfDay.Add(totalBillPerDayOfWeek[indexBill].PercentMoney);
                indexBill++;
                startDate = startDate.AddDays(1);
            }
            chartWeeklySales.PercentOfSalesByDay = percentsOfDay;
            return new ApiSuccessResult<ChartWeeklySaleDtos>(chartWeeklySales);
        }
        public async Task<ApiResult<ChartCategoryDtos>> GetChartCategory()
        {
            throw new NotImplementedException();
        }

        //Get all sales by week of two month
        public async Task<ApiResult<ChartMonthlySaleDtos>> GetChartMonthlySale()
        {
            var chartMonthlySaleDtos = new ChartMonthlySaleDtos();
            var totalBills = _repository.Bill.GetAll().ToList();
            //LAST MONTH
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            var firstDayLastMonth = month.AddMonths(-1);
            var lastDayLastMonth = month.AddDays(-1);
            var dateLastMonths = Enumerable.Range(1, DateTime.DaysInMonth(firstDayLastMonth.Year, firstDayLastMonth.Month)).Select(n => new DateTime(firstDayLastMonth.Year, firstDayLastMonth.Month, n));
            var weekendLastMonths = from d in dateLastMonths
                           where d.DayOfWeek == DayOfWeek.Monday
                           select d;
            var totalPriceLastMonth = totalBills.Where(b => b.DateCreated <= lastDayLastMonth && b.DateCreated >= firstDayLastMonth).Select(p => p.TotalMoney).Sum();
            chartMonthlySaleDtos.PercentOfSalesLastMonth = CaculatePercentPricesWeeks(weekendLastMonths.ToList(), firstDayLastMonth, lastDayLastMonth, totalBills, totalPriceLastMonth);

            // CURRENT MONTH
            DateTime date = DateTime.Today;
            var firstDayCurrentMonth = lastDayLastMonth.AddDays(1);
            var lastDayCurrentMonth = month.AddMonths(1).AddDays(-1);
            var dateCurrentMonths = Enumerable.Range(1, DateTime.DaysInMonth(date.Year, date.Month)).Select(n => new DateTime(date.Year, date.Month, n));
            var weekendCurrentMonths = from d in dateCurrentMonths
                           where d.DayOfWeek == DayOfWeek.Monday
                           select d;
            var totalPriceCurrentMonth = totalBills.Where(b => b.DateCreated <= lastDayCurrentMonth && b.DateCreated >= firstDayCurrentMonth).Select(p => p.TotalMoney).Sum();
            chartMonthlySaleDtos.PercentOfSalesCurrentMonth = CaculatePercentPricesWeeks(weekendCurrentMonths.ToList(), firstDayCurrentMonth, lastDayCurrentMonth, totalBills, totalPriceCurrentMonth);

            return new ApiSuccessResult<ChartMonthlySaleDtos>(chartMonthlySaleDtos);
        }
        private List<double> CaculatePercentPricesWeeks(
            List<DateTime> weekendMonths, 
            DateTime startDate, 
            DateTime lastDate, 
            List<Domain.Bill> totalBills,
            double totalPriceOfMonth)
        {
            var result = new List<double>();
            var startD = startDate;
            var endD = new DateTime();
            var checkFirstDate = weekendMonths.ToList().First();
            var checkLastDate = weekendMonths.Last();
            foreach (var week in weekendMonths)
            {
                if(week.Day < 4)
                {
                    continue;
                }
                if(week == checkFirstDate && weekendMonths.Count == 5 && week.Day > 3)  
                {
                    continue;
                }
                if (week == checkLastDate && weekendMonths.Count == 5 && week.Day > checkLastDate.Day)
                {
                    continue;
                }
                if (week != checkLastDate)
                {
                    endD = week;
                }
                else
                {
                    endD = lastDate;
                }
                result.Add(Math.Round(((totalBills.Where(b => b.DateCreated <= endD && b.DateCreated >= startD).Select(p => p.TotalMoney).Sum()) / totalPriceOfMonth) * 100, 2));
                startD = week;
            }
            return result;  
        }

        public async Task<ApiResult<ChartYearSaleDtos>> GetChartInYearSale(TargetMonthDtos targetMonthDtos, bool isSetTarGet)
        {
            var result = new ChartYearSaleDtos();
            var targets = new TargetMonthDtos();
            targets.TargetOfMonths = new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            if (isSetTarGet == true)
            {
                targets = targetMonthDtos;
            }
            int year = DateTime.Now.Year;
            DateTime firstDayInCurrentYear = new DateTime(year, 1, 1);
            DateTime lastDayInCurrentYear = new DateTime(year, 12, 31);
            var montshBill = _repository.Bill.GetAll()
                    .Where(bill => bill.DateCreated >= firstDayInCurrentYear && bill.DateCreated <= lastDayInCurrentYear)
                    .ToList();
            var totalMoney = montshBill.Select(l => l.TotalMoney).Sum();
            var last12MonthBillGroup = montshBill.GroupBy(p => p.DateCreated.Month)
                                        .Select(g => new
                                        {
                                            Month = g.Key,
                                            TotalPrice = g.Select(t => t.TotalMoney).Sum()
                                        }).ToList();
            for (int i = 0; i < 12; i++)
            {
                result.DataChartPerMonthOfYear.Add(new DataChartYear
                {
                    Target = targets.TargetOfMonths[0],
                    TotalPrice = last12MonthBillGroup[i].TotalPrice
                });
            }
            return new ApiSuccessResult<ChartYearSaleDtos>(result);
        }
    }
}
