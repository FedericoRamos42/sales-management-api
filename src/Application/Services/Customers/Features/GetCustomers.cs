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
using FluentValidation;

namespace Application.Services.Customers.Features
{
    public class GetCustomers
    {

        private readonly IUnitOfWork _repository;
        private readonly IValidator<PaginationParams>_validator;
        public GetCustomers(IUnitOfWork repository,IValidator<PaginationParams> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<Result<PaginationList<CustomerDto>>> Execute(PaginationParams request)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(s=>s.ErrorMessage).ToList();
                return Result<PaginationList<CustomerDto>>.Failure(errors);
            }

            var customers = await _repository.Customers.GetByPagination(c=>c.Id,request.PageIndex,request.PageSize);
            var count = await _repository.Customers.Count();
            var totalPages = (int)Math.Ceiling((double)count / request.PageSize);

            var dto = customers.ToListDto();

            var paginated = new PaginationList<CustomerDto>(dto,request.PageIndex,totalPages);
            return Result<PaginationList<CustomerDto>>.Succes(paginated);
        }
    }
}
