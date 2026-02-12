using Application.Services.Categories;
using Application.Services.Categories.Features;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Api.Dependencies
{
    public static class CategoryContainerDI
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<CreateCategories>();
            services.AddScoped<GetAllCategories>();
            services.AddScoped<UpdateCategories>();
            services.AddScoped<CategoryUseCases>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }
    }
}
