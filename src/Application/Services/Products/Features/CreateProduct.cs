using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Result;
using Application.Services.Producto.Mappers;
using Application.Services.Products.Models;
using Application.Services.Products.Models.Request;
using Domain.Enitites;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Services.Producto.Features
{
    public class CreateProduct
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateProductRequest> _validator;
        public CreateProduct(IUnitOfWork repository,IValidator<CreateProductRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<Result<ProductDto>> Execute(CreateProductRequest request) 
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<ProductDto>.Failure(errors);
            }
            var category = await _repository.Categories.Get(c=>c.Id == request.CategoryId);

            if(category is null)
            {
                return Result<ProductDto>.Failure($"category with id {request.CategoryId} does not exist");
            }

            var price = new ProductPrice()
            {
                UnitPrice = request.Price,
            };
            var prices = new List<ProductPrice>() { price };
            var product = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                CategoryId = request.CategoryId,
                Stock = request.Stock,
                Prices = prices,
                Category = category,
            };

            await _repository.Products.Create(product);
            await _repository.SaveChangesAsync();

            var dto = product.ToDto();
            return Result<ProductDto>.Succes(dto);

        }
    }
}
