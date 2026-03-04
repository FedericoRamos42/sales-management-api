using Domain.Abstraction;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enitites
{
    public class CalendarEvent : BaseEntity
    {
        public int AdminId { get; set; }
        public Admin Admin { get; set; } = default!;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } 
        public DateTime? EndDate { get; set; }
        public EventType Type { get; set; }

    }
}
