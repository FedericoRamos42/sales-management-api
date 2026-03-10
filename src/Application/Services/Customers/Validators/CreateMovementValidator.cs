using Application.Services.Customers.Models.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Customers.Validators
{
    public class CreateMovementValidator : AbstractValidator<CreateMovementRequest>
    {
        public CreateMovementValidator()
        {
            RuleFor(c=>c.Amount).NotEmpty();
            RuleFor(c=>c.Description).NotEmpty().MaximumLength(250);
        }
    }
}
