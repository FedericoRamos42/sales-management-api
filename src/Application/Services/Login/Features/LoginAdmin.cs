using Application.Services.Login.Interfaces;
using Application.Services.Login.Models.Request;
using Application.Services.Login.Models.Response;
using Application.Utils.Result;
using Domain.Enitites;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Login.Features
{
    public class LoginAdmin
    {
        private readonly IAuthRepository _repository;
        private readonly IPasswordHasher _hasher;
        private readonly IAuthService _authService;
        
        
        public LoginAdmin(IAuthRepository repository, IPasswordHasher hasher,IAuthService authService)
        {
           _repository = repository; 
           _hasher = hasher;
           _authService = authService;
        }

        public async Task<Result<LoginResponse>> Execute(LoginRequest request)
        {
            var admin = await _repository.Get(x => x.Email == request.Email);

            if (admin is null)
                return Result<LoginResponse>.Failure("invalid email");

            if (!_hasher.Verify(request.Password, admin.Password))
                return Result<LoginResponse>.Failure("invalid password");

            string token = _authService.CreateToken(admin);

            var refreshToken = new RefreshToken()
            {
                AdminId = admin.Id,
                Token = _authService.CreateRefreshToken(),
                ExpiresOnUtc = DateTime.UtcNow.AddDays(7),
            };

            await _repository.AddRefreshToken(refreshToken);
            return Result<LoginResponse>.Succes(new LoginResponse(token, refreshToken.Token));
        }
    }
}
