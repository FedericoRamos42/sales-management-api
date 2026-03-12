using Application.Services.Customers.Mappers;
using Application.Services.Customers.Models;
using Application.Utils.Result;
using Domain.Interfaces;

namespace Application.Services.Customers.Features
{
    public class DeleteCustomer
    {
        private readonly IUnitOfWork _repository;
        public DeleteCustomer(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<CustomerDto>> Execute(int id)
        {
            var customer = await _repository.Customers.Get(p => p.Id == id);

            if (customer is null)
                return Result<CustomerDto>.Failure($"customer with id {id} does not exist");

            await _repository.Customers.Delete(customer);
            await _repository.SaveChangesAsync();

            var dto = customer.ToDto();
            return Result<CustomerDto>.Succes(dto);
        }
    }
}
