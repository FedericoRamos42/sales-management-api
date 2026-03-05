using Application.Services.Calendar;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Api.Dependencies
{
    public class CalendarEventsDI
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ICalendarEventRepository, CalendarEventRepository>();
            services.AddScoped<CalendarUseCases>();
        }
    }
}
