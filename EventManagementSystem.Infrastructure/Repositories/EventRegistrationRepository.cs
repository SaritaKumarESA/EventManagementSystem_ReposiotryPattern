using EventManagementSystem.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Repositories.Models;
using SQLitePCL;

namespace EventManagementSystem.Infrastructure.Repositories
{
    public class EventRegistrationRepository : GenericRepository<EventRegistration>, IEventRegistrationRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EventManagementDbContext _context;

        public EventRegistrationRepository(EventManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EventRegistration> GetByUserAndEventAsync(int userId, int eventId)
        {
         //var context=    _unitOfWork.DbContext;
          return await _context.Set<EventRegistration>().FirstOrDefaultAsync(x => x.AttendeeId == userId && x.EventId == eventId);
        }
    }
    
}