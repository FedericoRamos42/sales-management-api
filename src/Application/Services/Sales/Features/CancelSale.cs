using Application.Services.Sales.Mappers;
using Application.Services.Sales.Models;
using Application.Utils.Result;
using Domain.Enums;
using Domain.Interfaces;
using System.Diagnostics;

namespace Application.Services.Sales.Features
{
    public class CancelSale
    {
        private readonly IUnitOfWork _repository;
        public CancelSale(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<SaleDto>> Execute(int id)
        {
            var sale = await _repository.Sales.Get(s => s.Id == id, s => s.Items,s=>s.Customer,s=>s.Customer.Account);

            if (sale is null)
            {
                return Result<SaleDto>.Failure($"Sale with id {id} does not exist");
            }

            if (sale.Status == SaleStatus.Canceled)
            {
                return Result<SaleDto>.Failure("Sale already canceled");
            }


            var debt = sale.TotalAmount - sale.PaidAmount;

            if (debt > 0) 
            {
                sale.Customer.Account.AddMovement(debt, $"Reversal of debt for sale #{sale.Id}");
            }

            foreach (var item in sale.Items)
            {
                var product = await _repository.Products.Get(p => p.Id == item.ProductId);
                product!.Stock += item.Quantity;
            }

            sale.Status = SaleStatus.Canceled;

            await _repository.SaveChangesAsync();

            return Result<SaleDto>.Succes(sale.ToDto());

        }
    }
}
