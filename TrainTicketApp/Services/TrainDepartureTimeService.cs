// TrainTicketApp/Services/TrainDepartureTimeService.cs
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainTicketApp.Data;
using TrainTicketApp.Models;

public class TrainDepartureTimeService : ITrainDepartureTimeService
{
    private readonly ApplicationDbContext _context;

    public TrainDepartureTimeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Train>> GetAllAsync()
    {
        return await _context.Trains.ToListAsync();
    }

    public async Task<Train> GetByIdAsync(int id)
    {
        return await _context.Trains.FindAsync(id);
    }

    public async Task AddAsync(Train train)
    {
        _context.Trains.Add(train);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Train train)
    {
        _context.Entry(train).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var train = await _context.Trains.FindAsync(id);
        if (train != null)
        {
            _context.Trains.Remove(train);
            await _context.SaveChangesAsync();
        }
    }
}