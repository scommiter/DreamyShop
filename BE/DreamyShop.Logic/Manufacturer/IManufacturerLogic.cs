using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Logic.Manufacturer
{
    public interface IManufacturerLogic
    {
        Task<ApiResult<PageResult<ManufacturerDto>>> GetAllManufacturer(PagingRequest pagingRequest);
        Task<ApiResult<ManufacturerDto>> CreateManufacturer(ManufacturerCreateUpdateDto manufacturerCreateUpdateDto);
        Task<ApiResult<ManufacturerDto>> UpdateManufacturer(Guid id, ManufacturerCreateUpdateDto manufacturerCreateUpdateDto);
        Task<ApiResult<bool>> RemoveManufacturer(Guid id);
    }
}
