using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Models
{
    public class EventManagementDbContext : DbContext
    {

        public EventManagementDbContext(DbContextOptions<EventManagementDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Attendee> Attendees { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<EventRegistration> EventRegistrations { get; set; }

        //TODO: Add the remaining entities as the application progresses
        //public DbSet<Organizer> Organizers { get; set; }
        //public DbSet<Session> Sessions { get; set; }
        //public DbSet<Speaker> Speakers { get; set; }
        //public DbSet<Track> Tracks { get; set; }
        //public DbSet<SessionAttendee> SessionAttendees { get; set; }
        //public DbSet<SessionSpeaker> SessionSpeakers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }

}
