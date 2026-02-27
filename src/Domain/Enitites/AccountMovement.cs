using Domain.Abstraction;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enitites
{
    public class AccountMovement : BaseEntity
    {
        public int AccountId { get; private set; }
        public Account Account { get; private set; } = default!;
        public decimal Amount { get; private set; }
        public decimal BalanceAfter { get; private set; }
        public string Description { get; private set; } = string.Empty;

        public AccountMovement()
        {

        }

        public AccountMovement(int id,decimal amount,decimal balance, string description)
        {
            AccountId = id;
            Amount = amount;
            BalanceAfter = balance;
            Description = description;
        }
    }
}
