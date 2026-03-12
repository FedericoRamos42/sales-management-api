using Application.Services.Sales.Mappers;
using Application.Services.Sales.Models;
using Application.Services.Sales.Models.Request;
using Application.Utils.Result;
using Domain.Enitites;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Services.Sales.Features
{
    public class CreateSale
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateSaleRequest> _validator;

        public CreateSale(IUnitOfWork repository, IValidator<CreateSaleRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<SaleDto>> Execute(CreateSaleRequest request)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<SaleDto>.Failure(errors);
            }

            var customer = await _repository.Customers.Get(c => c.Id == request.CustomerId);

            if (customer is null)
                return Result<SaleDto>.Failure($"customer with id {request.CustomerId} does not exist");

            var details = new List<SaleDetail>();

            foreach (var detail in request.Details)
            {
                Product product = await _repository.Products.Get(p => p.Id == detail.ProductId, p => p.Prices);

                if (product is null)
                    return Result<SaleDto>.Failure($"product with id {detail.ProductId} does not exist");

                if (product.Stock < detail.Quantity)
                    return Result<SaleDto>.Failure(
                        $"Insufficient stock for product {product.Name}"
                    );

                var item = new SaleDetail()
                {
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                    UnitPrice = product.GetActualPrice(),
                };
                item.CalculateTotal();
                details.Add(item);

                product.Stock -= detail.Quantity;
            }

            Sale sale = new Sale()
            {
                CustomerId = request.CustomerId,
                Items = details
            };

            sale.CalculateTotal();

            if(request.InitialPaymentAmount > sale.TotalAmount)
            {
                return Result<SaleDto>.Failure($"initial paymenth is invalid");
            }

            if(request.InitialPaymentAmount > 0)
            {
                sale.RegisterPayment(request.InitialPaymentAmount, request.Method!.Value);
            }


            await _repository.Sales.Create(sale);
            await _repository.SaveChangesAsync();

            var debt = sale.TotalAmount - sale.PaidAmount;

            if (debt > 0)
            {
                customer.Account.AddMovement(
                    -debt,
                    $"Debt from sale #{sale.Id}"
                    );
            }

            await _repository.SaveChangesAsync();

            var dto = sale.ToDto(customer);

            return Result<SaleDto>.Succes(dto);
        }
    }
}
