using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
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


        //STATISTIC FOR CHAR
        Task<ApiResult<ChartWeeklySaleDtos>> GetChartWeeklySale();
        Task<ApiResult<ChartCategoryDtos>> GetChartCategory();
    }
}
