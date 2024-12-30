using Bogus;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore.BulkExtensions;

namespace EventManagementSystem.Infrastructure.Data
{
    public class DataSeeder
    {
        public static async Task SeedEventsWithBogusAsync(EventManagementDbContext context)
        {
            if (await context.Events.AnyAsync())
                return;

            var faker = new Faker<Event>()
                .RuleFor(e => e.EventName, f => f.Commerce.ProductName())
                .RuleFor(e => e.EventDescription, f => f.Lorem.Sentence())
                .RuleFor(e => e.EventDate, f => f.Date.Future())
                .RuleFor(e => e.Location, f => f.Address.City())
                .RuleFor(e => e.Capacity, f => f.Random.Int(50, 500));

            var events = faker.Generate(10000);
            await context.BulkInsertAsync(events);  // Use bulk insert for efficiency
        }
    }
}
