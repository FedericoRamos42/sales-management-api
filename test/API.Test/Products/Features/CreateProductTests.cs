using Application.Services.Producto.Features;
using Application.Services.Products.Models.Request;
using Domain.Enitites;
using Domain.Interfaces;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Linq.Expressions;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace API.Test.Products.Features
{
    public class CreateProductTests
    {
        private readonly Mock<IValidator<CreateProductRequest>> _validatorMock;
        private readonly Mock<IUnitOfWork> _repositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly CreateProduct _useCase;
        public CreateProductTests()
        {
            _validatorMock = new Mock<IValidator<CreateProductRequest>>();
            _repositoryMock = new Mock<IUnitOfWork>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();

            _repositoryMock.SetupGet(r => r.Products).Returns(_productRepositoryMock.Object);
            _repositoryMock.SetupGet(r => r.Categories).Returns(_categoryRepositoryMock.Object);
            _repositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
            _productRepositoryMock.Setup(r => r.Create(It.IsAny<Product>())).Returns(Task.CompletedTask);

            _useCase = new CreateProduct(
                _repositoryMock.Object,
                _validatorMock.Object
            );
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenValidationFails()
        {
            var request = new CreateProductRequest();
            var validationResult = new ValidationResult(
                new List<ValidationFailure>
                {
                    new ValidationFailure("Name","Error")
                }   
            );

            _validatorMock
                .Setup(v=>v.ValidateAsync(request,default))
                .ReturnsAsync(validationResult);

            var result = await _useCase.Execute(request);

            result.IsSucces.Should().BeFalse();

            _repositoryMock.Verify(
            r => r.Products.Create(It.IsAny<Product>()),
            Times.Never);

            _repositoryMock.Verify(
            r => r.SaveChangesAsync(),
            Times.Never);
        }


        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenCategoryDoesNotExist()
        {
            var request = ValidRequest();

            _validatorMock
                .Setup(v => v.ValidateAsync(request, default))
                .ReturnsAsync(new ValidationResult());

            _categoryRepositoryMock
                .Setup(v => v.Get(
                    It.IsAny<Expression<Func<Category, bool>>>(),
                    It.IsAny<Expression<Func<Category, object>>[]>()
                    ))
                .ReturnsAsync(() => null);

            var result = await _useCase.Execute(request);

            result.IsSucces.Should().BeFalse();
            _productRepositoryMock.Verify(
                r => r.Create(It.IsAny<Product>()),
                Times.Never);

            _repositoryMock.Verify(
                r => r.SaveChangesAsync(),
                Times.Never);
        }


        [Fact]
        public async Task Execute_ShouldCreateProduct_WhenDataIsValid()
        {
            var request = ValidRequest();

            _validatorMock
                .Setup(v => v.ValidateAsync(request, default))
                .ReturnsAsync(new ValidationResult());

            _categoryRepositoryMock
                .Setup(r => r.Get(
                    It.IsAny<Expression<Func<Category, bool>>>(),
                    It.IsAny<Expression<Func<Category, object>>[]>()))
                .ReturnsAsync(new Category { Id = 1 , Name = "Federico Ramos"});



            var result = await _useCase.Execute(request);

            result.IsSucces.Should().BeTrue();

            _productRepositoryMock.Verify(
                r => r.Create(It.Is<Product>(p =>
                    p.Name == request.Name &&
                    p.Stock == request.Stock &&
                    p.CategoryId == request.CategoryId &&
                    p.Description == request.Description &&
                    p.Prices.First().UnitPrice == request.Price
                )),
                Times.Once);

            _repositoryMock.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);

            result.Value.Should().NotBeNull();
        }
        

        private CreateProductRequest ValidRequest()
        {
            return new CreateProductRequest
            {
                Name = "Mouse",
                Price = 100,
                Stock = 5,
                CategoryId = 1,
                Description = "Gaming mouse"
            };
        }
    }
}
