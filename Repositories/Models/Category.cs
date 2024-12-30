namespace Repositories.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public ICollection<Event>? Events { get; set; } // A Category can have many Events
    }
}