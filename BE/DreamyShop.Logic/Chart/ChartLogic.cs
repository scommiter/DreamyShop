using AutoMapper;
using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.RepositoryWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }
    }
}
