using AutoMapper;
using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.RepositoryWrapper;

namespace DreamyShop.Logic.Cart
{
    public class CartLogic : ICartLogic
    {
        private readonly DreamyShopDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public CartLogic(
            DreamyShopDbContext context,
            IRepositoryWrapper repository,
            IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<PageResult<CartItemsDto>>> GetAllCart(PagingRequest pagingRequest)
        {
            throw new NotImplementedException();
        }
    }
}
