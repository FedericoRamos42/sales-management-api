using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Customers.Mappers;
using Application.Services.Customers.Models;
using Application.Utils.Pagination;
using Application.Utils.Result;
using Domain.Enitites;
using Domain.Interfaces;

namespace Application.Services.Customers.Features
{
    public class GetCustomers
    {

        private readonly IUnitOfWork _repository;
        public GetCustomers(IUnitOfWork repository)
        {
            _repository = repository;
        }
        public async Task<Result<PaginationList<CustomerDto>>> Execute(int pageIndex, int pageSize)
        {
            var customers = await _repository.Customers.GetByPagination(c=>c.Id,pageIndex,pageSize);
            var count = await _repository.Customers.Count();
            var totalPages = (int)Math.Ceiling((double)count / pageSize);

            var dto = customers.ToListDto();

            var paginated = new PaginationList<CustomerDto>(dto,pageIndex,totalPages);
            return Result<PaginationList<CustomerDto>>.Succes(paginated);
        }
    }
}
