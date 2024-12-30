namespace EventManagementSystem.API.DTOs
{
    public class EventDto
    {
        public int Id { get; set; } // Optional: for updating or returning data
        public string Name { get; set; }        = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }

        //public CategoryDto Category { get; set; } // Nested Category
        //
        public int CategoryId { get; set; }
    }
}
