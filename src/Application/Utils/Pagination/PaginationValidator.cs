using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils.Pagination
{
    public class PaginationValidator : AbstractValidator<PaginationParams>
    {
        public PaginationValidator()
        {
            RuleFor(p => p.PageIndex).GreaterThan(0);
            RuleFor(p=> p.PageSize).GreaterThan(0).LessThan(50);

        }
    }
}
