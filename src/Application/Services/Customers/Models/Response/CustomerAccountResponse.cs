using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Customers.Models.Response
{
    public class CustomerAccountResponse
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public List<AccountMovementDTO> Movement { get; set; } = new List<AccountMovementDTO>();
    }
}
