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
    public class GetProduct
    {
        private readonly IUnitOfWork _repository;

        public GetProduct(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductDto>> Execute(int id)
        {
            var product = await _repository.Products.Get(p => p.Id == id,p=> p.Category);
            if (product is null)
                return Result<ProductDto>.Failure($"product with id {id} does not exist");

            var dto = product.ToDto();
            return Result<ProductDto>.Succes(dto);
        }
    }
}
