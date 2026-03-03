using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Sales.Features;

namespace Application.Services.Sales
{
    public record SaleUseCases(
        CreateSale CreateSale,
        GetAllSales GetAllSale,
        GetSale GetSale,
        RegisterSalePayment RegisterPayment,
        ExportSalesToExcel ExportSales
       );
}
