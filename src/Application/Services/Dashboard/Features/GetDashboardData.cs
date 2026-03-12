using Application.Services.Dashboard.Models;
using Application.Services.Producto.Mappers;
using Application.Utils.Result;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Dashboard.Features
{
    public class GetDashboardData
    {
        private readonly IUnitOfWork _repository;
        public GetDashboardData(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<DashboardDataDto>> Execute()
        {
            var totalSales = await _repository.Sales.Count();
            var totalSalesAmount = await _repository.Dashboard.GetTotalSalesAmount();
            var totalProducts = await _repository.Products.Count();
            var totalCustomers = await _repository.Customers.Count();
            var lowStockProducts = await _repository.Dashboard.GetLowStockProducts(5,5);
            
            return Result<DashboardDataDto>.Succes(new DashboardDataDto
            {
                TotalSalesAmount = totalSalesAmount,
                TotalSalesCount = totalSales,
                TotalProducts = totalProducts,
                TotalCustomers = totalCustomers,
                AverageSalesAmount = totalSales == 0 ? 0 : totalSalesAmount / totalSales,
                LowStockProducts = lowStockProducts.ToListDto(),
            });
        }
    }
}
