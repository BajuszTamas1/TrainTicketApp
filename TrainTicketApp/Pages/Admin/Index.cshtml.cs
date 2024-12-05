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
        public string Monday { get; set; }
        [BindProperty]
        public string Tuesday { get; set; }
        [BindProperty]
        public string Wednesday { get; set; }
        [BindProperty]
        public string Thursday { get; set; }
        [BindProperty]
        public string Friday { get; set; }
        [BindProperty]
        public string Saturday { get; set; }
        [BindProperty]
        public string Sunday { get; set; }

        public bool TrainAdded { get; set; }
        public IList<Train> Trains { get; set; } = new List<Train>();

        public async Task OnGetAsync()
        {
            Trains = await _context.Trains.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!Regex.IsMatch(TravelTime, @"^([0-9]|[0-1][0-9]|2[0-3]):[0-5][0-9]$"))
            {
                ModelState.AddModelError("TravelTime", "Invalid time format. Please use hh:mm or h:mm.");
                return Page();
            }

            var train = new Train
            {
                DepartureLocation = DepartureLocation,
                ArrivalLocation = ArrivalLocation,
                Distance = Distance,
                Price = Price,
                TravelTime = TravelTime, // Ensure TravelTime is set
                Monday = TimeSpan.TryParse(Monday, out var monday) ? monday : TimeSpan.Zero,
                Tuesday = TimeSpan.TryParse(Tuesday, out var tuesday) ? tuesday : TimeSpan.Zero,
                Wednesday = TimeSpan.TryParse(Wednesday, out var wednesday) ? wednesday : TimeSpan.Zero,
                Thursday = TimeSpan.TryParse(Thursday, out var thursday) ? thursday : TimeSpan.Zero,
                Friday = TimeSpan.TryParse(Friday, out var friday) ? friday : TimeSpan.Zero,
                Saturday = TimeSpan.TryParse(Saturday, out var saturday) ? saturday : TimeSpan.Zero,
                Sunday = TimeSpan.TryParse(Sunday, out var sunday) ? sunday : TimeSpan.Zero
            };

            _context.Trains.Add(train);
            await _context.SaveChangesAsync();

            TrainAdded = true;
            await OnGetAsync(); // Refresh the list after adding a new train
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            if (!Regex.IsMatch(TravelTime, @"^([0-9]|[0-1][0-9]|2[0-3]):[0-5][0-9]$"))
            {
                ModelState.AddModelError("TravelTime", "Invalid time format. Please use hh:mm or h:mm.");
                return Page();
            }

            var train = await _context.Trains.FindAsync(id);
            if (train != null)
            {
                train.DepartureLocation = DepartureLocation;
                train.ArrivalLocation = ArrivalLocation;
                train.Distance = Distance;
                train.Price = Price;
                train.TravelTime = TravelTime; // Ensure TravelTime is set
                train.Monday = TimeSpan.TryParse(Monday, out var monday) ? monday : TimeSpan.Zero;
                train.Tuesday = TimeSpan.TryParse(Tuesday, out var tuesday) ? tuesday : TimeSpan.Zero;
                train.Wednesday = TimeSpan.TryParse(Wednesday, out var wednesday) ? wednesday : TimeSpan.Zero;
                train.Thursday = TimeSpan.TryParse(Thursday, out var thursday) ? thursday : TimeSpan.Zero;
                train.Friday = TimeSpan.TryParse(Friday, out var friday) ? friday : TimeSpan.Zero;
                train.Saturday = TimeSpan.TryParse(Saturday, out var saturday) ? saturday : TimeSpan.Zero;
                train.Sunday = TimeSpan.TryParse(Sunday, out var sunday) ? sunday : TimeSpan.Zero;

                await _context.SaveChangesAsync();
            }

            await OnGetAsync(); // Refresh the list after editing a train
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var train = await _context.Trains.FindAsync(id);
            if (train != null)
            {
                _context.Trains.Remove(train);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}