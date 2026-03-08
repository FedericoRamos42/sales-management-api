using Application.Result;
using Application.Services.Calendar.Mappers;
using Application.Services.Calendar.Models;
using Application.Services.Calendar.Models.Request;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Calendar.Features
{
    public class UpdateCalendarEvent
    {
        private readonly IValidator<CreateCalendarEventsRequest> _validator;
        private readonly IUnitOfWork _repository;
        public UpdateCalendarEvent(IUnitOfWork repository, IValidator<CreateCalendarEventsRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<CalendarEventDto>> Execute(int id, CreateCalendarEventsRequest request)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<CalendarEventDto>.Failure(errors);
            }
            var calendarEvent = await _repository.CalendarEvents.Get(c => c.Id == id);
            if (calendarEvent is null)
            {
                return Result<CalendarEventDto>.Failure($"Event with id {id} does not exist");
            }

            calendarEvent.Title = request.Title;
            calendarEvent.Description = request.Description;
            calendarEvent.StartDate = request.StartDate;
            calendarEvent.EndDate = request.EndDate;
            calendarEvent.Type = request.Type;

            await _repository.SaveChangesAsync();

            return Result<CalendarEventDto>.Succes(calendarEvent.ToDto());

        }
    }
}
