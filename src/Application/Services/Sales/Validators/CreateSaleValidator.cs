using Application.Services.Sales.Models.Request;
using FluentValidation;

namespace Application.Services.Sales.Validators
{
    public class CreateSaleValidator: AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleValidator()
        {
            RuleFor(x => x.CustomerId)
           .GreaterThan(0)
           .WithMessage("CustomerId must be a valid value");

            RuleFor(x => x.Method)
            .NotNull()
            .When(x => x.InitialPaymentAmount > 0)
            .WithMessage("Payment method is required when there is an initial payment.");

            RuleFor(x => x.Details)
                .NotNull()
                .WithMessage("Sale must have items")
                .NotEmpty()
                .WithMessage("Sale must contain at least one item");
            
            RuleFor(x => x.InitialPaymentAmount)
                .GreaterThanOrEqualTo(0);

            RuleForEach(x => x.Details)
                .SetValidator(new CreateSaleDetailValidator());
        }
    }
}
