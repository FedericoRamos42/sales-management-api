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
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }

    }
}
