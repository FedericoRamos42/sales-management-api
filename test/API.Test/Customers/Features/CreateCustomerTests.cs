using Application.Services.Customers.Features;
using Application.Services.Customers.Models.Request;
using Domain.Enitites;
using Domain.Interfaces;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace API.Test.Customers.Features
{
    public class CreateCustomerTests
    {
        private readonly Mock<IValidator<CustomerForRequest>> _mockValidator;
        private readonly Mock<IUnitOfWork> _mockRepository;
        private readonly Mock<ICustomerRepository> _customerRepository;
        private readonly CreateCustomer _useCase;
        public CreateCustomerTests()
        {
            _mockValidator = new Mock<IValidator<CustomerForRequest>>();
            _mockRepository = new Mock<IUnitOfWork>();
            _customerRepository = new Mock<ICustomerRepository>();
            _mockRepository
                .SetupGet(r=>r.Customers)
                .Returns(_customerRepository.Object);
             _mockRepository
                .Setup(r=>r.SaveChangesAsync())
                .Returns(Task.CompletedTask);
            _useCase = new CreateCustomer(
               _mockRepository.Object,
               _mockValidator.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccesAndValue_WhenDataIsValid()
        {
            var request = new CustomerForRequest()
            {
                Name = "Federico Ramos",
                Email = "FedericoRamos@gmail.com",
                PhoneNumber = "1234567890",
                Address = "Address"
            };

            _mockValidator
                .Setup(c => c.ValidateAsync(request, default))
                .ReturnsAsync(new ValidationResult());

            _customerRepository
                .Setup(r => r.Create(It.IsAny<Customer>()))
                .Returns(Task.CompletedTask);

            var result = await _useCase.Execute(request);

            _customerRepository.Verify(
                r => r.Create(It.IsAny<Customer>()),
                Times.Once);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);

            result.Value.Should().NotBeNull();
            result.IsSucces.Should().BeTrue();

        }

        [Fact]

        public async Task Execute_ShouldReturnFailure_WhenDataIsInvalid()
        {
            var request = new CustomerForRequest();
            var errors = new List<ValidationFailure>()
            {
                new ValidationFailure("Name", "Name is Required")
            };

            _mockValidator
                .Setup(c=>c.ValidateAsync(request,default))
                .ReturnsAsync(new ValidationResult(errors));

            var result = await _useCase.Execute(request);

            result.IsSucces.Should().BeFalse();

            _mockRepository.Verify(c=>c.Customers.Create(It.IsAny<Customer>()),Times.Never);
            _mockRepository.Verify(c=>c.SaveChangesAsync(),Times.Never);
        }


    }
}
