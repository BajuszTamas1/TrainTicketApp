// TrainTicketApp/Models/Train.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainTicketApp.Models
{
    public class Train
    {
        public int Id { get; set; }
        [Required]
        public string DepartureLocation { get; set; }
        [Required]
        public string ArrivalLocation { get; set; }
        public int Distance { get; set; }
        public decimal Price { get; set; }
        [Required]
        public string TravelTime { get; set; }
        public List<TrainDepartureTime> DepartureTimes { get; set; } = new List<TrainDepartureTime>();
    }
}