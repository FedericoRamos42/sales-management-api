using Application.Services.Sales;
using Application.Services.Sales.Features;
using Application.Services.Sales.Models.Request;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Api.Dependencies
{
    public class SaleContainerDi
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ISaleRepository,SaleRepository>();
            services.AddScoped<CreateSale>();
            services.AddScoped<GetAllSales>();
            services.AddScoped<GetSale>();
            services.AddScoped<ExportSalesToExcel>();
            services.AddScoped<RegisterPaymentRequest>();
            services.AddScoped<CancelSale>();
            services.AddScoped<SaleUseCases>();

        }
    }
}
