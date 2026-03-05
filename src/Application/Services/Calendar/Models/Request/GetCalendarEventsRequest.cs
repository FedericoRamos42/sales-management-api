using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Calendar.Models.Request
{
   public record GetCalendarEventsRequest(
       DateTime DateStart,
       DateTime DateEnd
       );
}
