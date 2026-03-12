using Application.Services.Customers.Models.Request;
using Application.Services.Customers.Models.Response;
using Application.Utils.Result;
using Domain.Enitites;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Customers.Features
{
    public class CreateAccountMovement
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateMovementRequest> _validator;
        public CreateAccountMovement(IUnitOfWork repository, IValidator<CreateMovementRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<CustomerAccountResponse>> Execute(int id, CreateMovementRequest request)
        {
            var validation = await _validator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(c=>c.ErrorMessage).ToList();
                return Result<CustomerAccountResponse>.Failure(errors);  
            }

            var customer = await _repository.Customers.Get(c => c.Id == id, c => c.Account,c=>c.Account.Movements);

            if (customer is null)
            {
                return Result<CustomerAccountResponse>.Failure($"customer with id {id} does not exist");
            }

            customer.Account.AddMovement(request.Amount, request.Description);

            await _repository.SaveChangesAsync();

            var dto = new CustomerAccountResponse()
            {
                CustomerId = id,
                CustomerName = customer.Name,
                Balance = customer.Account.Balance,
                Movement = customer.Account.Movements.Select(a => new AccountMovementDTO()
                {
                    Date = a.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                    Amount = a.Amount,
                    Description = a.Description,
                    BalanceAfter = a.BalanceAfter,
                }).ToList()
            };

            return Result<CustomerAccountResponse>.Succes(dto);
        }

    }
}
