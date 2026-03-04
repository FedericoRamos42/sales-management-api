using Domain.Enitites;
using Domain.Interfaces;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CalendarEventRepository : BaseRepository<CalendarEvent>, ICalendarEventRepository
    {
        private readonly ApplicationDbContext _context;
        public CalendarEventRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
