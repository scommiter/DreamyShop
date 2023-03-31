using AutoMapper;
using AutoMapper.QueryableExtensions;
using DreamyShop.Common.Exceptions;
using DreamyShop.Common.Results;
using DreamyShop.Domain;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Domain.Shared.Types;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Repository.RepositoryWrapper;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;

namespace DreamyShop.Logic.Product
{
    public class ProductLogic : IProductLogic
    {
        private readonly DreamyShopDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ProductLogic(
            DreamyShopDbContext context,
            IRepositoryWrapper repository,
            IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<ProductDto>> CreateProduct(ProductCreateUpdateDto productCreateUpdateDto)
        {
            var newProduct = _mapper.Map<Domain.Product>(productCreateUpdateDto);
            await _repository.Product.AddAsync(newProduct);
            _repository.Save();
            return new ApiSuccessResult<ProductDto>(_mapper.Map<ProductDto>(newProduct));
        }

        public async Task<ApiResult<PageResult<ProductDto>>> GetAllProduct(int page, int limit)
        {
            var productPagings = _context.Products
                                .Include(opt => opt.Manufacturer)
                                .Include(opt => opt.ProductCategory)
                                .OrderByDescending(p => p.DateCreated)
                                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                .Skip((page - 1) * limit)
                                .Take(limit)
                                .ToList();
            var pageResult = new PageResult<ProductDto>()
            {
                Items = productPagings,
                Totals = productPagings.Count()
            };
            return new ApiSuccessResult<PageResult<ProductDto>>(pageResult);
        }

        public async Task<ApiResult<PageResult<ProductAttributeDto>>> GetListProductAttribute(Guid productId)
        {
            var product = _repository.Product.GetByIdAsync(productId);
            var productAttributes = from pa in _context.ProductAttributes
                                    join padt in _context.ProductAttributeDateTimes on pa.Id equals padt.AttributeId into aDateTimeTable
                                    from padt in aDateTimeTable.DefaultIfEmpty()
                                    join pad in _context.ProductAttributeDecimals on pa.Id equals pad.AttributeId into aDecimalTable
                                    from pad in aDecimalTable.DefaultIfEmpty()
                                    join pai in _context.ProductAttributeInts on pa.Id equals pai.AttributeId into aIntTable
                                    from pai in aIntTable.DefaultIfEmpty()
                                    join pat in _context.ProductAttributeTexts on pa.Id equals pat.AttributeId into aTextTable
                                    from pat in aTextTable.DefaultIfEmpty()
                                    join pavc in _context.ProductAttributeVarchars on pa.Id equals pavc.AttributeId into aVarcharTable
                                    from pavc in aVarcharTable.DefaultIfEmpty()
                                    where (padt == null || padt.ProductId == productId)
                                    && (pad == null || pad.ProductId == productId)
                                    && (pai == null || pai.ProductId == productId)
                                    && (pat == null || pat.ProductId == productId)
                                    && (pavc == null || pavc.ProductId == productId)
                                    select new ProductAttributeDto()
                                    {
                                        Name = pa.Name,
                                        AttributeId = pa.Id,
                                        DataType = pa.DataType,
                                        Code = pa.Code,
                                        ProductId = productId,
                                        DateTimeValue = padt != null ? padt.Value : null,
                                        DecimalValue = pad != null ? pad.Value : null,
                                        IntValue = pai != null ? pai.Value : null,
                                        TextValue = pat != null ? pat.Value : null,
                                        VarcharValue = pavc != null ? pavc.Value : null,
                                        DateTimeId = padt != null ? padt.Id : null,
                                        DecimalId = pad != null ? pad.Id : null,
                                        IntId = pai != null ? pai.Id : null,
                                        TextId = pat != null ? pat.Id : null,
                                        VarcharId = pavc != null ? pavc.Id : null,
                                    };
            productAttributes = productAttributes.Where(x => x.DateTimeId != null
                           || x.DecimalId != null
                           || x.IntValue != null
                           || x.TextId != null
                           || x.VarcharId != null);

            var pageResult = new PageResult<ProductAttributeDto>()
            {
                Items = productAttributes.ToList() ?? new List<ProductAttributeDto>(),
                Totals = productAttributes.Count()
            };
            return new ApiSuccessResult<PageResult<ProductAttributeDto>>(pageResult);
        }

        public async Task<ApiResult<bool>> CreateAtributeProduct(CreateProductAttributeDto productAttributeDto)
        {
            var product = _repository.Product.GetByIdAsync(productAttributeDto.ProductId);
            if (product == null)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var attribute = await _repository.ProductAttribute.GetByIdAsync(productAttributeDto.AttributeId);
            if (attribute == null)
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            var attributeDto = _mapper.Map<ProductAttributeDto>(attribute);
            var newAttributeId = Guid.NewGuid();
            switch (attributeDto.DataType)
            {
                case AttributeType.Date:
                    if (productAttributeDto.DateTimeValue == null)
                    {
                        return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
                    }
                    var productAttributeDateTime = new ProductAttributeDateTime(newAttributeId, productAttributeDto.AttributeId, productAttributeDto.ProductId, productAttributeDto.DateTimeValue);
                    await _repository.ProductAttributeDateTime.AddAsync(productAttributeDateTime);
                    break;

                case AttributeType.Int:
                    if (productAttributeDto.IntValue == null)
                    {
                        return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
                    }
                    var productAttributeInt = new ProductAttributeInt(newAttributeId, productAttributeDto.AttributeId, productAttributeDto.ProductId, productAttributeDto.IntValue.Value);
                    await _repository.ProductAttributeInt.AddAsync(productAttributeInt);
                    break;

                case AttributeType.Decimal:
                    if (productAttributeDto.DecimalValue == null)
                    {
                        return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
                    }
                    var productAttributeDecimal = new ProductAttributeDecimal(newAttributeId, productAttributeDto.AttributeId, productAttributeDto.ProductId, productAttributeDto.DecimalValue.Value);
                    await _repository.ProductAttributeDecimal.AddAsync(productAttributeDecimal);
                    break;

                case AttributeType.Varchar:
                    if (productAttributeDto.VarcharValue == null)
                    {
                        return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
                    }
                    var productAttributeVarchar = new ProductAttributeVarchar(newAttributeId, productAttributeDto.AttributeId, productAttributeDto.ProductId, productAttributeDto.VarcharValue);
                    await _repository.ProductAttributeVarchar.AddAsync(productAttributeVarchar);
                    break;

                case AttributeType.Text:
                    if (productAttributeDto.TextValue == null)
                    {
                        return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
                    }
                    var productAttributeText = new ProductAttributeText(newAttributeId, productAttributeDto.AttributeId, productAttributeDto.ProductId, productAttributeDto.TextValue);
                    await _repository.ProductAttributeText.AddAsync(productAttributeText);
                    break;
            }
            _repository.Save();
            return new ApiSuccessResult<bool>(true);
        }
        public async Task<ApiResult<ProductAttributeDto>> UpdateProductAttributeAsync(Guid attributeId, CreateProductAttributeDto updateProductAttributeDto)
        {
            var product = _repository.Product.GetByIdAsync(updateProductAttributeDto.ProductId);
            if (product == null)
            {
                return new ApiErrorResult<ProductAttributeDto>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var attribute = await _repository.ProductAttribute.GetByIdAsync(updateProductAttributeDto.AttributeId);
            if (attribute == null)
                return new ApiErrorResult<ProductAttributeDto>((int)ErrorCodes.DataEntryIsNotExisted);
            var attributeDto = _mapper.Map<ProductAttributeDto>(attribute);
            switch (attributeDto.DataType)
            {
                case AttributeType.Date:
                    var attributeDateTime = await _repository.ProductAttributeDateTime.GetByIdAsync(attributeId);
                    if (attributeDateTime == null || updateProductAttributeDto.DateTimeValue == null)
                        return new ApiErrorResult<ProductAttributeDto>((int)ErrorCodes.DataEntryIsNotExisted);
                    attributeDateTime.Value = updateProductAttributeDto.DateTimeValue.Value;
                    _repository.ProductAttributeDateTime.Update(attributeDateTime);
                    break;
                case AttributeType.Int:
                    var attributeInt = await _repository.ProductAttributeInt.GetByIdAsync(attributeId);
                    if (attributeInt == null || updateProductAttributeDto.IntValue == null)
                        return new ApiErrorResult<ProductAttributeDto>((int)ErrorCodes.DataEntryIsNotExisted);
                    attributeInt.Value = updateProductAttributeDto.IntValue.Value;
                    _repository.ProductAttributeInt.Update(attributeInt);
                    break;
                case AttributeType.Decimal:
                    var attributeDecimal = await _repository.ProductAttributeDecimal.GetByIdAsync(attributeId);
                    if (attributeDecimal == null || updateProductAttributeDto.DecimalValue == null)
                        return new ApiErrorResult<ProductAttributeDto>((int)ErrorCodes.DataEntryIsNotExisted);
                    attributeDecimal.Value = updateProductAttributeDto.DecimalValue.Value;
                    _repository.ProductAttributeDecimal.Update(attributeDecimal);
                    break;
                case AttributeType.Varchar:
                    var attributeVarchar = await _repository.ProductAttributeVarchar.GetByIdAsync(attributeId);
                    if (attributeVarchar == null || updateProductAttributeDto.VarcharValue == null)
                        return new ApiErrorResult<ProductAttributeDto>((int)ErrorCodes.DataEntryIsNotExisted);
                    attributeVarchar.Value = updateProductAttributeDto.VarcharValue;
                    _repository.ProductAttributeVarchar.Update(attributeVarchar);
                    break;
                case AttributeType.Text:
                    var attributeText = await _repository.ProductAttributeText.GetByIdAsync(attributeId);
                    if (attributeText == null || updateProductAttributeDto.TextValue == null)
                        return new ApiErrorResult<ProductAttributeDto>((int)ErrorCodes.DataEntryIsNotExisted);
                    attributeText.Value = updateProductAttributeDto.TextValue;
                    _repository.ProductAttributeText.Update(attributeText);
                    break;
            }
            _repository.Save();
            return new ApiSuccessResult<ProductAttributeDto>(new ProductAttributeDto
            {
                AttributeId = updateProductAttributeDto.AttributeId,
                Code = attribute.Code,
                DataType = attribute.DataType,
                DateTimeValue = updateProductAttributeDto.DateTimeValue,
                DecimalValue = updateProductAttributeDto.DecimalValue,
                Id = attributeId,
                IntValue = updateProductAttributeDto.IntValue,
                Name = attribute.Name,
                ProductId = updateProductAttributeDto.ProductId,
                TextValue = updateProductAttributeDto.TextValue
            });
        }

        public async Task<ApiResult<bool>> RemoveAtributeProduct(Guid attributeId, Guid attributeTypeId)
        {
            var attribute = await _repository.ProductAttribute.GetByIdAsync(attributeId);
            if (attribute == null)
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            var attributeDto = _mapper.Map<ProductAttributeDto>(attribute);
            switch (attributeDto.DataType)
            {
                case AttributeType.Date:
                    try
                    {
                        await _repository.ProductAttributeDateTime.BeginTransactionAsync();
                        _repository.ProductAttributeDateTime.Remove(attributeTypeId);
                        await _repository.ProductAttributeDateTime.EndTransactionAsync();
                    }
                    catch
                    {
                        await _repository.ProductAttributeDateTime.RollbackTransactionAsync();
                        return new ApiErrorResult<bool>((int)ErrorCodes.DeleteFailed);
                    }
                    break;

                case AttributeType.Int:
                    try
                    {
                        await _repository.ProductAttributeInt.BeginTransactionAsync();
                        _repository.ProductAttributeInt.Remove(attributeTypeId);
                        await _repository.ProductAttributeInt.EndTransactionAsync();
                    }
                    catch
                    {
                        await _repository.ProductAttributeInt.RollbackTransactionAsync();
                        return new ApiErrorResult<bool>((int)ErrorCodes.DeleteFailed);
                    }
                    break;

                case AttributeType.Decimal:
                    try
                    {
                        await _repository.ProductAttributeDecimal.BeginTransactionAsync();
                        _repository.ProductAttributeDecimal.Remove(attributeTypeId);
                        await _repository.ProductAttributeDecimal.EndTransactionAsync();
                    }
                    catch
                    {
                        await _repository.ProductAttributeDecimal.RollbackTransactionAsync();
                        return new ApiErrorResult<bool>((int)ErrorCodes.DeleteFailed);
                    }
                    break;

                case AttributeType.Varchar:
                    try
                    {
                        await _repository.ProductAttributeVarchar.BeginTransactionAsync();
                        _repository.ProductAttributeVarchar.Remove(attributeTypeId);
                        await _repository.ProductAttributeVarchar.EndTransactionAsync();
                    }
                    catch
                    {
                        await _repository.ProductAttributeVarchar.RollbackTransactionAsync();
                        return new ApiErrorResult<bool>((int)ErrorCodes.DeleteFailed);
                    }
                    break;

                case AttributeType.Text:
                    try
                    {
                        await _repository.ProductAttributeText.BeginTransactionAsync();
                        _repository.ProductAttributeText.Remove(attributeTypeId);
                        await _repository.ProductAttributeText.EndTransactionAsync();
                    }
                    catch
                    {
                        await _repository.ProductAttributeText.RollbackTransactionAsync();
                        return new ApiErrorResult<bool>((int)ErrorCodes.DeleteFailed);
                    }
                    break;
            }
            _repository.Save();
            return new ApiSuccessResult<bool>(true);
        }
    }
}