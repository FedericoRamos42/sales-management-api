using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Customers
{
    public class AccountMovementDTO
    {
        public string Date { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal BalanceAfter { get; set; }

    }
}
