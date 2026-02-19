using Application.Services.Producto.Features;
using Domain.Enitites;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.Test.Products.Features
{
    public class GetProductTests
    {
        private readonly Mock<IUnitOfWork> _repositoryMock;
        private readonly GetProduct _useCase;

        public GetProductTests()
        {
            _repositoryMock = new Mock<IUnitOfWork>();
            _useCase = new GetProduct(_repositoryMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenProductDoesNotExist()
        {
            _repositoryMock
               .Setup(r => r.Products.Get(
                   It.IsAny<Expression<Func<Product, bool>>>(),
                   It.IsAny<Expression<Func<Product, object>>[]>()))
               .ReturnsAsync(() => null);

            var result = await _useCase.Execute(1);

            result.IsSucces.Should().BeFalse();
        }
        [Fact]
        public async Task Execute_ShouldReturnProductDto_WhenProductExists()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Description = "Test Description",
                Stock = 5,
                CategoryId = 1,
                Category = new Category { Id = 1, Name = "Test Category" }
            };
            _repositoryMock
               .Setup(r => r.Products.Get(
                   It.IsAny<Expression<Func<Product, bool>>>(),
                   It.IsAny<Expression<Func<Product, object>>[]>()))
               .ReturnsAsync(product);
            var result = await _useCase.Execute(1);
            result.IsSucces.Should().BeTrue();
            result.Value.Should().NotBeNull();

        }        }
}
