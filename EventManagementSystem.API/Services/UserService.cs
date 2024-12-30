using EventManagementSystem.API.DTOs;
using EventManagementSystem.API.Services.Interfaces;
using EventManagementSystem.Infrastructure.Repositories;
using EventManagementSystem.Infrastructure.Repositories.Interfaces;
using Repositories.Models;
using SQLitePCL;

namespace EventManagementSystem.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Attendee> CreateUser(UserDto userDto)
        {
            if (userDto == null) throw new ArgumentNullException(nameof(userDto));
            var user = new Attendee
            {
                Name = userDto.UserName,
                //Email = userDto.Email,
            };
            await _unitOfWork.Attendees.AddAsync(user);
            var userId = await _unitOfWork.SaveChangesAsync();
            return user;
        }
        public async Task<Attendee> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.Attendees.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
