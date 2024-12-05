// TrainTicketApp/Pages/Admin/Index.cshtml.cs
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TrainTicketApp.Models;
using TrainTicketApp.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TrainTicketApp.Pages.Admin
{
    public class AdminIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AdminIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty, Required]
        public string DepartureLocation { get; set; }
        [BindProperty, Required]
        public string ArrivalLocation { get; set; }
        [BindProperty]
        public int Distance { get; set; }
        [BindProperty]
        public decimal Price { get; set; }
        [BindProperty, Required]
        public string TravelTime { get; set; }
        [BindProperty]
        public Dictionary<DayOfWeek, List<string>> DepartureTimes { get; set; } = new Dictionary<DayOfWeek, List<string>>();

        public bool TrainAdded { get; set; }
        public IList<Train> Trains { get; set; } = new List<Train>();

        public void OnGet()
        {
            Trains = _context.Trains.Include(t => t.DepartureTimes).ToList();
        }

        public void OnPost()
        {
            if (!Regex.IsMatch(TravelTime, @"^([0-9]|[0-1][0-9]|2[0-3]):[0-5][0-9]$"))
            {
                // Handle invalid format
                ModelState.AddModelError("TravelTime", "Invalid time format. Please use hh:mm or h:mm.");
                return;
            }

            var departureTimes = new List<TrainDepartureTime>();
            foreach (var day in Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>())
            {
                if (DepartureTimes.ContainsKey(day) && DepartureTimes[day].Any())
                {
                    foreach (var time in DepartureTimes[day])
                    {
                        if (Regex.IsMatch(time, @"^([0-9]|[0-1][0-9]|2[0-3]):[0-5][0-9]$"))
                        {
                            departureTimes.Add(new TrainDepartureTime
                            {
                                DayOfWeek = day,
                                DepartureTime = TimeSpan.Parse(time)
                            });
                        }
                    }
                }
            }

            var train = new Train
            {
                DepartureLocation = DepartureLocation,
                ArrivalLocation = ArrivalLocation,
                Distance = Distance,
                Price = Price,
                TravelTime = TravelTime,
                DepartureTimes = departureTimes
            };

            _context.Trains.Add(train);
            _context.SaveChanges();
            TrainAdded = true;

            OnGet(); // Refresh the list after adding a new train
        }

        public void OnPostEdit(int id)
        {
            if (!Regex.IsMatch(TravelTime, @"^([0-9]|[0-1][0-9]|2[0-3]):[0-5][0-9]$"))
            {
                // Handle invalid format
                ModelState.AddModelError("TravelTime", "Invalid time format. Please use hh:mm or h:mm.");
                return;
            }

            var train = _context.Trains.Include(t => t.DepartureTimes).FirstOrDefault(t => t.Id == id);
            if (train != null)
            {
                var departureTimes = new List<TrainDepartureTime>();
                foreach (var day in Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>())
                {
                    if (DepartureTimes.ContainsKey(day) && DepartureTimes[day].Any())
                    {
                        foreach (var time in DepartureTimes[day])
                        {
                            if (Regex.IsMatch(time, @"^([0-9]|[0-1][0-9]|2[0-3]):[0-5][0-9]$"))
                            {
                                departureTimes.Add(new TrainDepartureTime
                                {
                                    DayOfWeek = day,
                                    DepartureTime = TimeSpan.Parse(time)
                                });
                            }
                        }
                    }
                }

                train.DepartureLocation = DepartureLocation;
                train.ArrivalLocation = ArrivalLocation;
                train.Distance = Distance;
                train.Price = Price;
                train.TravelTime = TravelTime;
                train.DepartureTimes = departureTimes;

                _context.SaveChanges();
            }

            OnGet(); // Refresh the list after editing a train
        }

        public void OnPostDelete([FromForm] int id)
        {
            var train = _context.Trains.FirstOrDefault(t => t.Id == id);
            if (train != null)
            {
                _context.Trains.Remove(train);
                _context.SaveChanges();
            }

            OnGet(); // Refresh the list after deleting a train
        }
    }
}