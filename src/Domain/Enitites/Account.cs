using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enitites
{
    public class Account : BaseEntity
    {
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public Customer Customer { get; set; } = default!;
        public ICollection<AccountMovement> Movements { get; set; } = new List<AccountMovement>();

        public void AddMovement(decimal amount,string description)
        {
            Balance += amount;

            Movements.Add(new AccountMovement(
                Id,
                amount,
                Balance,
                description
            ));
        }

    }
}
