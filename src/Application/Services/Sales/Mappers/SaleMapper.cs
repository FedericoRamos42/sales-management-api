using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Customers.Models;
using Application.Services.Sales.Models;
using Domain.Enitites;

namespace Application.Services.Sales.Mappers
{
    public static class SaleMapper
    {
        public static SaleDto ToDto(this Sale sale)
        {
            return new SaleDto
            {
                Id = sale.Id,
                Date = sale.CreatedAt.ToString("dd/MM/yy"),
                CustomerName = sale.Customer?.Name!,
                //PaymenthMethod = sale.PaymenthMethod.ToString(),
                TotalAmount = sale.TotalAmount,
                Items = sale.Items.Select(x => x.ToDto()).ToList(),
            };

        }
        public static SaleDto ToDto(this Sale sale,Customer customer)
        {
            return new SaleDto
            {
                Id = sale.Id,
                Date = sale.CreatedAt.ToString("dd/MM/yy"),
                CustomerName = customer.Name,
                //PaymenthMethod = sale.PaymenthMethod.ToString(),
                TotalAmount = sale.TotalAmount,
                Items = sale.Items.Select(x => x.ToDto()).ToList(),
            };

        }

        public static List<SaleDto> ToListDto(this List<Sale> sales) => sales.Select(ToDto).ToList();
    }
}
