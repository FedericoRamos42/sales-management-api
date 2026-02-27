using Application.Services.Customers;
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
    public class GetCustomerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly GetCustomer _useCase;
        public GetCustomerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _useCase = new GetCustomer(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenCustomerDoesNotExist()
        {
            _mockUnitOfWork
                .Setup(c => c.Customers.Get(
                    It.IsAny<Expression<Func<Customer, bool>>>(),
                    It.IsAny<Expression<Func<Customer, object>>[]>()))
                .ReturnsAsync(() => null);

            var result = await _useCase.Execute(1);

            result.IsSucces.Should().BeFalse();
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccesAndDto_WhenCustomerDataIsValid() 
        {
            _mockUnitOfWork
               .Setup(c => c.Customers.Get(
                   It.IsAny<Expression<Func<Customer, bool>>>(),
                   It.IsAny<Expression<Func<Customer, object>>[]>()))
               .ReturnsAsync(new Customer());

            var result = await _useCase.Execute(1);

            result.IsSucces.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }
    }
}
