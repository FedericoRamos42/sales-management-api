using Application.Services.Calendar;
using Application.Services.Calendar.Features;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Api.Dependencies
{
    public class CalendarEventsDI
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ICalendarEventRepository, CalendarEventRepository>();
            services.AddScoped<GetCalendarEvent>();
            services.AddScoped<CreateCalendarEvent>();
            services.AddScoped<CalendarUseCases>();
        }
    }
}
