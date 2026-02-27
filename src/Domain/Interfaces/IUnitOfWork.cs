using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        ISaleRepository Sales { get; }
        ICustomerRepository Customers { get; }
        ICategoryRepository Categories { get; }
        IDashboardRepository Dashboard { get; }
        IAccountRepository Accounts { get; }
        Task SaveChangesAsync();
    }
}
