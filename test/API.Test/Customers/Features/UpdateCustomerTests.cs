using Application.Services.Customers.Features;
using Application.Services.Customers.Models.Request;
using Domain.Enitites;
using Domain.Interfaces;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.Test.Customers.Features
{
    public class UpdateCustomerTests
    {
        private readonly Mock<IValidator<CustomerForRequest>> _mockValidator;
        private readonly Mock<IUnitOfWork> _mockRepository;
        private readonly UpdateCustomer _useCase;
        public UpdateCustomerTests()
        {
            _mockValidator = new Mock<IValidator<CustomerForRequest>>();
            _mockRepository = new Mock<IUnitOfWork>();
            _useCase = new UpdateCustomer(
               _mockRepository.Object,
               _mockValidator.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccesAndDto_WhenDataIsValid()
        {
            var request = new CustomerForRequest()
            {
                Name = "name",
                Email = "pepito@gmail.com",
                PhoneNumber = "1234567890",
                Address = "sarmiento"
            };
            int id = 1;

            _mockValidator
                .Setup(c => c.ValidateAsync(request, default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _mockRepository
                .Setup(c => c.Customers.Get(
                   It.IsAny<Expression<Func<Customer, bool>>>(),
                   It.IsAny<Expression<Func<Customer, object>>[]>()))
                .ReturnsAsync(new Customer());

            var result = await _useCase.Execute(id, request);

            result.IsSucces.Should().BeTrue();
            result.Value.Should().NotBeNull();

            _mockRepository.Verify(c => c.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task Execute_ShouldReturnFaillure_WhenValidationFailed()
        {
            var errors = new List<ValidationFailure>()
            {
                new ValidationFailure("Name","Name is required")
            };

            var request = new CustomerForRequest();

            int id = 1;

            _mockValidator
                .Setup(c=>c.ValidateAsync(request,default))
                .ReturnsAsync(new ValidationResult(errors));

            _mockRepository
                .Setup(c => c.Customers.Get(
                   It.IsAny<Expression<Func<Customer, bool>>>(),
                   It.IsAny<Expression<Func<Customer, object>>[]>()))
                .ReturnsAsync(new Customer());

            var result = await _useCase.Execute(id, request);

            result.IsSucces.Should().BeFalse();

            _mockRepository.Verify(c=>c.SaveChangesAsync(), Times.Never());

        }
        [Fact]
        public async Task Execute_ShouldReturnFaillure_WhenCustomerDoesNotExist()
        {
            var request = new CustomerForRequest();
            int id = 1;

            _mockValidator
                .Setup(c => c.ValidateAsync(request, default))
                .ReturnsAsync(new ValidationResult());

            _mockRepository
                .Setup(c => c.Customers.Get(
                   It.IsAny<Expression<Func<Customer, bool>>>(),
                   It.IsAny<Expression<Func<Customer, object>>[]>()))
                .ReturnsAsync(() => null);

            var result = await _useCase.Execute(id, request);

            result.IsSucces.Should().BeFalse();

            _mockRepository.Verify(c => c.SaveChangesAsync(), Times.Never());

        }


    }
}
