using Application.Services.Customers.Mappers;
using Application.Services.Customers.Models;
using Application.Utils.Result;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Customers.Features
{
    public class GetCustomer
    {
        private readonly IUnitOfWork _repository;
        public GetCustomer(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<CustomerDto>> Execute(int id)
        {
            var customer = await _repository.Customers.Get(c => c.Id == id);
            if (customer is null)
            {
                return Result<CustomerDto>.Failure($"Customer with id {id} does not exist");
            }
            var customerDto = customer.ToDto();   
            return Result<CustomerDto>.Succes(customerDto);
        }
    }
}
