using Application.Services.Customers.Mappers;
using Application.Services.Customers.Models;
using Application.Services.Customers.Models.Request;
using Application.Utils.Result;
using Domain.Enitites;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Services.Customers.Features
{
    public class CreateCustomer
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CustomerForRequest> _validator;

        public CreateCustomer(IUnitOfWork repository,IValidator<CustomerForRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<CustomerDto>> Execute(CustomerForRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e=> e.ErrorMessage).ToList();
                return Result<CustomerDto>.Failure(errors);
            }

            Customer customer = new Customer()
            {
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                Account = new Account()
            };

            await _repository.Customers.Create(customer);
            await _repository.SaveChangesAsync();
            var dto = customer.ToDto();
            return Result<CustomerDto>.Succes(dto);
        }




    }
}
