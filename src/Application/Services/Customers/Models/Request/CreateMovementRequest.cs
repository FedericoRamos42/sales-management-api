
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Customers.Models.Request
{
    public record CreateMovementRequest(
        decimal Amount,
        string Description
        );
}
