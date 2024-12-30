using EventManagementSystem.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventManagementDbContext _dbContext;

        public UnitOfWork(EventManagementDbContext dbContext, IGenericRepository<Event> EventRepository, IGenericRepository<Category> categoryRepository, IGenericRepository<Attendee> attendees,
            IGenericRepository<EventRegistration> eventRegisGenericRepository, IEventRegistrationRepository eventRegistrationRepository)
        {
            _dbContext = dbContext;
            Events = EventRepository;
            Categories = categoryRepository;
            Attendees = attendees;
            EventRegistrations = eventRegistrationRepository;
            EventsRegistrationRepository = eventRegisGenericRepository;
        }
        public IGenericRepository<Event> Events { get; }

        public IGenericRepository<Category> Categories { get; }

        public IGenericRepository<Attendee> Attendees { get; }

        public EventManagementDbContext DbContext => _dbContext;

        public IEventRegistrationRepository EventRegistrations { get; }

        public IGenericRepository<EventRegistration> EventsRegistrationRepository { get; }

        public Task<int> SaveChangesAsync()
        {
            ApplyValidation();
            return _dbContext.SaveChangesAsync();
        }

        private void ApplyValidation()
        {
            var invalidEvents = _dbContext.ChangeTracker.Entries<Event>()
                                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                                .Where(e => e.Entity.Capacity > 1000);

            if (invalidEvents.Any())
            {
                throw new InvalidOperationException("Event capacity cannot exceed 1000.");
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

    }
}
