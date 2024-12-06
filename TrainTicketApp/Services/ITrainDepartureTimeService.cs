// TrainTicketApp/Services/ITrainDepartureTimeService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainTicketApp.Models;

public interface ITrainDepartureTimeService
{
    Task<IEnumerable<Train>> GetAllAsync();
    Task<Train> GetByIdAsync(int id);
    Task AddAsync(Train train);
    Task UpdateAsync(Train train);
    Task DeleteAsync(int id);
    
}