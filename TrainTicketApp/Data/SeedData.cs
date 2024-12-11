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
                if (context.Trains.Any())
                {
                    return;
                }

                context.Trains.AddRange(
                    new Train
                    {
                        DepartureLocation = "City A",
                        ArrivalLocation = "City B",
                        Distance = 100,
                        Price = 50,
                        TravelTime = "2:00",
                        TicketType = "Normal",
                        Monday = new TimeSpan(6, 0, 0),
                        Tuesday = new TimeSpan(7, 0, 0),
                        Wednesday = new TimeSpan(8, 0, 0),
                        Thursday = new TimeSpan(9, 0, 0),
                        Friday = new TimeSpan(10, 0, 0),
                        Saturday = new TimeSpan(11, 0, 0),
                        Sunday = new TimeSpan(12, 0, 0),
                        CarCount = 1,
                        SeatCount = 50,
                        AllSeats = 50,
                        AvailableSeats = 50,
                        ReservationPrice = 10,
                        SupplementaryPrice = 5,
                        AvailableSeatsMonday = 50,
                        AvailableSeatsTuesday = 50,
                        AvailableSeatsWednesday = 50,
                        AvailableSeatsThursday = 50,
                        AvailableSeatsFriday = 50,
                        AvailableSeatsSaturday = 50,
                        AvailableSeatsSunday = 50
                    },
                    new Train
                    {
                        DepartureLocation = "City C",
                        ArrivalLocation = "City D",
                        Distance = 200,
                        Price = 100,
                        TravelTime = "4:00",
                        TicketType = "Normal",
                        Monday = new TimeSpan(6, 0, 0),
                        Tuesday = new TimeSpan(7, 0, 0),
                        Wednesday = new TimeSpan(8, 0, 0),
                        Thursday = new TimeSpan(9, 0, 0),
                        Friday = new TimeSpan(10, 0, 0),
                        Saturday = new TimeSpan(11, 0, 0),
                        Sunday = new TimeSpan(12, 0, 0),
                        CarCount = 1,
                        SeatCount = 50,
                        AllSeats = 50,
                        AvailableSeats = 50,
                        ReservationPrice = 10,
                        SupplementaryPrice = 5,
                        AvailableSeatsMonday = 50,
                        AvailableSeatsTuesday = 50,
                        AvailableSeatsWednesday = 50,
                        AvailableSeatsThursday = 50,
                        AvailableSeatsFriday = 50,
                        AvailableSeatsSaturday = 50,
                        AvailableSeatsSunday = 50
                    }
                );

                context.SaveChanges();

                if (context.Orders.Any())
                {
                    return;
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
                        ArrivalLocation = "City D",
                        DepartureLocation = "City C",
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
                        ArrivalLocation = "City D",
                        DepartureLocation = "City C",
                        DepartureTime = new TimeSpan(7, 0, 0)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}