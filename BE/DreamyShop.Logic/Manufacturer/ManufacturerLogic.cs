using AutoMapper;
using AutoMapper.QueryableExtensions;
using DreamyShop.Common.Exceptions;
using DreamyShop.Common.Results;
using DreamyShop.Domain;
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

        public async Task<ApiResult<ManufacturerDto>> CreateManufacturer(ManufacturerCreateUpdateDto manufacturerCreateUpdateDto)
        {
            var newManufacturer = _mapper.Map<Domain.Manufacturer>(manufacturerCreateUpdateDto);
            await _repository.Manufacturer.AddAsync(newManufacturer);
            return new ApiSuccessResult<ManufacturerDto>(_mapper.Map<ManufacturerDto>(newManufacturer));
        }

        public async Task<ApiResult<PageResult<ManufacturerDto>>> GetAllManufacturer(PagingRequest pagingRequest)
        {
            var manufacturerPagings = await _repository.Manufacturer.GetAll();
            var pageResult = new PageResult<ManufacturerDto>()
            {
                Items = _mapper.Map<List<ManufacturerDto>>(manufacturerPagings.ToList()),
                Totals = manufacturerPagings.Count()
            };
            return new ApiSuccessResult<PageResult<ManufacturerDto>>(pageResult);
        }

        public async Task<ApiResult<bool>> RemoveManufacturer(int id)
        {
            var manufacturer = await _repository.Manufacturer.GetByIdAsync(id);
            if (manufacturer == null)
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            await _repository.Manufacturer.Remove(id);
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<ManufacturerDto>> UpdateManufacturer(int id, ManufacturerCreateUpdateDto manufacturerCreateUpdateDto)
        {
            var manufacturer = await _repository.Manufacturer.GetByIdAsync(id);
            if (manufacturer == null)
                return new ApiErrorResult<ManufacturerDto>((int)ErrorCodes.DataEntryIsNotExisted);
            var newManufacturer = _mapper.Map(manufacturerCreateUpdateDto, manufacturer);
            await _repository.Manufacturer.UpdateAsync(newManufacturer, id);
            return new ApiSuccessResult<ManufacturerDto>(_mapper.Map<ManufacturerDto>(manufacturer));
        }
    }
}
