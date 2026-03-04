using Application.Services.Calendar.Models;
using Domain.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Calendar.Mappers
{
    public static class CalendarEventMapper
    {
        public static CalendarEventDto ToDto(this CalendarEvent calendarEvent) => 
            new CalendarEventDto(
                calendarEvent.Id,
                calendarEvent.AdminId, 
                calendarEvent.Title,
                calendarEvent.Description,
                calendarEvent.StartDate.ToString(),
                calendarEvent.EndDate.ToString(),
                calendarEvent.Type.ToString()
            );

        public static List<CalendarEventDto> ToListDto(this List<CalendarEvent> calendarEvents) => calendarEvents.Select(e => e.ToDto()).ToList();
    }
}
