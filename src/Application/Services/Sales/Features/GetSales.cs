using Application.Services.Products.Models;
using Application.Services.Sales.Mappers;
using Application.Services.Sales.Models;
using Application.Utils.Pagination;
using Application.Utils.Result;
using Domain.Enitites;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Sales.Features
{
    public class GetSales
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<PaginationParams> _validator;
        public GetSales(IUnitOfWork repository,IValidator<PaginationParams>validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<PaginationList<SaleDto>>> Execute(PaginationParams request)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(s => s.ErrorMessage).ToList();
                return Result<PaginationList<SaleDto>>.Failure(errors);
            }

            var sales = await _repository.Sales.GetAllSalesByPagination(request.PageIndex, request.PageSize);
            var count = await _repository.Sales.Count();
            var totalPages = (int) Math.Ceiling((double)count / request.PageSize);

            var dto = sales.ToListDto();
            var pagination = new PaginationList<SaleDto>(dto,request.PageIndex,totalPages);
            return Result<PaginationList<SaleDto>>.Succes(pagination);
        }
    }
}
