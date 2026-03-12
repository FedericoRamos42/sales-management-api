using Application.Services.Sales.Mappers;
using Application.Services.Sales.Models;
using Application.Utils.Result;
using Domain.Enitites;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Sales.Features
{
    public class GetSale
    {
        private readonly IUnitOfWork _repository;

        public GetSale(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<SaleDto>> Execute(int saleId)
        {
            var sale = await _repository.Sales.Get(s => s.Id == saleId);
            if(sale is null)
            {
                return Result<SaleDto>.Failure($"sale whit id {saleId} does not exist");
            }
            var saleDto = sale.ToDto();
            return Result<SaleDto>.Succes(saleDto);
        }
    }
}
