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
        public TimeSpan Monday { get; set; }
        public TimeSpan Tuesday { get; set; }
        public TimeSpan Wednesday { get; set; }
        public TimeSpan Thursday { get; set; }
        public TimeSpan Friday { get; set; }
        public TimeSpan Saturday { get; set; }
        public TimeSpan Sunday { get; set; }
        public string TicketType { get; set; }
        public int CarCount { get; set; }
        public int SeatCount { get; set; }
        public int AllSeats { get; set; }
        public int AvailableSeats { get; set; }
        public int ReservationPrice { get; set; }
        public int SupplementaryPrice { get; set; }
    }
}