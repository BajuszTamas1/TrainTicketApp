// TrainTicketApp/Services/TrainService.cs
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TrainTicketApp.Models;
using TrainTicketApp.Data;

namespace TrainTicketApp.Services
{
    public class TrainService : ITrainService
    {
        private readonly ApplicationDbContext _context;

        public TrainService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Train> GetAllTrains()
        {
            return _context.Trains.Include(t => t.DepartureTimes).ToList();
        }

        public Train GetTrainById(int id)
        {
            return _context.Trains.Include(t => t.DepartureTimes).FirstOrDefault(t => t.Id == id);
        }

        public void AddTrain(Train train)
        {
            _context.Trains.Add(train);
            _context.SaveChanges();
        }
    }
}