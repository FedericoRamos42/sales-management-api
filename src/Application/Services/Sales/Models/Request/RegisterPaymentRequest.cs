using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Sales.Models.Request
{
    public class RegisterPaymentRequest
    {
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
