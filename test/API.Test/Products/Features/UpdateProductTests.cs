using Application.Services.Producto.Features;
using Application.Services.Products.Features;
using Application.Services.Products.Models.Request;
using Domain.Enitites;
using Domain.Interfaces;
using FluentAssertions;
using FluentValidation;
using Moq;
using System.Linq.Expressions;

namespace API.Test.Products.Features
{
    public class UpdateProductTests
    {
        private readonly Mock<IValidator<UpdatePriceRequest>> _validatorPriceMock;
        private readonly Mock<IValidator<UpdateStockRequest>> _validatorStockMock;
        private readonly Mock<IUnitOfWork> _repositoryMock;
        private readonly UpdateProductPrice _useCasePrice;
        private readonly UpdateProductStock _useCaseStock;
        public UpdateProductTests()
        {
            _repositoryMock = new Mock<IUnitOfWork>();
            _validatorPriceMock = new Mock<IValidator<UpdatePriceRequest>>();
            _validatorStockMock = new Mock<IValidator<UpdateStockRequest>>();
            _useCasePrice = new UpdateProductPrice(_repositoryMock.Object, _validatorPriceMock.Object);
            _useCaseStock = new UpdateProductStock(_repositoryMock.Object, _validatorStockMock.Object);
        }

        [Fact]
        public async Task UpdateProductPrice_ShouldReturnFailure_WhenValidationFails()
        {
            var request = new UpdatePriceRequest() { Price = -10 };
            var validationResult = new FluentValidation.Results.ValidationResult(
                new List<FluentValidation.Results.ValidationFailure>
                {
                    new FluentValidation.Results.ValidationFailure("Price","Price must be greater than 0")
                }
            );

            _validatorPriceMock
                .Setup(v => v.ValidateAsync(request, default))
                .ReturnsAsync(validationResult);
            var result = await _useCasePrice.Execute(1, request);

            result.IsSucces.Should().BeFalse();

            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never());
        }

        [Fact]
        public async Task UpdateProductStock_ShouldReturnFailure_WhenValidationFails()
        {
            var request = new UpdateStockRequest() { Stock = -5 };
            var validationResult = new FluentValidation.Results.ValidationResult(
                new List<FluentValidation.Results.ValidationFailure>
                {
                    new FluentValidation.Results.ValidationFailure("Stock","Stock must be greater than or equal to 0")
                }
            );
            _validatorStockMock
                .Setup(v => v.ValidateAsync(request, default))
                .ReturnsAsync(validationResult);
            var result = await _useCaseStock.Execute(1, request);
            result.IsSucces.Should().BeFalse();
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never());

        }

        [Fact]
        public async Task UpdateProductPrice_ShouldReturnFailure_WhenProductDoesNotExist()
        {
            var request = new UpdatePriceRequest() { Price = 100 };
            _validatorPriceMock
                .Setup(v => v.ValidateAsync(request, default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _repositoryMock.Setup(r => r.Products.Get(
                It.IsAny<Expression<Func<Product, bool>>>(),
                It.IsAny<Expression<Func<Product, object>>[]>()
                )).ReturnsAsync((Product?)null);
            var result = await _useCasePrice.Execute(1, request);
            result.IsSucces.Should().BeFalse();
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never());
        }

        [Fact]
        public async Task UpdateProductStock_ShouldReturnFailure_WhenProductDoesNotExist()
        {
            var request = new UpdateStockRequest() { Stock = 50 };
            _validatorStockMock
                .Setup(v => v.ValidateAsync(request, default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _repositoryMock.Setup(r => r.Products.Get(
                It.IsAny<Expression<Func<Product, bool>>>(),
                It.IsAny<Expression<Func<Product, object>>[]>()
                )).ReturnsAsync((Product?)null);
            var result = await _useCaseStock.Execute(1, request);
            result.IsSucces.Should().BeFalse();
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never());
        }

        [Fact]
        public async Task UpdateProductPrice_ShouldUpdatePriceAndReturnDto_WhenDataIsValid()
        {
            var request = new UpdatePriceRequest() { Price = 150 };
            _validatorPriceMock
                .Setup(v => v.ValidateAsync(request, default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());
            var product = new Product() { Prices = new List<ProductPrice>() { new ProductPrice() { UnitPrice = 100 } } , Category = new Category()};
            _repositoryMock.Setup(r => r.Products.Get(
                It.IsAny<Expression<Func<Product, bool>>>(),
                It.IsAny<Expression<Func<Product, object>>[]>()
                )).ReturnsAsync(product);
            var result = await _useCasePrice.Execute(1, request);
            result.IsSucces.Should().BeTrue();
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task UpdateProductStock_ShouldUpdateStockAndReturnDto_WhenDataIsValid()
        {
            var request = new UpdateStockRequest() { Stock = 20 };
            _validatorStockMock
                .Setup(v => v.ValidateAsync(request, default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());
            var product = new Product() { Stock = 10 , Category = new Category()};
            _repositoryMock.Setup(r => r.Products.Get(
                It.IsAny<Expression<Func<Product, bool>>>(),
                It.IsAny<Expression<Func<Product, object>>[]>()
                )).ReturnsAsync(product);
            var result = await _useCaseStock.Execute(1, request);
            result.IsSucces.Should().BeTrue();
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once());
        }
    }
}