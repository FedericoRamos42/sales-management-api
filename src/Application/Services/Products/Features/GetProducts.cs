using Application.Services.Customers.Models;
using Application.Services.Producto.Mappers;
using Application.Services.Products.Models;
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

namespace Application.Services.Producto.Features
{
    public class GetProducts
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<PaginationParams> _validator;
        public GetProducts(IUnitOfWork repository, IValidator<PaginationParams> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<PaginationList<ProductDto>>> Execute(PaginationParams request)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(s => s.ErrorMessage).ToList();
                return Result<PaginationList<ProductDto>>.Failure(errors);
            }

            List<Product> products =  await _repository.Products.GetByPagination(p=> p.Id,request.PageIndex,request.PageSize, p=> p.Category, p=> p.Prices);
            var count = await _repository.Products.Count();
            var totalPage = (int) Math.Ceiling((double)count / request.PageSize);

            var dto = products.ToListDto();
            var pagination = new PaginationList<ProductDto>(dto,request.PageIndex,request.PageSize);

            return Result<PaginationList<ProductDto>>.Succes(pagination);
        }

    }
}
