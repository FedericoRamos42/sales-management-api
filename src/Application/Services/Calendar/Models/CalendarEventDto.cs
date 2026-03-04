using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Calendar.Models
{
   public record CalendarEventDto(
       int Id,
       int UserId,
       string Title,
       string? Description,
       string StartDate,
       string? EndDate,
       string Type
       );
}
