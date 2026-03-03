using Application.Result;
using Application.Services.Sales.Mappers;
using Application.Services.Sales.Models;
using Application.Services.Sales.Models.Request;
using Domain.Enums;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Sales.Features
{
    public class RegisterSalePayment
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<RegisterPaymentRequest> _validator;

        public RegisterSalePayment(IUnitOfWork repository, IValidator<RegisterPaymentRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<SaleDto>> Execute(int id , RegisterPaymentRequest request)
        {
            var validation = await _validator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(v=>v.ErrorMessage).ToList();
                return Result<SaleDto>.Failure(errors);
            }

            var sale = await _repository.Sales.Get(s => s.Id == id, s=>s.Customer, s=>s.Customer.Account);

            if(sale is null)
            {
                return Result<SaleDto>.Failure($"sale with id {id} does not exist");
            }

            if(sale.Status == SaleStatus.Paid || sale.Status == SaleStatus.Canceled)
            {
                return Result<SaleDto>.Failure($"this sale has been paid or canceled");
            }

            if(request.Amount > sale.TotalAmount - sale.PaidAmount)
            {
                return Result<SaleDto>.Failure("Amount exceeds remaining debt." );
            }

            sale.RegisterPayment(request.Amount, request.PaymentMethod);
            sale.Customer.Account.AddMovement(request.Amount, $"Payment for sale #{sale.Id}");

            await _repository.SaveChangesAsync();

            return Result<SaleDto>.Succes(sale.ToDto());
        }
    }
}
