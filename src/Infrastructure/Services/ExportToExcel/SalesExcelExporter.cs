using Application.Services.Sales.Interfaces;
using Application.Services.Sales.Models;
using ClosedXML.Excel;

namespace Infrastructure.Services.ExportToExcel
{
    public class SalesExcelExporter : ISalesExcelExporter
    {
        public byte[] ExportSalesToExcel(List<SaleDto> sales)
        {
            using var wb = new XLWorkbook();
            var wsSales = wb.Worksheets.Add("Ventas");

            wsSales.Cell(1, 1).Value = "Nro Venta";
            wsSales.Cell(1, 2).Value = "Fecha";
            wsSales.Cell(1, 3).Value = "Cliente";
            wsSales.Cell(1, 4).Value = "MetodoPago";
            wsSales.Cell(1, 5).Value = "MontoTotal";

            var row = 2;
            foreach (var s in sales ?? [])
            {
                wsSales.Cell(row, 1).Value = s.Id;

                if (DateTime.TryParse(s.Date, out var parsedDate))
                {
                    wsSales.Cell(row, 2).Value = parsedDate;
                    wsSales.Cell(row, 2).Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
                }
                else
                {
                    wsSales.Cell(row, 2).Value = s.Date;
                }

                wsSales.Cell(row, 3).Value = s.CustomerName;
                wsSales.Cell(row, 4).Value = s.PaymenthMethod;
                wsSales.Cell(row, 5).Value = s.TotalAmount;
                wsSales.Cell(row, 5).Style.NumberFormat.Format = "#,##0.00";
                row++;
            }

            var lastDataRow = Math.Max(2, row - 1);
            var fullRange = wsSales.Range(1, 1, lastDataRow, 5);
            var headerRange = wsSales.Range(1, 1, 1, 5);

            wsSales.Row(1).Height = 26;
            wsSales.Rows(2, lastDataRow).Height = 22;

            headerRange.Style.Font.Bold = true;
            headerRange.Style.Font.FontColor = XLColor.White;
            headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#1F4E78");
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            headerRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            fullRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            fullRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            fullRange.Style.Border.OutsideBorderColor = XLColor.FromHtml("#D9D9D9");
            fullRange.Style.Border.InsideBorderColor = XLColor.FromHtml("#D9D9D9");

            wsSales.Column(1).Width = 12;
            wsSales.Column(2).Width = 22;
            wsSales.Column(3).Width = 30;
            wsSales.Column(4).Width = 18;
            wsSales.Column(5).Width = 16;

            wsSales.Column(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsSales.Column(2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsSales.Column(4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsSales.Column(5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            wsSales.SheetView.FreezeRows(1);

            using var ms = new MemoryStream();
            wb.SaveAs(ms);
            return ms.ToArray();
        }
    }
}
