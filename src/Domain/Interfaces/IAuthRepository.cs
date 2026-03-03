using Domain.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuthRepository : IBaseRepository<Admin>
    {
        Task AddRefreshToken(RefreshToken token);
        Task<RefreshToken?> GetRefreshToken(string refreshToken);
    }
}
