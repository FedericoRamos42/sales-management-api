using Application.Services.Sales.Interfaces;
using Application.Services.Sales.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            foreach (var s in sales)
            {
                wsSales.Cell(row, 1).Value = s.Id;
                wsSales.Cell(row, 2).Value = s.Date;
                wsSales.Cell(row, 3).Value = s.CustomerName;
                wsSales.Cell(row, 4).Value = s.PaymenthMethod;
                wsSales.Cell(row, 5).Value = s.TotalAmount;
                row++;
            }
            
            using var ms = new MemoryStream();
            wb.SaveAs(ms);
            return ms.ToArray();
        }
    }
}
