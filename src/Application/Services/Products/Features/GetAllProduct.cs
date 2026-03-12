using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Producto.Mappers;
using Application.Services.Products.Models;
using Application.Utils.Result;
using Domain.Enitites;
using Domain.Interfaces;

namespace Application.Services.Producto.Features
{
    public class GetAllProduct
    {
        private readonly IUnitOfWork _repository;

        public GetAllProduct(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<ProductDto>>> Execute()
        {
            List<Product> products = (List<Product>) await _repository.Products.Search(null, p => p.Category, p => p.Prices);
            var dto = products.ToListDto();
            return Result<List<ProductDto>>.Succes(dto);
        }

    }
}
