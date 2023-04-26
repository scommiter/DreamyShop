using DreamyShop.Domain;
using DreamyShop.Domain.Shared.Dtos;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Drawing;
using System.Text;

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
            worksheet.DefaultColWidth = 16;
            worksheet.Column(1).Width = 20;
            worksheet.Column(7).Width = 30;
            worksheet.Column(12).Width = 20;
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
            worksheet.Cells[1, 10].Value = "Date Create";
            worksheet.Cells[1, 11].Value = "Date Update";
            worksheet.Cells[1, 12].Value = "Attribute Name";
            worksheet.Cells[1, 13].Value = "SKU";
            worksheet.Cells[1, 14].Value = "Quantity";
            worksheet.Cells[1, 15].Value = "Price";

            worksheet.Cells[12, 2, 15, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[12, 2, 15, 2].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffedb3"));

            worksheet.Cells["A1:O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells["A1:O1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffedb3"));
            worksheet.Cells["A1:O1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["A1:O1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheet.Cells["A1:O1"].Style.Font.SetFromFont("Arial", 12);
            worksheet.Cells["A1:O1"].Style.Font.Bold = true;
            worksheet.Cells["A1:O1"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            SetBorderColHead(worksheet, 15);

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
                var cacherow = r;
                foreach (var productAttribute in product.ProductAttributeDisplayDtos)
                {
                    worksheet.Cells[r, 12].Value = string.Join(",", productAttribute.AttributeNames);
                    worksheet.Cells[r, 13].Value = productAttribute.SKU;
                    worksheet.Cells[r, 14].Value = productAttribute.Quantity;
                    worksheet.Cells[r, 15].Value = productAttribute.Price;
                    if (productAttribute.Quantity == 0)
                    {
                        worksheet.Cells[$"L{r}:O{r}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[$"L{r}:O{r}"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#bfbfbf"));
                    }
                    r++;
                }
                var range = cacherow;
                if (product.ProductAttributeDisplayDtos.Count > 0)
                {
                    range = range + product.ProductAttributeDisplayDtos.Count - 1;
                }
                SetBorderColDetail(worksheet, cacherow, 15, range);
                worksheet.Cells[$"A{cacherow}:O{cacherow + product.ProductAttributeDisplayDtos.Count - 1}"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            }

            using (var range = worksheet.Cells[$"A2:O{r}"])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                range.Style.Font.SetFromFont("Arial", 10);
            }

            return excel.GetAsByteArray();
        }

        private void SetBorderColDetail(ExcelWorksheet worksheet, int row, int col, int range)
        {
            for (int i = 1; i <= col; i++)
            {
                if(row != range && i <= 11)
                {
                    worksheet.Cells[row, i, range, i].Merge = true;
                }
                worksheet.Cells[row, i, range, i].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            }
        }

        private void SetBorderColHead(ExcelWorksheet worksheet, int col)
        {
            for (int i = 1; i <= col; i++)
            {
                worksheet.Cells[1,i].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            }
        }
    }
}
