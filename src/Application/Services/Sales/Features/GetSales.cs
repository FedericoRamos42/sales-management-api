using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Sales.Mappers;
using Application.Services.Sales.Models;
using Application.Utils.Result;
using Domain.Enitites;

using Domain.Interfaces;

namespace Application.Services.Sales.Features
{
    public class GetSales
    {
        private readonly IUnitOfWork _repository;

        public GetSales(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<SaleDto>>> Execute()
        {
            var sales = await _repository.Sales.GetAllSales();

            var dto = sales.ToListDto();
            return Result<List<SaleDto>>.Succes(dto);
        }
    }
}
