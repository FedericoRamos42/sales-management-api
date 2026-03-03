using Domain.Enitites;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthRepository : BaseRepository<Admin>, IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        public AuthRepository( ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddRefreshToken(RefreshToken token)
        {
            await _context.Set<RefreshToken>().AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public Task<RefreshToken?> GetRefreshToken(string refreshToken)
        {
            return _context.Set<RefreshToken>().Include(r=>r.Admin).FirstOrDefaultAsync(r=>r.Token == refreshToken);
        }
    }
}
