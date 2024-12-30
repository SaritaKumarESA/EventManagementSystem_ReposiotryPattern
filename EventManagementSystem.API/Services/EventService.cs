using EventManagementSystem.API.DTOs;
using EventManagementSystem.API.Services.Interfaces;
using EventManagementSystem.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Index.HPRtree;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EventManagementDbContext dbContext;

        public EventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _unitOfWork.Events.GetAllAsync();
        }

        public async Task<IEnumerable<Event>> GetPaginatedEventsAsync(int pageIndex, int pageSize, string? searchTerm)
        {
            return await _unitOfWork.Events.GetPaginatedAsync(pageIndex, pageSize, query =>
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(e => e.EventName.Contains(searchTerm) || e.EventDescription.Contains(searchTerm));
                }

                query = query.OrderBy(e => e.EventDate); // Sort events by date
                return query;
            });
        }

        public async Task<Event> CreateEventAsync(EventDto eventDto)
        {
            var eventEntity = new Event
            {
                EventName = eventDto.Name,
                EventDescription = eventDto.Description,
                EventDate = eventDto.Date,
                Location = eventDto.Location,
                Capacity = eventDto.Capacity,
                CategoryId = eventDto.CategoryId


            };

            await _unitOfWork.Events.AddAsync(eventEntity);
            await _unitOfWork.SaveChangesAsync();

            return eventEntity;
        }

        public async Task<int> UpdateEventAsync(EventDto eventDto, string? searchTerm)
        {
            if ((eventDto.Id <= 0) && string.IsNullOrEmpty(searchTerm))
            {
                throw new ArgumentException("Invalid inputs");
            };

            var eventEntity = await _unitOfWork.Events.GetByIdAsync(eventDto.Id);

            if (eventEntity == null)
            {
                var events = await GetPaginatedEventsAsync(1, 5, searchTerm).ConfigureAwait(false);
                if (events != null)
                {
                    foreach (var item in events)
                    {
                        if (item.EventName.IndexOf(searchTerm, 0, StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            MapEvent(eventDto, item);
                        }
                    }
                }
            }
            else
            {
                MapEvent(eventDto, eventEntity);
            }
            var eventId = await _unitOfWork.SaveChangesAsync();
            return eventId;
            throw new ArgumentException("Event not found");
        }

        private void MapEvent(EventDto eventDto, Event eventEntity)
        {
            if (!string.IsNullOrEmpty(eventDto.Name)) eventEntity.EventName = eventDto.Name;
            if (!string.IsNullOrEmpty(eventDto.Description)) eventEntity.EventDescription = eventDto.Description;
            if (eventDto.Date != DateTime.MinValue) eventEntity.EventDate = eventDto.Date;
            if (!string.IsNullOrEmpty(eventDto.Location)) eventEntity.Location = eventDto.Location;
            if (eventDto.Capacity > 0) eventEntity.Capacity = eventDto.Capacity;
            if (eventDto.CategoryId > 0) eventEntity.CategoryId = eventDto.CategoryId;
            _unitOfWork.Events.Update(eventEntity);
        }

        //private void UpdateEvent(EventCreateUpdateDto eventCreateUpdateDto, Event eventEntity)
        //{
        //    eventEntity.EventName = eventCreateUpdateDto.Name;
        //    eventEntity.EventDescription = eventCreateUpdateDto.Description;
        //    eventEntity.EventDate = eventCreateUpdateDto.Date;
        //    eventEntity.Location = eventCreateUpdateDto.Location;
        //    eventEntity.Capacity = eventCreateUpdateDto.Capacity;
        //    eventEntity.CategoryId = eventCreateUpdateDto.CategoryId;
        //}



        //public async Task CreateEventWithOrganizersAsync(EventDto eventDto, List<int> organizerIds)
        //{
        //    var context = _unitOfWork.DbContext;
        //    using var transaction = await context.Database.BeginTransactionAsync();

        //    try
        //    {
        //        // Add the event
        //        await context.Categories.AddAsync(new Category { CategoryName=eventDto.Category.CategoryName});
        //        await context.SaveChangesAsync();

        //        //// Add organizers for the event
        //        //foreach (var organizerId in organizerIds)
        //        //{
        //        //    var organizer = new EventOrganizer
        //        //    {
        //        //        EventId = eventEntity.Id,
        //        //        OrganizerId = organizerId
        //        //    };
        //        //    await _context.EventOrganizers.AddAsync(organizer);
        //        //}


        //        //await _context.SaveChangesAsync();

        //        // Commit transaction
        //        await transaction.CommitAsync();
        //    }
        //    catch (Exception)
        //    {
        //        // Rollback transaction in case of error
        //        await transaction.RollbackAsync();
        //        throw; // Re-throw the exception
        //    }
        //}

    }
}
