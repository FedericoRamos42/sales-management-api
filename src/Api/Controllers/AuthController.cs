using Application.Services.Login;
using Application.Services.Login.Features;
using Application.Services.Login.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LoginUseCases _services;

        public AuthController(LoginUseCases services)
        {
            _services = services;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result =  await _services.LoginAdmin.Execute(request);

            if(!result.IsSucces)
                return Unauthorized(new {errors = result.Errors});

            return Ok(new {token = result.Value});
        }

        [HttpPost("login/refresh-token")]
        public async Task<IActionResult> Login(RefreshTokenRequest request)
        {
            var result = await _services.LoginWithRefreshToken.Execute(request);

            if (!result.IsSucces)
                return Unauthorized(new { errors = result.Errors });

            return Ok(result.Value);
        }
    }
}
