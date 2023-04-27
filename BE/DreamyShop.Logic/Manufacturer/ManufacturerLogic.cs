using AutoMapper;
using AutoMapper.QueryableExtensions;
using DreamyShop.Common.Exceptions;
using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.RepositoryWrapper;

namespace DreamyShop.Logic.Manufacturer
{
    public class ManufacturerLogic : IManufacturerLogic
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ManufacturerLogic(
            IRepositoryWrapper repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<ApiResult<PageResult<ManufacturerDto>>> GetAllManufacturer(PagingRequest pagingRequest)
        {
            var manufacturerPagings = _repository.Manufacturer.GetAll().ToList();
            var pageResult = new PageResult<ManufacturerDto>()
            {
                Items = _mapper.Map<List<ManufacturerDto>>(manufacturerPagings),
                Totals = manufacturerPagings.Count()
            };
            return new ApiSuccessResult<PageResult<ManufacturerDto>>(pageResult);
        }
    }
}
