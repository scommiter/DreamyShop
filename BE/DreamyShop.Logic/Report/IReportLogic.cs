using DreamyShop.Domain.Shared.Dtos;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Logic.Report
{
    public interface IReportLogic
    {
        byte[] ExporttoExcel<T>(List<ProductDto> products, string filename);
    }
}
