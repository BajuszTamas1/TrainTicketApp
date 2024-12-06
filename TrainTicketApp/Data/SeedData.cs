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
                        DepartureLocation = "City A",
                        ArrivalLocation = "City B",
                        Distance = 100,
                        Price = 50,
                        TravelTime = "2:00",
                        Monday = new TimeSpan(6, 0, 0),
                        Tuesday = new TimeSpan(7, 0, 0),
                        Wednesday = new TimeSpan(8, 0, 0),
                        Thursday = new TimeSpan(9, 0, 0),
                        Friday = new TimeSpan(10, 0, 0),
                        Saturday = new TimeSpan(11, 0, 0),
                        Sunday = new TimeSpan(12, 0, 0)
                    },
                    new Train
                    {
                        DepartureLocation = "City C",
                        ArrivalLocation = "City D",
                        Distance = 200,
                        Price = 100,
                        TravelTime = "4:00",
                        Monday = new TimeSpan(6, 0, 0),
                        Tuesday = new TimeSpan(7, 0, 0),
                        Wednesday = new TimeSpan(8, 0, 0),
                        Thursday = new TimeSpan(9, 0, 0),
                        Friday = new TimeSpan(10, 0, 0),
                        Saturday = new TimeSpan(11, 0, 0),
                        Sunday = new TimeSpan(12, 0, 0)
                    }
                );

                context.SaveChanges();

                // Look for any orders.
                if (context.Orders.Any())
                {
                    return;   // DB has been seeded
                }

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
                        Status = "Active",
                        DayOfWeek = "Monday",
                        DepartureTime = new TimeSpan(6, 0, 0)
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
                        Status = "Active",
                        DayOfWeek = "Tuesday",
                        DepartureTime = new TimeSpan(7, 0, 0)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}