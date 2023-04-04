using AutoMapper;
using AutoMapper.QueryableExtensions;
using DreamyShop.Common.Exceptions;
using DreamyShop.Common.Results;
using DreamyShop.Domain;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.RepositoryWrapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace DreamyShop.Logic.Manufacturer
{
    public class ManufacturerLogic : IManufacturerLogic
    {
        private readonly DreamyShopDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ManufacturerLogic(
            DreamyShopDbContext context,
            IRepositoryWrapper repository,
            IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<ManufacturerDto>> CreateManufacturer(ManufacturerCreateUpdateDto manufacturerCreateUpdateDto)
        {
            var newManufacturer = _mapper.Map<Domain.Manufacturer>(manufacturerCreateUpdateDto);
            await _repository.Manufacturer.AddAsync(newManufacturer);
            _repository.Save();
            return new ApiSuccessResult<ManufacturerDto>(_mapper.Map<ManufacturerDto>(newManufacturer));
        }

        public async Task<ApiResult<PageResult<ManufacturerDto>>> GetAllManufacturer(int page, int limit)
        {
            var manufacturerPagings = _repository.Manufacturer.GetAll()
                                .ProjectTo<ManufacturerDto>(_mapper.ConfigurationProvider)
                                .Skip((page - 1) * limit)
                                .Take(limit)
                                .ToList();
            var pageResult = new PageResult<ManufacturerDto>()
            {
                Items = manufacturerPagings ?? new List<ManufacturerDto>(),
                Totals = manufacturerPagings.Count()
            };
            return new ApiSuccessResult<PageResult<ManufacturerDto>>(pageResult);
        }

        public async Task<ApiResult<bool>> RemoveManufacturer(Guid id)
        {
            var manufacturer = await _repository.Manufacturer.GetByIdAsync(id);
            if (manufacturer == null)
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            _repository.Manufacturer.Remove(id);
            _repository.Save();
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<ManufacturerDto>> UpdateManufacturer(Guid id, ManufacturerCreateUpdateDto manufacturerCreateUpdateDto)
        {
            var manufacturer = await _repository.Manufacturer.GetByIdAsync(id);
            if (manufacturer == null)
                return new ApiErrorResult<ManufacturerDto>((int)ErrorCodes.DataEntryIsNotExisted);
            var newManufacturer = _mapper.Map(manufacturerCreateUpdateDto, manufacturer);
            _repository.Manufacturer.Update(newManufacturer);
            _repository.Save();
            return new ApiSuccessResult<ManufacturerDto>(_mapper.Map<ManufacturerDto>(manufacturer));
        }
    }
}
