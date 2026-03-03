using Application.Services.Login.Models;
using Domain.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Login.Interfaces
{
    public interface IAuthService
    {
        string CreateToken(Admin user);
        string CreateRefreshToken();
    }
}
