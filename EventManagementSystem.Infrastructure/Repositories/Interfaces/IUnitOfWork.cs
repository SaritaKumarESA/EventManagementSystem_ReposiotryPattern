using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Event> Events { get; }
        IGenericRepository<Category> Categories { get; }

        IGenericRepository<Attendee> Attendees { get; }
        EventManagementDbContext DbContext { get; }
        IEventRegistrationRepository EventRegistrations { get; }
        IGenericRepository<EventRegistration> EventsRegistrationRepository { get; }

        Task< int> SaveChangesAsync();
    }
}
