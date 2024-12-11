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
            return _context.Trains.ToList();
        }

        public Train GetTrainById(int id)
        {
            return _context.Trains.Find(id);
        }

        public void AddTrain(Train train)
        {
            _context.Trains.Add(train);
            _context.SaveChanges();
        }

        public void UpdateTrain(Train train)
        {
            _context.Entry(train).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteTrain(int id)
        {
            var train = _context.Trains.Find(id);
            if (train != null)
            {
                _context.Trains.Remove(train);
                _context.SaveChanges();
            }
        }
    }
}