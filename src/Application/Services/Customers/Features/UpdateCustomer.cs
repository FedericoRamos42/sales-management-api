using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Customers.Mappers;
using Application.Services.Customers.Models;
using Application.Services.Customers.Models.Request;
using Application.Utils.Result;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Services.Customers.Features
{
    public class UpdateCustomer
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CustomerForRequest> _validator;
        public UpdateCustomer(IUnitOfWork repository,IValidator<CustomerForRequest>validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<CustomerDto>> Execute(int id,CustomerForRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return Result<CustomerDto>.Failure(errors);
            }

            var customer = await _repository.Customers.Get(p => p.Id == id);
            if (customer is null)
                return Result<CustomerDto>.Failure($"customer with id {id} does not exist");

            customer.PhoneNumber = request.PhoneNumber;
            customer.Name = request.Name;
            customer.Email = request.Email;
            customer.Address = request.Address;

            await _repository.SaveChangesAsync();

            var dto = customer.ToDto();
            return Result<CustomerDto>.Succes(dto);
        }
    }
}
