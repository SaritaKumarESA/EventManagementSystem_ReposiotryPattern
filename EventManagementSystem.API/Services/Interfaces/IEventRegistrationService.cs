namespace EventManagementSystem.API.Services.Interfaces
{
    public interface IEventRegistrationService
    {
        Task<bool> RegisterUserToEventAsync(int userId, int eventId);
    }
}
