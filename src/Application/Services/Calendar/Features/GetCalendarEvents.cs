using Application.Result;
using Application.Services.Calendar.Mappers;
using Application.Services.Calendar.Models;
using Application.Services.Calendar.Models.Request;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Calendar.Features
{
    public class GetCalendarEvents
    {
        private readonly IUnitOfWork _repository;
        public GetCalendarEvents(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<CalendarEventDto>>> Execute(int adminId, GetCalendarEventsRequest request)
        {
            var admin = await _repository.Auths.Get(a=> a.Id == adminId);

            if(admin is null)
            {
                return Result<List<CalendarEventDto>>.Failure($"admin with id {adminId} does not exist");
            }

            var events = await _repository.CalendarEvents.Search(a => a.AdminId == adminId);

            return Result<List<CalendarEventDto>>.Succes(events.ToListDto());

        }
    }
}
