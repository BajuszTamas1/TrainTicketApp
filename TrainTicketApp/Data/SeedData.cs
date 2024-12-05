// TrainTicketApp/Data/SeedData.cs
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
                        DepartureTimes = new List<TrainDepartureTime>
                        {
                            new TrainDepartureTime { DayOfWeek = DayOfWeek.Monday, DepartureTime = new TimeSpan(6, 0, 0) },
                            new TrainDepartureTime { DayOfWeek = DayOfWeek.Monday, DepartureTime = new TimeSpan(14, 0, 0) },
                            new TrainDepartureTime { DayOfWeek = DayOfWeek.Tuesday, DepartureTime = new TimeSpan(6, 0, 0) },
                            new TrainDepartureTime { DayOfWeek = DayOfWeek.Tuesday, DepartureTime = new TimeSpan(14, 0, 0) }
                        }
                    },
                    new Train
                    {
                        DepartureLocation = "Budapest",
                        ArrivalLocation = "Prague",
                        Distance = 525,
                        Price = 49.99m,
                        TravelTime = "6:30",
                        DepartureTimes = new List<TrainDepartureTime>
                        {
                            new TrainDepartureTime { DayOfWeek = DayOfWeek.Wednesday, DepartureTime = new TimeSpan(7, 0, 0) },
                            new TrainDepartureTime { DayOfWeek = DayOfWeek.Wednesday, DepartureTime = new TimeSpan(15, 0, 0) },
                            new TrainDepartureTime { DayOfWeek = DayOfWeek.Thursday, DepartureTime = new TimeSpan(7, 0, 0) },
                            new TrainDepartureTime { DayOfWeek = DayOfWeek.Thursday, DepartureTime = new TimeSpan(15, 0, 0) }
                        }
                    }
                );

                context.SaveChanges();
            }
        }
    }
}