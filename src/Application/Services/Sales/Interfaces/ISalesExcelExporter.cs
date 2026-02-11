using Application.Services.Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Sales.Interfaces
{
    public interface ISalesExcelExporter
    {
        byte[] ExportSalesToExcel(List<SaleDto> sales);
    }
}
