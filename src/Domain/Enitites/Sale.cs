using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstraction;
using Domain.Enums;

namespace Domain.Enitites
{
    public class Sale : BaseEntity
    {
        public int CustomerId { get; set; }
        public decimal PaidAmount { get; private set; }
        public Customer Customer { get; set; } = default!;
        public decimal TotalAmount { get; set; }
        public SaleStatus Status { get; set; }
        public ICollection<SaleDetail> Items { get; set; } = new List<SaleDetail>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public decimal CalculateTotal()
        {
            return TotalAmount = Items.Sum(d => d.Total);
        }

        public void RegisterPayment(decimal amount,PaymentMethod method)
        {
            PaidAmount += amount;
            UpdateStatus();
            Payments.Add(new Payment (amount, method));
        }

        public void UpdateStatus()
        {
            if(PaidAmount == 0)
            {
                Status = SaleStatus.OnCredit;
            }
            else if (PaidAmount < TotalAmount)
            {
                Status = SaleStatus.PartialPaid;
            }
            else
            {
                Status = SaleStatus.Paid;
            }
        }
    }
}
