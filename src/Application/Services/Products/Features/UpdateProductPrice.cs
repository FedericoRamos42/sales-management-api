using Application.Services.Producto.Mappers;
using Application.Services.Products.Models;
using Application.Services.Products.Models.Request;
using Application.Utils.Result;
using Domain.Enitites;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Products.Features
{
    public class UpdateProductPrice
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<UpdatePriceRequest> _validator;

        public UpdateProductPrice(IUnitOfWork repository, IValidator<UpdatePriceRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<ProductDto>> Execute(int id, UpdatePriceRequest request) 
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e=>e.ErrorMessage).ToList();
                return Result<ProductDto>.Failure(errors);
            }
            var product = await _repository.Products.Get(p=>p.Id == id, p=>p.Category);

            if (product is null)
                return Result<ProductDto>.Failure($"product with id {id} does not exist");

            var newPrice = new ProductPrice()
            {
                UnitPrice = request.Price,

            };
            product.Prices.Add(newPrice);

            await _repository.SaveChangesAsync();

            var dto = product.ToDto();
            return Result<ProductDto>.Succes(dto);

        }
    }
}
