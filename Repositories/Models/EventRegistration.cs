
using System.ComponentModel.DataAnnotations;

namespace Repositories.Models
{
    public class EventRegistration
    {
        public Guid Id { get; set; }
        public int EventId { get; set; }

        public int AttendeeId { get; set; }

        public DateTime RegistrationDate { get; set; }

        [Required]
        public Event Event { get; set; }

        public ICollection<Attendee> Attendees { get; set; }
    }
}