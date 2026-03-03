using Domain.Abstraction;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enitites
{
    public class Payment : BaseEntity
    {
        public int SaleId { get; set; }
        public Sale Sale { get; set; } = default!;
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }

        public Payment()
        {
            
        }
        public Payment(decimal amount,PaymentMethod method)
        {
            Amount = amount;
            Method = method;
        }

    }
}
