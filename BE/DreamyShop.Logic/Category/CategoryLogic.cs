using AutoMapper;
using AutoMapper.QueryableExtensions;
using DreamyShop.Common.Exceptions;
using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.RepositoryWrapper;
using Microsoft.EntityFrameworkCore;

namespace DreamyShop.Logic.Category
{
    public class CategoryLogic : ICategoryLogic
    {
        private readonly DreamyShopDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public CategoryLogic(
            DreamyShopDbContext context,
            IRepositoryWrapper repository,
            IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ApiResult<PageResult<CategoryDto>>> GetAllCategory(PagingRequest pagingRequest)
        {
            var categoryPagings = _context.Products
                                .OrderByDescending(p => p.DateCreated)
                                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                                .Skip((pagingRequest.Page - 1) * pagingRequest.Limit)
                                .Take(pagingRequest.Limit)
                                .ToList();
            var pageResult = new PageResult<CategoryDto>()
            {
                Items = categoryPagings,
                Totals = categoryPagings.Count()
            };
            return new ApiSuccessResult<PageResult<CategoryDto>>(pageResult);
        }

        public async Task<ApiResult<CategoryDto>> CreateCategory(CategoryCreateUpdateDto categoryCreateUpdateDto)
        {
            var newCategory = _mapper.Map<Domain.ProductCategory>(categoryCreateUpdateDto);
            await _repository.Category.AddAsync(newCategory);
            _repository.Save();
            return new ApiSuccessResult<CategoryDto>(_mapper.Map<CategoryDto>(newCategory));
        }

        public async Task<ApiResult<CategoryDto>> UpdateCategory(Guid id, CategoryCreateUpdateDto categoryCreateUpdateDto)
        {
            var category = await _repository.Product.GetByIdAsync(id);
            if (category == null)
            {
                return new ApiErrorResult<CategoryDto>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            _repository.Product.Update(_mapper.Map(categoryCreateUpdateDto, category));
            _repository.Save();
            return new ApiSuccessResult<CategoryDto>(_mapper.Map<CategoryDto>(category));
        }

        public async Task<ApiResult<bool>> RemoveCategory(Guid id)
        {
            var category = await _repository.Category.GetByIdAsync(id);
            if (category == null)
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            try
            {
                await _repository.Category.BeginTransactionAsync();
                _repository.Category.Remove(id);
                _repository.Save();
                await _repository.Category.EndTransactionAsync();
            }
            catch
            {
                await _repository.Category.RollbackTransactionAsync();
                return new ApiErrorResult<bool>((int)ErrorCodes.DeleteFailed);
            }
            return new ApiSuccessResult<bool>(true);
        }

    }
}
