using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TrainTicketApp.Models;

namespace TrainTicketApp.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any trains.
                if (context.Trains.Any())
                {
                    return;   // DB has been seeded
                }

                context.Trains.AddRange(
                    new Train
                    {
                        DepartureLocation = "Budapest",
                        ArrivalLocation = "Vienna",
                        Distance = 250,
                        Price = 29.99m,
                        TravelTime = "2:30",
                        Monday = new TimeSpan(6, 0, 0),
                        Tuesday = new TimeSpan(6, 0, 0),
                        Wednesday = new TimeSpan(6, 0, 0),
                        Thursday = new TimeSpan(6, 0, 0),
                        Friday = new TimeSpan(6, 0, 0),
                        Saturday = new TimeSpan(6, 0, 0),
                        Sunday = new TimeSpan(6, 0, 0)
                    },
                    new Train
                    {
                        DepartureLocation = "Budapest",
                        ArrivalLocation = "Prague",
                        Distance = 525,
                        Price = 49.99m,
                        TravelTime = "6:30",
                        Monday = new TimeSpan(7, 0, 0),
                        Tuesday = new TimeSpan(7, 0, 0),
                        Wednesday = new TimeSpan(7, 0, 0),
                        Thursday = new TimeSpan(7, 0, 0),
                        Friday = new TimeSpan(7, 0, 0),
                        Saturday = new TimeSpan(7, 0, 0),
                        Sunday = new TimeSpan(7, 0, 0)
                    }
                );

                context.Orders.AddRange(
                    new Order
                    {
                        TrainId = 1,
                        UserName = "Alice",
                        UserAddress = "123 Main St",
                        UserEmail = "alice@example.com",
                        UserPhone = "1234567890",
                        TicketType = "Normal",
                        OrderDate = DateTime.Now,
                        Status = "Active"
                    },
                    new Order
                    {
                        TrainId = 2,
                        UserName = "Bob",
                        UserAddress = "456 Elm St",
                        UserEmail = "bob@example.com",
                        UserPhone = "0987654321",
                        TicketType = "Supplementary",
                        OrderDate = DateTime.Now,
                        Status = "Active"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}