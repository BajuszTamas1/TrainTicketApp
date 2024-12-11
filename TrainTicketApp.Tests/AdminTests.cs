using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TrainTicketApp.Data;
using TrainTicketApp.Models;
using Xunit;

namespace TrainTicketApp.Tests
{
    public class AdminTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task CanAddTrainSchedule()
        {
            using var context = GetDbContext();
            var train = new Train
            {
                DepartureLocation = "City C",
                ArrivalLocation = "City D",
                Distance = 200,
                Price = 100,
                TravelTime = "04:00",
                TicketType = "Normal",
                Monday = new TimeSpan(6, 0, 0),
                Tuesday = new TimeSpan(7, 0, 0),
                Wednesday = new TimeSpan(8, 0, 0),
                Thursday = new TimeSpan(9, 0, 0),
                Friday = new TimeSpan(10, 0, 0),
                Saturday = new TimeSpan(11, 0, 0),
                Sunday = new TimeSpan(12, 0, 0)
            };

            context.Trains.Add(train);
            await context.SaveChangesAsync();

            var result = await context.Trains.FirstOrDefaultAsync(t => t.DepartureLocation == "City C");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanPlaceOrderAsAdmin()
        {
            using var context = GetDbContext();
            context.Trains.Add(new Train
            {
                DepartureLocation = "City C",
                ArrivalLocation = "City D",
                Distance = 200,
                Price = 100,
                TravelTime = "04:00",
                TicketType = "Normal",
                Monday = new TimeSpan(6, 0, 0),
                Tuesday = new TimeSpan(7, 0, 0),
                Wednesday = new TimeSpan(8, 0, 0),
                Thursday = new TimeSpan(9, 0, 0),
                Friday = new TimeSpan(10, 0, 0),
                Saturday = new TimeSpan(11, 0, 0),
                Sunday = new TimeSpan(12, 0, 0)
            });
            await context.SaveChangesAsync();

            var order = new Order
            {
                TrainId = 2,
                UserName = "admin",
                UserAddress = "456 Elm St",
                UserEmail = "testuser@example.com",
                UserPhone = "0987654321",
                TicketType = "Supplementary",
                OrderDate = DateTime.Now,
                Status = "Active",
                DayOfWeek = "Tuesday",
                ArrivalLocation = "City D",
                DepartureLocation = "City C",
                DepartureTime = new TimeSpan(7, 0, 0)
            };

            context.Orders.Add(order);
            await context.SaveChangesAsync();

            var result = await context.Orders.FirstOrDefaultAsync(o => o.UserName == "admin");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanViewOrderStatistics()
        {
            using var context = GetDbContext();
            context.Orders.Add(new Order
            {
                TrainId = 2,
                UserName = "admin",
                UserAddress = "456 Elm St",
                UserEmail = "testuser@example.com",
                UserPhone = "0987654321",
                TicketType = "Supplementary",
                OrderDate = DateTime.Now,
                Status = "Active",
                DayOfWeek = "Tuesday",
                ArrivalLocation = "City D",
                DepartureLocation = "City C",
                DepartureTime = new TimeSpan(7, 0, 0)
            });
            await context.SaveChangesAsync();

            var orders = await context.Orders
                .Where(o => o.OrderDate >= DateTime.Now.AddDays(-1) && o.OrderDate <= DateTime.Now.AddDays(1))
                .ToListAsync();

            Assert.Single(orders);
        }

        [Fact]
        public async Task CanViewTicketTypeStatistics()
        {
            using var context = GetDbContext();
            context.Orders.Add(new Order
            {
                TrainId = 2,
                UserName = "admin",
                UserAddress = "456 Elm St",
                UserEmail = "testuser@example.com",
                UserPhone = "0987654321",
                TicketType = "Normal",
                OrderDate = DateTime.Now,
                Status = "Active",
                DayOfWeek = "Tuesday",
                ArrivalLocation = "City D",
                DepartureLocation = "City C",
                DepartureTime = new TimeSpan(7, 0, 0)
            });
            await context.SaveChangesAsync();

            var ticketTypes = await context.Orders
                .GroupBy(o => o.TicketType)
                .Select(g => new { TicketType = g.Key, Count = g.Count() })
                .ToListAsync();

            Assert.Single(ticketTypes);
            Assert.Equal("Normal", ticketTypes.First().TicketType);
        }

        [Fact]
        public async Task CanSpecifyTicketType()
        {
            using var context = GetDbContext();
            var train = new Train
            {
                DepartureLocation = "City C",
                ArrivalLocation = "City D",
                Distance = 200,
                TravelTime = "02:00",
                TicketType = "Helyjegy",
                CarCount = 5,
                SeatCount = 100,
                Price = 100,
                Monday = new TimeSpan(8, 0, 0),
                Tuesday = new TimeSpan(7, 0, 0),
                Wednesday = new TimeSpan(8, 0, 0),
                Thursday = new TimeSpan(9, 0, 0),
                Friday = new TimeSpan(10, 0, 0),
                Saturday = new TimeSpan(11, 0, 0),
                Sunday = new TimeSpan(12, 0, 0)
            };

            context.Trains.Add(train);
            await context.SaveChangesAsync();

            var result = await context.Trains.FirstOrDefaultAsync(t => t.TicketType == "Helyjegy");

            Assert.NotNull(result);
            Assert.Equal(5, result.CarCount);
            Assert.Equal(100, result.SeatCount);
        }
    }
}