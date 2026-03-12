using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Producto;
using Application.Services.Producto.Features;
using Application.Services.Products.Features;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Dependencies
{
    public static class ProductContainerDI
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<CreateProduct>();
            services.AddScoped<UpdateProductStock>();
            services.AddScoped<DeleteProduct>();
            services.AddScoped<GetProducts>();
            services.AddScoped<ProductUseCases>();
            services.AddScoped<GetProduct>();
            services.AddScoped<UpdateProductPrice>();
            services.AddScoped<IProductRepository, ProductRepository>(); 
        }
    }
}
