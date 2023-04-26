using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using Microsoft.AspNetCore.Http;

namespace DreamyShop.Logic.Report
{
    public interface IReportLogic
    {
        byte[] ExporttoExcel<T>(List<ProductDto> products, string filename);
        Task<ApiResult<List<ProductCreateDto>>> ReadFromExcel(IFormFile reportFile);
    }
}
