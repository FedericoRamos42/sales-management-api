using Application.Services.Producto.Mappers;
using Application.Services.Products.Models;
using Application.Utils.Result;
using Domain.Interfaces;

namespace Application.Services.Producto.Features
{
    public class DeleteProduct
    {
        private readonly IUnitOfWork _repository;

        public DeleteProduct(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductDto>> Execute(int id) 
        {
            var product = await _repository.Products.Get(p => p.Id == id, p=> p.Category);

            if (product is null)
                return Result<ProductDto>.Failure($"product with id {id} does not exist");

            await _repository.Products.Delete(product);
            await _repository.SaveChangesAsync();
            var dto = product.ToDto();
            return Result<ProductDto>.Succes(dto);
        }
    }
}
