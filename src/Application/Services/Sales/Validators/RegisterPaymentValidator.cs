using Application.Services.Sales.Models.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Sales.Validators
{
    public class RegisterPaymentValidator : AbstractValidator<RegisterPaymentRequest>
    {
        public RegisterPaymentValidator()
        {

            RuleFor(p => p.PaymentMethod)
                .NotNull()
                .WithMessage("Payment method is required.");

            RuleFor(p=>p.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0");
        }
    }
}
