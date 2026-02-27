using Application.Services.Customers.Features;
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

namespace API.Test.Customers.Features
{
    public class DeleteCustomerTests
    {
        private readonly Mock<IUnitOfWork> _mockRepository;
        private readonly DeleteCustomer _useCase;

        public DeleteCustomerTests()
        {
            _mockRepository = new Mock<IUnitOfWork>();
            _useCase = new DeleteCustomer(_mockRepository.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenCustomerDoesNotExist()
        {
            _mockRepository
                .Setup(c => c.Customers.Get(
                   It.IsAny<Expression<Func<Customer, bool>>>(),
                   It.IsAny<Expression<Func<Customer, object>>[]>()))
                .ReturnsAsync(()=>null);

            var result = await _useCase.Execute(1);

            result.IsSucces.Should().BeFalse();

            _mockRepository.Verify(c=>c.SaveChangesAsync(), Times.Never());
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess_WhenDataIsValid()
        {
            _mockRepository
                .Setup(c => c.Customers.Get(
                   It.IsAny<Expression<Func<Customer, bool>>>(),
                   It.IsAny<Expression<Func<Customer, object>>[]>()))
                .ReturnsAsync(new Customer());

            var result = await _useCase.Execute(1);

            result.IsSucces.Should().BeTrue();
            result.Value.Should().NotBeNull();

            _mockRepository.Verify(c => c.Customers.Delete(It.IsAny<Customer>()),Times.Once);
            _mockRepository.Verify(c => c.SaveChangesAsync(), Times.Once());
        }

    }
}
