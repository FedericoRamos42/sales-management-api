using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Customers.Features;

namespace Application.Services.Customers
{
    public record CustomerUseCases(
        CreateCustomer CreateCustomer,
        UpdateCustomer UpdateCustomer,
        GetCustomers GetPagination,
        SearchCustomer Search,
        GetCustomer Get,
        DeleteCustomer DeleteCustomer,
        GetCustomerAccount GetCustomerAccount,
        CreateAccountMovement CreateMovement
        );
}
