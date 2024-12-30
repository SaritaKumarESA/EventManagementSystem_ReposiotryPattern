using Repositories.Models;

namespace EventManagementSystem.Infrastructure.Repositories.Interfaces
{
    public interface IEventRegistrationRepository
    {
        Task<EventRegistration> GetByUserAndEventAsync(int userId, int eventId);
    }
}