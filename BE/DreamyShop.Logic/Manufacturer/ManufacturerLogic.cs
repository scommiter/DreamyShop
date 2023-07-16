using AutoMapper;
using AutoMapper.QueryableExtensions;
using DreamyShop.Common.Constants;
using DreamyShop.Common.Exceptions;
using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Domain.Shared.Dtos.Manufacturer;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.Helpers;
using DreamyShop.Repository.RepositoryWrapper;
using Microsoft.Extensions.Caching.Memory;

namespace DreamyShop.Logic.Manufacturer
{
    public class ManufacturerLogic : IManufacturerLogic
    {
        private readonly DreamyShopDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public ManufacturerLogic(
            DreamyShopDbContext context,
            IRepositoryWrapper repository,
            IMapper mapper,
            IMemoryCache memoryCache)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<ApiResult<ManufacturerDto>> CreateManufacturer(ManufacturerCreateUpdateDto manufacturerCreateUpdateDto)
        {
            var newManufacturer = _mapper.Map<Domain.Manufacturer>(manufacturerCreateUpdateDto);
            await _repository.Manufacturer.AddAsync(newManufacturer);
            _repository.Save();
            return new ApiSuccessResult<ManufacturerDto>(_mapper.Map<ManufacturerDto>(newManufacturer));
        }

        public async Task<ApiResult<PageResult<ManufacturerDto>>> GetAllManufacturer(PagingRequest pagingRequest)
        {
            //var _manufacturerCache = new CacheHelper<IQueryable<Domain.Manufacturer>>(cache);
            //var manufacturers = await _manufacturerCache.GetOrCreate(ConstantCaches.MANUFACTURERCACHES, async () => _repository.Manufacturer.GetAll());

            var manufacturerPagings = _repository.Manufacturer.GetAll()
                                .ProjectTo<ManufacturerDto>(_mapper.ConfigurationProvider)
                                .Skip((pagingRequest.Page - 1) * pagingRequest.Limit)
                                .Take(pagingRequest.Limit)
                                .ToList();

            var pageResult = new PageResult<ManufacturerDto>()
            {
                Items = manufacturerPagings ?? new List<ManufacturerDto>(),
                Totals = manufacturerPagings.Count()
            };
            return new ApiSuccessResult<PageResult<ManufacturerDto>>(pageResult);
        }

        public async Task<ApiResult<bool>> RemoveManufacturer(int id)
        {
            var manufacturer = await _repository.Manufacturer.GetByIdAsync(id);
            if (manufacturer == null)
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            _repository.Manufacturer.Remove(id);
            _repository.Save();
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<ManufacturerDto>> UpdateManufacturer(int id, ManufacturerCreateUpdateDto manufacturerCreateUpdateDto)
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
