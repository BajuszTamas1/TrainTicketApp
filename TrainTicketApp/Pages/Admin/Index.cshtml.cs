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
using System.Threading.Tasks;

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
        public Dictionary<DayOfWeek, string> DepartureTimes { get; set; } = new Dictionary<DayOfWeek, string>();

        public bool TrainAdded { get; set; }
        public IList<Train> Trains { get; set; } = new List<Train>();

        public async Task OnGetAsync()
        {
            Trains = await _context.Trains.ToListAsync();
        }

        public async Task OnPostAsync()
        {
            if (!Regex.IsMatch(TravelTime, @"^([0-9]|[0-1][0-9]|2[0-3]):[0-5][0-9]$"))
            {
                ModelState.AddModelError("TravelTime", "Invalid time format. Please use hh:mm or h:mm.");
                return;
            }

            var train = new Train
            {
                DepartureLocation = DepartureLocation,
                ArrivalLocation = ArrivalLocation,
                Distance = Distance,
                Price = Price,
                Monday = TimeSpan.Parse(DepartureTimes.GetValueOrDefault(DayOfWeek.Monday, "00:00")),
                Tuesday = TimeSpan.Parse(DepartureTimes.GetValueOrDefault(DayOfWeek.Tuesday, "00:00")),
                Wednesday = TimeSpan.Parse(DepartureTimes.GetValueOrDefault(DayOfWeek.Wednesday, "00:00")),
                Thursday = TimeSpan.Parse(DepartureTimes.GetValueOrDefault(DayOfWeek.Thursday, "00:00")),
                Friday = TimeSpan.Parse(DepartureTimes.GetValueOrDefault(DayOfWeek.Friday, "00:00")),
                Saturday = TimeSpan.Parse(DepartureTimes.GetValueOrDefault(DayOfWeek.Saturday, "00:00")),
                Sunday = TimeSpan.Parse(DepartureTimes.GetValueOrDefault(DayOfWeek.Sunday, "00:00"))
            };

            _context.Trains.Add(train);
            await _context.SaveChangesAsync();

            TrainAdded = true;
            await OnGetAsync(); // Refresh the list after adding a new train
        }

        public async Task OnPostEditAsync(int id)
        {
            if (!Regex.IsMatch(TravelTime, @"^([0-9]|[0-1][0-9]|2[0-3]):[0-5][0-9]$"))
            {
                ModelState.AddModelError("TravelTime", "Invalid time format. Please use hh:mm or h:mm.");
                return;
            }

            var train = await _context.Trains.FindAsync(id);
            if (train != null)
            {
                train.DepartureLocation = DepartureLocation;
                train.ArrivalLocation = ArrivalLocation;
                train.Distance = Distance;
                train.Price = Price;
                train.Monday = TimeSpan.Parse(DepartureTimes.GetValueOrDefault(DayOfWeek.Monday, "00:00"));
                train.Tuesday = TimeSpan.Parse(DepartureTimes.GetValueOrDefault(DayOfWeek.Tuesday, "00:00"));
                train.Wednesday = TimeSpan.Parse(DepartureTimes.GetValueOrDefault(DayOfWeek.Wednesday, "00:00"));
                train.Thursday = TimeSpan.Parse(DepartureTimes.GetValueOrDefault(DayOfWeek.Thursday, "00:00"));
                train.Friday = TimeSpan.Parse(DepartureTimes.GetValueOrDefault(DayOfWeek.Friday, "00:00"));
                train.Saturday = TimeSpan.Parse(DepartureTimes.GetValueOrDefault(DayOfWeek.Saturday, "00:00"));
                train.Sunday = TimeSpan.Parse(DepartureTimes.GetValueOrDefault(DayOfWeek.Sunday, "00:00"));

                await _context.SaveChangesAsync();
            }

            await OnGetAsync(); // Refresh the list after editing a train
        }

        public async Task OnPostDeleteAsync(int id)
        {
            var train = await _context.Trains.FindAsync(id);
            if (train != null)
            {
                _context.Trains.Remove(train);
                await _context.SaveChangesAsync();
            }

            await OnGetAsync(); // Refresh the list after deleting a train
        }
    }
}