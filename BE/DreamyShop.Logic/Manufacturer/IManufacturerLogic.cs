using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;

namespace DreamyShop.Logic.Manufacturer
{
    public interface IManufacturerLogic
    {
        Task<ApiResult<PageResult<ManufacturerDto>>> GetAllManufacturer(PagingRequest pagingRequest);
    }
}
