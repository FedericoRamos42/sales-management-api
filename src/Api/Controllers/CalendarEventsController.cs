using Application.Services.Calendar;
using Application.Services.Calendar.Models.Request;
using Application.Services.Categories.Features;
using Application.Services.Login.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEventsController : ControllerBase
    {
        private readonly CalendarUseCases _services;
        private readonly IUserContext _context;

        public CalendarEventsController(IUserContext context, CalendarUseCases services)
        {
            _services = services;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] GetCalendarEventsRequest request)
        {
            var adminId = _context.AdminId;
            
            var result = await _services.GetCalendar.Execute(adminId,request);

            if (!result.IsSucces)
            {
                return BadRequest(new { errors = result.Errors });
            }
            return Ok(result.Value);
        }

    }
}
