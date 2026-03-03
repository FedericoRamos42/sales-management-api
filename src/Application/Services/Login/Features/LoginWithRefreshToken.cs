using Application.Result;
using Application.Services.Login.Interfaces;
using Application.Services.Login.Models.Request;
using Application.Services.Login.Models.Response;
using Domain.Enitites;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Login.Features
{
    public class LoginWithRefreshToken
    {
        private readonly IAuthService _authService;
        private readonly IAuthRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        
        public LoginWithRefreshToken(IAuthService authService,IAuthRepository repository,IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<LoginResponse>> Execute(RefreshTokenRequest request)
        {
            var token = await _repository.GetRefreshToken(request.RefreshToken);

            if (token is null || token.ExpiresOnUtc < DateTime.UtcNow)
            {
                return Result<LoginResponse>.Failure($"refresh token does not exist or has expired");
            }

            var accesToken = _authService.CreateToken(token.Admin);
            

            token.Token = _authService.CreateRefreshToken();
            token.ExpiresOnUtc = DateTime.UtcNow.AddDays(7);

            await _unitOfWork.SaveChangesAsync();

            return Result<LoginResponse>.Succes(new LoginResponse(accesToken, token.Token));

        }
    }
}
