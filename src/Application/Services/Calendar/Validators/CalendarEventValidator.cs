using Application.Services.Calendar.Features;
using Application.Services.Calendar.Models.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Calendar.Validators
{
    public class CalendarEventValidator : AbstractValidator<CreateCalendarEventsRequest>
    {
        public CalendarEventValidator()
        {
            RuleFor(a => a.AdminId).GreaterThan(0);
            RuleFor(a => a.Title).MaximumLength(100);
            RuleFor(a=>a.Description).MaximumLength(200);
            RuleFor(a => a.Type).IsInEnum();
        }
    }
}
