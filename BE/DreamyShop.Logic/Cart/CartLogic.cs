using AutoMapper;
using DreamyShop.Common.Exceptions;
using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Domain.Shared.Dtos.Cart;
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

        public async Task<ApiResult<PageResult<CartItemsDto>>> GetAllCart(int userId, PagingRequest pagingRequest)
        {
            var cart = _repository.Cart.GetAll().Where(p => p.UserId == userId).ToList().FirstOrDefault();
            if(cart == null)
            {
                return new ApiErrorResult<PageResult<CartItemsDto>>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var cartDetails = _repository.CartDetail.GetAll().Where(p => p.CartId == cart.Id);
            var cartDtos = from cD in cartDetails
                           join pV in _context.ProductVariants on cD.VariantId equals pV.Id into CDPV
                           from pV in CDPV.DefaultIfEmpty()
                           join p in _context.Products on pV.ProductId equals p.Id into PVP
                           from p in PVP.DefaultIfEmpty()
                           select new { cD, pV, p };
            var cartItemsDto =  cartDtos
                                .OrderByDescending(c => c.cD.DateCreated)
                                .Skip((pagingRequest.Page - 1) * pagingRequest.Limit)
                                .Take(pagingRequest.Limit)
                                .Select(c => new CartItemsDto
                                {
                                   Price = c.pV.Price,
                                   ProductName = c.p.Name,
                                   ProductSKU = c.pV.SKU,
                                   Quantity = c.cD.Quantity,
                                   Tax = 0
                                }).ToList();

            var pageResult = new PageResult<CartItemsDto>()
            {
                Items = cartItemsDto,
                Totals = cartDtos.Count()
            };
            return new ApiSuccessResult<PageResult<CartItemsDto>>(pageResult);
        }
        public async Task<ApiResult<bool>> AddToCart(CartAddDto cartAddDto)
        {
            var productVariant = _repository.ProductVariant.GetAll().Where(c => c.SKU == cartAddDto.Sku).FirstOrDefault();
            if (productVariant == null)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var cart = _repository.Cart.GetAll().Where(p => p.UserId == cartAddDto.UserId).ToList().FirstOrDefault();
            if (cart != null)
            {
                var cartUpdate = _repository.CartDetail.GetAll().Where(p => p.CartId == cart.Id && p.VariantId == productVariant.Id).ToList().FirstOrDefault();
                if (cartUpdate != null)
                {
                    cartUpdate.Quantity += cartAddDto.Quantity;
                    _repository.CartDetail.Update(cartUpdate);
                    _repository.Save();
                    return new ApiSuccessResult<bool>(true);
                }

                await _repository.CartDetail.AddAsync(new Domain.CartDetail
                {
                    CartId = cart.Id,
                    VariantId = productVariant.Id,
                    Quantity = cartAddDto.Quantity,
                    DateCreated = DateTime.Now
                });
                _repository.Save();
            }
            else
            {
                var newCart = new Domain.Cart
                {
                    UserId = cartAddDto.UserId,
                    Status = true
                };
                await _repository.Cart.AddAsync(newCart);
                _repository.Save();
                await _repository.CartDetail.AddAsync(new Domain.CartDetail
                {
                    CartId = newCart.Id,
                    VariantId = productVariant.Id,
                    Quantity = cartAddDto.Quantity,
                    DateCreated = DateTime.Now
                });
                _repository.Save();
            }
            
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> RemoveFromCart(int userId, int cartDetailId)
        {
            var cart = _repository.Cart.GetAll().Where(p => p.UserId == userId).ToList().FirstOrDefault();
            if (cart == null)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var checkCartDetail = await _repository.CartDetail.GetByIdAsync(cartDetailId);
            if(checkCartDetail == null || checkCartDetail?.CartId != cart.Id)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }

            var cartDetailsByUser = _repository.CartDetail.GetAll().Where(p => p.CartId == cart.Id).ToList();
            if(cartDetailsByUser.ToList().Count == 1)
            {
                _repository.Cart.Remove(cart.Id);
            }
            _repository.CartDetail.Remove(cartDetailId);
            _repository.Save();
            return new ApiSuccessResult<bool>(true);
        }
    }
}
