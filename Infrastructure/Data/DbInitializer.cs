using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Data;

public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any()) return;

            var users = new User[]
            {
                new User { Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), Role =  Role.Admin},
                new User { Username = "user1", PasswordHash = BCrypt.Net.BCrypt.HashPassword("user1234"), Role = Role.User },
                new User { Username = "user2", PasswordHash = BCrypt.Net.BCrypt.HashPassword("user5678"), Role = Role.User }
            };
            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();

            var venues = new Venue[]
            {
                new Venue { Name = "Main Hall", Location = "123 Main St" },
                new Venue { Name = "Conference Center", Location = "456 Elm St" }
            };
            foreach (var venue in venues)
            {
                context.Venues.Add(venue);
            }
            context.SaveChanges();

            var events = new Event[]
            {
                new Event { Name = "Tech Conference", Date = new DateTime(2024, 7, 20), Description = "A conference about technology.", OrganizerId = users[0].Id },
                new Event { Name = "Music Concert", Date = new DateTime(2024, 8, 15), Description = "A live music concert.", OrganizerId = users[1].Id }
            };
            foreach (var eventItem in events)
            {
                context.Events.Add(eventItem);
            }
            context.SaveChanges();

            var tickets = new Ticket[]
            {
                new Ticket { TicketNumber = "TICKET001", EventId = events[0].Id, UserId = users[1].Id },
                new Ticket { TicketNumber = "TICKET002", EventId = events[1].Id, UserId = users[2].Id }
            };
            foreach (var ticket in tickets)
            {
                context.Tickets.Add(ticket);
            }
            context.SaveChanges();
        }
    }