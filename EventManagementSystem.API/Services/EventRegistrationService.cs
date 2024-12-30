using EventManagementSystem.API.Services.Interfaces;
using EventManagementSystem.Infrastructure.Repositories;
using EventManagementSystem.Infrastructure.Repositories.Interfaces;
using EventManagementSystem.Infrastructure.Services;
using Repositories.Models;

namespace EventManagementSystem.API.Services
{
    public class EventRegistrationService : IEventRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EventService> _logger;

        public EventRegistrationService(IUnitOfWork unitOfWork, ILogger<EventService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<bool> RegisterUserToEventAsync(int userId, int eventId)
        {
            _logger.LogInformation("Attempting to register User {UserId} to Event {EventId}", userId, eventId);
            try
            {
                var eventEntity = await _unitOfWork.Events.GetByIdAsync(eventId);
                if (eventEntity == null)
                    throw new KeyNotFoundException("Event not found");

                if (eventEntity.Capacity <= 0)
                    throw new InvalidOperationException("Event is full");

                var user = await _unitOfWork.Attendees.GetByIdAsync(userId);
                if (user == null)
                    throw new KeyNotFoundException("User not found");

                var existingRegistration = await _unitOfWork.EventRegistrations.GetByUserAndEventAsync(userId, eventId);
                if (existingRegistration != null)
                    throw new InvalidOperationException("User is already registered for this event");

                // Register user
                var registration = new EventRegistration
                {
                    AttendeeId = userId,
                    EventId = eventId,
                    RegistrationDate = DateTime.UtcNow
                };

                await _unitOfWork.EventsRegistrationRepository.AddAsync(registration);

                // Decrease event capacity
                eventEntity.Capacity--;
                _unitOfWork.Events.Update(eventEntity);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering User {UserId} to Event {EventId}", userId, eventId);
                throw;
            }
        }
    }
}
