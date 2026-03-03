using Application.Services.Login.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Login
{
    public record LoginUseCases(
        
        LoginAdmin LoginAdmin,
        LoginWithRefreshToken LoginWithRefreshToken
        
        );
}
