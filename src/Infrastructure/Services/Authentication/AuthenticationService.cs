using Application.Services.Login.Models;
using Microsoft.Extensions.Configuration;
using Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Application.Services.Login.Interfaces;
using Domain.Enitites;
using System.Buffers.Text;
using System.Security.Cryptography;

namespace Infrastructure.Services.Authentication
{
    public class AuthenticationService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _configuration;
        public AuthenticationService(IAuthRepository repository,IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        
        public string CreateToken(Admin user) 
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,"Admin")

            };

            var tokenKey = _configuration["AppSettings:Token"];

            if(string.IsNullOrWhiteSpace(tokenKey))
                throw new Exception("Token is not configured.");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(tokenKey)
                );


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["AppSettings:Issuer"],
                audience: _configuration["AppSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public string CreateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

    }
}
