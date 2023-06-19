using AutoMapper;
using DreamyShop.Common.Exceptions;
using DreamyShop.Common.Results;
using DreamyShop.Domain;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Domain.Shared.Types;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Logic.Conditions;
using DreamyShop.Repository.RepositoryWrapper;
using Microsoft.EntityFrameworkCore;

namespace DreamyShop.Logic.Bill
{
    public class BillLogic : IBillLogic
    {
        private readonly DreamyShopDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public BillLogic(
            DreamyShopDbContext context,
            IRepositoryWrapper repository,
            IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<bool>> CreateBill(BillCreateDto billCreateDto)
        {
            if (billCreateDto == null)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var newBill = new Domain.Bill
            {
                UserId = billCreateDto.UserId,
                TotalMoney = billCreateDto.TotalMoney,
                ShippingFee = billCreateDto.ShippingFee,
                Discount = billCreateDto.Discount,
                PaymentType = billCreateDto.PaymentType,
                Phone = billCreateDto.Phone,
                Note = billCreateDto.Note ?? "",
                Address = billCreateDto.Address,
                ZipCode = billCreateDto.ZipCode ?? "100000",
                PaymentStatus = billCreateDto.PaymentStatus,
                BillStatus = BillStatus.Preparing
            };
            await _repository.Bill.AddAsync(newBill);
            _repository.Save();
            await _repository.BillDetail.AddRangeAsync(billCreateDto.ItemCarts.Select(b => new BillDetail
            {
                BillId = newBill.Id,
                VariantProductId = b.ProductVariantId,
                Quantity = b.Quantity,
                TotalPrice = b.Price * b.Quantity,
                Tax = 0,
                Note = newBill.Note ?? ""
            }));
            _repository.Save();
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<List<BillDto>>> GetBills(int userId)
        {
            var bills = _repository.Bill.GetAll().Where(b => b.UserId == userId);
            if (!bills.Any())
            {
                return new ApiErrorResult<List<BillDto>>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var user = await _repository.User.GetByIdAsync(userId);
            var billList = from b in bills
                           join bd in _context.BillDetails on b.Id equals bd.BillId into bbd
                           from bd in bbd.DefaultIfEmpty()
                           join pv in _context.ProductVariants on bd.VariantProductId equals pv.Id into bdpv
                           from pv in bdpv.DefaultIfEmpty()
                           join ps in _context.Products on pv.ProductId equals ps.Id into ppv
                           from ps in ppv.DefaultIfEmpty()
                           select new { b, bd, pv, ps };
            var billDtos = await billList.GroupBy(b => b.b.Id)
                .Select(p => new BillDto
                {
                    UserName = user.FullName,
                    Address = p.Select(e => e.b.Address).FirstOrDefault(),
                    Phone = p.Select(e => e.b.Phone).FirstOrDefault(),
                    ShippingFee = p.Select(e => e.b.ShippingFee).FirstOrDefault(),
                    Discount = p.Select(e => e.b.Discount).FirstOrDefault(),
                    TotalMoney = p.Select(e => e.b.TotalMoney).FirstOrDefault(),
                    PaymentType = p.Select(e => e.b.PaymentType).FirstOrDefault(),
                    ZipCode = p.Select(e => e.b.ZipCode).FirstOrDefault(),
                    DateCreated = p.Select(e => e.b.DateCreated).FirstOrDefault(),
                    ItemCarts = p.Select(e => new CartItemsDto
                    {
                        ProductName = e.ps.Name,
                        ProductSKU = e.pv.SKU,
                        Quantity = e.bd.Quantity,
                        Price = e.bd.TotalPrice,
                        Tax = 0
                    }).ToList()
                }).OrderByDescending(pp => pp.DateCreated).ToListAsync();
            return new ApiSuccessResult<List<BillDto>>(billDtos);
        }

        public Task<ApiResult<List<BillDto>>> SearchBill(SearchBillCondition searchBillCondition)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<bool>> UpdateBill(BillUpdateDto billUpdateDto, int userId, int billId)
        {
            var bills = _repository.Bill.GetAll().Where(b => b.UserId == userId);
            if (!bills.Any())
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var billUpdate = bills.Where(b => b.Id == billId).ToList().FirstOrDefault();
            if (billUpdate == null)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            billUpdate.Phone ??= billUpdateDto.Phone;
            billUpdate.Address ??= billUpdateDto.Address;
            billUpdate.Note ??= billUpdateDto.Note;
            _repository.Bill.Update(billUpdate);
            _repository.Save();
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> DeleteBill(int userId, int billId)
        {
            var bills = _repository.Bill.GetAll().Where(b => b.UserId == userId);
            if (!bills.Any())
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            _repository.Bill.Remove(billId);
            _repository.Save();
            return new ApiSuccessResult<bool>(true);
        }
    }
}
