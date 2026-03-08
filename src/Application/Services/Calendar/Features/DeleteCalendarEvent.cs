using Application.Result;
using Application.Services.Calendar.Mappers;
using Application.Services.Calendar.Models;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Calendar.Features
{
    public class DeleteCalendarEvent
    {
        private readonly IUnitOfWork _repository;
        public DeleteCalendarEvent(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<CalendarEventDto>> Execute(int id)
        {
            var calendarEvent = await _repository.CalendarEvents.Get(c=>c.Id == id);
            if(calendarEvent is null)
            {
                return Result<CalendarEventDto>.Failure($"event with id {id} does not exist");
            }

            await _repository.CalendarEvents.Delete(calendarEvent);
            await _repository.SaveChangesAsync();

            return Result<CalendarEventDto>.Succes(calendarEvent.ToDto());
        }
    }
}
