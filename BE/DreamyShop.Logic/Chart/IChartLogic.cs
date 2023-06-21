using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Logic.Chart
{
    public interface IChartLogic
    {
        //STATISTIC NUMBER
        Task<ApiResult<StatisticDashboardDto>> GetStatisticDashboard();
        Task<ApiResult<PricePaymentTypeDto>> GetPricePaymentType();


        //STATISTIC FOR CHAR
        Task<ApiResult<ChartWeeklySaleDtos>> GetChartWeeklySale();
        Task<ApiResult<ChartCategoryDtos>> GetChartCategory();

        Task<ApiResult<ChartMonthlySaleDtos>> GetChartMonthlySale();
        Task<ApiResult<ChartYearSaleDtos>> GetChartInYearSale();

    }
}
