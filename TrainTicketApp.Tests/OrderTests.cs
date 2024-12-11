using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TrainTicketApp.Data;
using TrainTicketApp.Models;
using Xunit;

namespace TrainTicketApp.Tests
{
    public class OrderTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task CanSearchTimetable()
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

            var result = await context.Trains
                .Where(t => t.DepartureLocation == "City C" && t.ArrivalLocation == "City D")
                .ToListAsync();

            Assert.Single(result);
        }

        [Fact]
        public async Task CanPlaceOrder()
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
                UserName = "testuser",
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

            var result = await context.Orders.FirstOrDefaultAsync(o => o.UserName == "testuser");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanShowOrderConfirmation()
        {
            using var context = GetDbContext();
            var order = new Order
            {
                TrainId = 2,
                UserName = "testuser",
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

            var result = await context.Orders.FirstOrDefaultAsync(o => o.UserName == "testuser");

            Assert.NotNull(result);
            Assert.Equal(order.Id, result.Id);
        }

        [Fact]
        public async Task CanExportTimetableToXml()
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

            var trains = await context.Trains.ToListAsync();
            var xml = new System.Xml.Serialization.XmlSerializer(typeof(Train[]));
            using var stringWriter = new System.IO.StringWriter();
            xml.Serialize(stringWriter, trains.ToArray());

            var result = stringWriter.ToString();

            Assert.Contains("City C", result);
            Assert.Contains("City D", result);
        }
    }
}