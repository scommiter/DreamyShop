using DreamyShop.Domain;
using DreamyShop.Domain.Shared.Dtos;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System.Drawing;

namespace DreamyShop.Logic.Report
{
    public class ReportLogic : IReportLogic
    {
        public byte[] ExporttoExcel<T>(List<ProductDto> products, string filename)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            var worksheet = excel.Workbook.Worksheets.Add(filename);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 15;
            worksheet.Column(1).Width = 20;
            worksheet.Column(7).Width = 30;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;

            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "Name";
            worksheet.Cells[1, 3].Value = "Code";
            worksheet.Cells[1, 4].Value = "Product Type";
            worksheet.Cells[1, 5].Value = "Category Name";
            worksheet.Cells[1, 6].Value = "Manufacturer Name";
            worksheet.Cells[1, 7].Value = "Description";
            worksheet.Cells[1, 8].Value = "Active";
            worksheet.Cells[1, 9].Value = "Visibility";
            worksheet.Cells[1, 10].Value = "Attribute Name";
            worksheet.Cells[1, 11].Value = "SKU";
            worksheet.Cells[1, 12].Value = "Quantity";
            worksheet.Cells[1, 13].Value = "Price";
            var r = 2;
            foreach (var product in products)
            {
                worksheet.Cells[r, 1].Value = product.Id;
                worksheet.Cells[r, 2].Value = product.Name;
                worksheet.Cells[r, 3].Value = product.Code;
                worksheet.Cells[r, 4].Value = product.ProductType;
                worksheet.Cells[r, 5].Value = product.CategoryName;
                worksheet.Cells[r, 6].Value = product.ManufacturerName;
                worksheet.Cells[r, 7].Value = product.Description;
                worksheet.Cells[r, 8].Value = product.IsActive;
                worksheet.Cells[r, 9].Value = product.IsVisibility;
                worksheet.Cells[r, 10].Value = product.DateCreated.ToString("MM/dd/yyyy");
                worksheet.Cells[r, 11].Value = product.DateUpdated.ToString("MM/dd/yyyy");
                if(product.ProductAttributeDisplayDtos.Count == 0)
                {
                    r++;
                    continue;
                }
                foreach (var productAttribute in product.ProductAttributeDisplayDtos)
                {
                    worksheet.Cells[r, 12].Value = string.Join(",", productAttribute.AttributeNames);
                    worksheet.Cells[r, 13].Value = productAttribute.SKU;
                    worksheet.Cells[r, 14].Value = productAttribute.Quantity;
                    worksheet.Cells[r, 15].Value = productAttribute.Price;
                    if (productAttribute.Quantity == 0)
                    {
                        worksheet.Cells[$"L{r}:O{r}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[$"L{r}:O{r}"].Style.Fill.BackgroundColor.SetColor(Color.DarkGray);
                    }
                    r++;
                }
            }

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var rangeHead = worksheet.Cells["A1:O1"])
            {
                // Set PatternType
                rangeHead.Style.Fill.PatternType = ExcelFillStyle.Solid;
                // Set Màu cho Background
                rangeHead.Style.Fill.BackgroundColor.SetColor(Color.LightYellow);
                // Canh giữa cho các text
                rangeHead.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                rangeHead.Style.Font.SetFromFont("Arial", 14);
                rangeHead.Style.Font.Bold = true;
                //// Set Border
                //range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                //// Set màu ch Border
                //range.Style.Border.Bottom.Color.SetColor(Color.Red);
            }

            using (var range = worksheet.Cells[$"A1:O{r}"])
            {
                worksheet.Cells[$"A1:O{r}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.DarkGray);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Font.SetFromFont("Arial", 10);
            }

            return excel.GetAsByteArray();
        }
    }
}
