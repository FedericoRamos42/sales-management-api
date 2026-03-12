using Application.Services.Customers.Models.Response;
using Application.Utils.Result;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Customers.Features
{
    public class GetCustomerAccount
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerAccount(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CustomerAccountResponse>> Execute(int id)
        {
            var accountMovements = await _unitOfWork.Accounts.Get(c => c.Id == id, c => c.Customer, c => c.Movements);

            if (accountMovements is null)
            {
                return Result<CustomerAccountResponse>.Failure($"Customer with id {id} does not exist");
            }

            var dto = new CustomerAccountResponse()
            {
                CustomerId = id,
                CustomerName = accountMovements.Customer.Name,
                Balance = accountMovements.Balance,
                Movement = accountMovements.Movements.Select(a => new AccountMovementDTO()
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
