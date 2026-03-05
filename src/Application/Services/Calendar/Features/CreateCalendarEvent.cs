using Application.Result;
using Application.Services.Calendar.Mappers;
using Application.Services.Calendar.Models;
using Application.Services.Calendar.Models.Request;
using Domain.Enitites;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Calendar.Features
{
    public class CreateCalendarEvent
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateCalendarEventsRequest> _validator;
        public CreateCalendarEvent(IUnitOfWork repository, IValidator<CreateCalendarEventsRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<CalendarEventDto>> Execute(CreateCalendarEventsRequest request)
        {
            var validation = await _validator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                var erorrs = validation.Errors.Select(x => x.ErrorMessage).ToList();
                return Result<CalendarEventDto>.Failure(erorrs);
            }

            var admin = await _repository.Auths.Get(a => a.Id == request.AdminId);

            if(admin is null)
            {
                return Result<CalendarEventDto>.Failure($"Admin with id {request.AdminId} does not exist");
            }


            var calendarEvent = new CalendarEvent()
            {
                AdminId = request.AdminId,
                Title = request.Title,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Type = request.Type,
            };

            await _repository.CalendarEvents.Create(calendarEvent);
            await _repository.SaveChangesAsync();

            return Result<CalendarEventDto>.Succes(calendarEvent.ToDto());
        }
    }
}
