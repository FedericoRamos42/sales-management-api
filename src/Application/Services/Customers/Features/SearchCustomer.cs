using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Customers.Mappers;
using Application.Services.Customers.Models;
using Application.Utils.Result;
using Domain.Enitites;
using Domain.Interfaces;

namespace Application.Services.Customers.Features
{
    public class SearchCustomer
    {
        private readonly IUnitOfWork _repository;
        public SearchCustomer(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<CustomerDto>>> Execute(string name)
        {
            var customers = (List<Customer>) await _repository.Customers.Search(c => c.Name == name);
            var dto = customers.ToListDto();
            return Result<IEnumerable<CustomerDto>>.Succes(dto);
        }
    }
}
