using System.Collections.Generic;
using TrainTicketApp.Models;

namespace TrainTicketApp.Services;

public interface ITrainService
{
    IEnumerable<Train> GetAllTrains();
    Train GetTrainById(int id);
    void AddTrain(Train train);
}