using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Login.Models.Request
{
    public sealed record RefreshTokenRequest(string RefreshToken);
}
