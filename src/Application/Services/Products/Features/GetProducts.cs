using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Producto.Mappers;
using Application.Services.Products.Models;
using Application.Utils.Pagination;
using Application.Utils.Result;
using Domain.Enitites;
using Domain.Interfaces;

namespace Application.Services.Producto.Features
{
    public class GetProducts
    {
        private readonly IUnitOfWork _repository;

        public GetProducts(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<PaginationList<ProductDto>>> Execute(PaginationParams request)
        {
            List<Product> products =  await _repository.Products.GetByPagination(p=> p.Id,request.PageIndex,request.PageSize, p=> p.Category, p=> p.Prices);
            var count = await _repository.Products.Count();
            var totalPage = (int) Math.Ceiling((double)count / request.PageSize);

            var dto = products.ToListDto();
            var pagination = new PaginationList<ProductDto>(dto,request.PageIndex,request.PageSize);

            return Result<PaginationList<ProductDto>>.Succes(pagination);
        }

    }
}
