using EventManagementSystem.API.DTOs;
using Repositories.Models;

namespace EventManagementSystem.API.Services.Interfaces
{
    public interface IUserService
    {
         Task<Attendee> GetByIdAsync(int id);

        Task<Attendee> CreateUser(UserDto userDto);
    }
}
