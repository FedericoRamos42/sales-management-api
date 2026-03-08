using Application.Services.Calendar.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Calendar
{
    public record CalendarUseCases(
        GetCalendarEvents GetCalendar,
        CreateCalendarEvent CreateCalendar,
        GetCalendarEvent GetEvent,
        DeleteCalendarEvent DeleteEvent
        );
}
