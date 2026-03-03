using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Login.Models.Response
{
    public sealed record LoginResponse(string AccesToken, string RefreshToken);
}
