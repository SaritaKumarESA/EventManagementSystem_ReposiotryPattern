using System.ComponentModel.DataAnnotations;

namespace Repositories.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Display(Name = "Event Name")]
        [Required(ErrorMessage = "Event Name is required")]
        public string EventName { get; set; } = string.Empty;

        [Display(Name = "Event Description")]
        [StringLength(500, ErrorMessage = "Event Description must be less than 500 characters")]
        public string EventDescription { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public string Location { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
