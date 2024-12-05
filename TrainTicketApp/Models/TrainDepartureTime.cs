// TrainTicketApp/Models/TrainDepartureTime.cs
using System;

namespace TrainTicketApp.Models
{
    public class TrainDepartureTime
    {
        public int Id { get; set; }
        public int TrainId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan DepartureTime { get; set; }
    }
}