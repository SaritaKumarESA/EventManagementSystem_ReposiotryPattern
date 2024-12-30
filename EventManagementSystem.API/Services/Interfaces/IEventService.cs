using EventManagementSystem.API.DTOs;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem.API.Services.Interfaces
{
    public interface IEventService
    {
        public Task<IEnumerable<Event>> GetAllEventsAsync();

        Task<IEnumerable<Event>> GetPaginatedEventsAsync(int pageIndex, int pageSize, string? searchTerm);

        Task<Event> CreateEventAsync(EventDto eventDto);
        Task<int> UpdateEventAsync(EventDto eventDto, string? searchTerm);
    }
}
