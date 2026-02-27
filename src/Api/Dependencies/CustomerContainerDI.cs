using Application.Services.Categories;
using Application.Services.Categories.Features;
using Application.Services.Customers;
using Application.Services.Customers.Features;
using Application.Services.Sales;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Api.Dependencies
{
    public static class CustomerContainerDI
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<CreateCustomer>();
            services.AddScoped<UpdateCustomer>();
            services.AddScoped<DeleteCustomer>();
            services.AddScoped<SearchCustomer>();
            services.AddScoped<GetCustomer>();
            services.AddScoped<GetAllCustomer>();
            services.AddScoped<GetCustomerAccount>();
            services.AddScoped<CustomerUseCases>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICustomerRepository,CustomerRepository>();
        }
    }
}
