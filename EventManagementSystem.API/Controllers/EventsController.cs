using EventManagementSystem.API.DTOs;
using EventManagementSystem.API.Services;
using EventManagementSystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IEventRegistrationService _eventRegistrationService;

        public EventsController(IEventService eventService, IEventRegistrationService eventRegistrationService)
        {
            _eventService = eventService;
            _eventRegistrationService = eventRegistrationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginatedEvents(int pageIndex = 1, int pageSize = 10, string? searchTerm = null)
        {
            var events = await _eventService.GetPaginatedEventsAsync(pageIndex, pageSize, searchTerm);
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventDto eventDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdEvent = await _eventService.CreateEventAsync(eventDto);
            return CreatedAtAction(nameof(GetAllEvents), new { id = createdEvent.EventId }, createdEvent);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent([FromBody] EventDto eventDto, string? searchTerm = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedEvent = await _eventService.UpdateEventAsync(eventDto, searchTerm);
            //if (updatedEvent == null)
            //    return NotFound();

            return Ok(updatedEvent);
        }

        [HttpPost("{userId}/{eventId}")]
        public async Task<ActionResult> RegisterUserToEventAsync(int userId, int eventId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _eventRegistrationService.RegisterUserToEventAsync(userId, eventId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
