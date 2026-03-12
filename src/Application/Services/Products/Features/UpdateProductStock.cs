using Application.Services.Producto.Mappers;
using Application.Services.Products.Models;
using Application.Services.Products.Models.Request;
using Application.Utils.Result;
using Domain.Interfaces;
using FluentValidation;
using System.ComponentModel.DataAnnotations;


namespace Application.Services.Producto.Features
{
    public class UpdateProductStock
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<UpdateStockRequest> _validator;

        public UpdateProductStock(IUnitOfWork repository, IValidator<UpdateStockRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<ProductDto>> Execute(int id, UpdateStockRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<ProductDto>.Failure(errors);
            }

            var product = await _repository.Products.Get(p => p.Id == id, p=> p.Category);

            if (product is null)
                return Result<ProductDto>.Failure($"product with id {id} does not exist");

            product.Stock = request.Stock;

            await _repository.SaveChangesAsync();
            var dto = product.ToDto();

            return Result<ProductDto>.Succes(dto);
        }
    }
}
