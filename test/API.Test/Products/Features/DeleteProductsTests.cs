using Application.Services.Producto.Features;
using Domain.Enitites;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace API.Test.Products.Features
{
    public class DeleteProductsTests
    {
        private readonly Mock<IUnitOfWork> _repositoryMock;
        private readonly DeleteProduct _useCases;
        public DeleteProductsTests()
        {
            _repositoryMock = new Mock<IUnitOfWork>();
            _useCases = new DeleteProduct(_repositoryMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenProductDoesNotExist()
        {
            _repositoryMock.Setup(r => r.Products.Get(
                            It.IsAny<Expression<Func<Product, bool>>>(),
                            It.IsAny<Expression<Func<Product, object>>[]>()
                            ))
                            .ReturnsAsync((Product?)null);

            var response = await _useCases.Execute(1);

            response.IsSucces.Should().BeFalse();

            _repositoryMock.Verify(r=>r.Products.Delete(It.IsAny<Product>()), Times.Never);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);

        }
        [Fact]
        public async Task Execute_ShouldDeleteProductAndReturnDto_WhenDataIsValid()
        {
            _repositoryMock.Setup(r => r.Products.Get(
                          It.IsAny<Expression<Func<Product, bool>>>(),
                          It.IsAny<Expression<Func<Product, object>>[]>()
                          ))
                          .ReturnsAsync(new Product() { Category = new Category()});

            var response = await _useCases.Execute(1);
            response.IsSucces.Should().BeTrue();
            response.Value.Should().NotBeNull();
            _repositoryMock.Verify(r => r.Products.Delete(It.IsAny<Product>()), Times.Once);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
