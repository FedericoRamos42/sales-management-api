using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Calendar.Models.Request
{
    public record CreateCalendarEventsRequest(
        
        int AdminId,
        string Title,
        string? Description,
        DateTime StartDate,
        DateTime? EndDate,
        EventType Type
        );
   
}
