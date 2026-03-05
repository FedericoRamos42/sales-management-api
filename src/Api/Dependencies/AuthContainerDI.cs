using Application.Services.Login;
using Application.Services.Login.Features;
using Application.Services.Login.Interfaces;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services.Authentication;

namespace Api.Dependencies
{
    public static class AuthContainerDi
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IAuthRepository,AuthRepository>();
            services.AddScoped<IAuthService,AuthenticationService>();
            services.AddScoped<LoginWithRefreshToken>();
            services.AddScoped<LoginAdmin>();
            services.AddScoped<IUserContext,UserContext>(); 
            services.AddScoped<LoginUseCases>();
        }
    }
}
