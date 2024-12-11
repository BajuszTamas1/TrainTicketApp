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
        [BindProperty]
        public string TicketType { get; set; }
        [BindProperty]
        public int CarCount { get; set; }
        [BindProperty]
        public int SeatCount { get; set; }
        public int AllSeats { get; set; }
        public int AvailableSeats { get; set; }
        [BindProperty]
        public int ReservationPrice { get; set; }
        [BindProperty]
        public int SupplementaryPrice { get; set; }

        public bool TrainAdded { get; set; }
        public IList<Train> Trains { get; set; } = new List<Train>();
        public IList<User> Users { get; set; } = new List<User>();

        [BindProperty]
        public DateTime? StartDate { get; set; }
        [BindProperty]
        public DateTime? EndDate { get; set; }
        public List<DateTime> OrderDates { get; set; }
        public List<int> OrderCounts { get; set; }
        public List<string> TicketTypes { get; set; }
        public List<int> TicketTypeCounts { get; set; }

        public async Task OnGetAsync(DateTime? startDate, DateTime? endDate, string departureLocation, string arrivalLocation)
        {
            Trains = await _context.Trains.ToListAsync();
            Users = await _context.Users.ToListAsync();

            StartDate = startDate;
            EndDate = endDate;
            DepartureLocation = departureLocation;
            ArrivalLocation = arrivalLocation;

            var ordersQuery = _context.Orders.AsQueryable();

            if (StartDate.HasValue)
            {
                ordersQuery = ordersQuery.Where(o => o.OrderDate >= StartDate.Value);
            }

            if (EndDate.HasValue)
            {
                ordersQuery = ordersQuery.Where(o => o.OrderDate <= EndDate.Value);
            }

            if (!string.IsNullOrEmpty(DepartureLocation))
            {
                ordersQuery = ordersQuery.Where(o => o.DepartureLocation == DepartureLocation);
            }

            if (!string.IsNullOrEmpty(ArrivalLocation))
            {
                ordersQuery = ordersQuery.Where(o => o.ArrivalLocation == ArrivalLocation);
            }

            var orders = await ordersQuery.ToListAsync();

            OrderDates = orders
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => g.Key)
                .ToList();

            OrderCounts = orders
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => g.Count())
                .ToList();

            TicketTypes = orders
                .GroupBy(o => o.TicketType)
                .Select(g => g.Key)
                .ToList();

            TicketTypeCounts = orders
                .GroupBy(o => o.TicketType)
                .Select(g => g.Count())
                .ToList();
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
                TravelTime = TravelTime,
                Monday = TimeSpan.TryParse(Monday, out var monday) ? monday : TimeSpan.Zero,
                Tuesday = TimeSpan.TryParse(Tuesday, out var tuesday) ? tuesday : TimeSpan.Zero,
                Wednesday = TimeSpan.TryParse(Wednesday, out var wednesday) ? wednesday : TimeSpan.Zero,
                Thursday = TimeSpan.TryParse(Thursday, out var thursday) ? thursday : TimeSpan.Zero,
                Friday = TimeSpan.TryParse(Friday, out var friday) ? friday : TimeSpan.Zero,
                Saturday = TimeSpan.TryParse(Saturday, out var saturday) ? saturday : TimeSpan.Zero,
                Sunday = TimeSpan.TryParse(Sunday, out var sunday) ? sunday : TimeSpan.Zero,
                TicketType = TicketType,
                CarCount = CarCount,
                SeatCount = SeatCount,
                AllSeats = CarCount * SeatCount,
                AvailableSeats = CarCount * SeatCount,
                ReservationPrice = ReservationPrice,
                SupplementaryPrice = SupplementaryPrice,
            };
            decimal totalPrice = Price;
            if (TicketType == "Helyjegy")
            {
                totalPrice += ReservationPrice;
            }
            else if (TicketType == "Pótjegy")
            {
                totalPrice += SupplementaryPrice;
            }

            train.Price = totalPrice;

            _context.Trains.Add(train);
            await _context.SaveChangesAsync();

            TrainAdded = true;
            await OnGetAsync(StartDate,EndDate,DepartureLocation,ArrivalLocation);
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
            Console.WriteLine("Train!!!!!!!!!!!!!!!!ÁÁÁÁÁÁÁÁÁÁÁÁÁÁ: " + train);
            if (train != null)
            {
                train.DepartureLocation = DepartureLocation;
                train.ArrivalLocation = ArrivalLocation;
                train.Distance = Distance;
                train.TravelTime = TravelTime;
                train.Monday = TimeSpan.TryParse(Monday, out var monday) ? monday : TimeSpan.Zero;
                train.Tuesday = TimeSpan.TryParse(Tuesday, out var tuesday) ? tuesday : TimeSpan.Zero;
                train.Wednesday = TimeSpan.TryParse(Wednesday, out var wednesday) ? wednesday : TimeSpan.Zero;
                train.Thursday = TimeSpan.TryParse(Thursday, out var thursday) ? thursday : TimeSpan.Zero;
                train.Friday = TimeSpan.TryParse(Friday, out var friday) ? friday : TimeSpan.Zero;
                train.Saturday = TimeSpan.TryParse(Saturday, out var saturday) ? saturday : TimeSpan.Zero;
                train.Sunday = TimeSpan.TryParse(Sunday, out var sunday) ? sunday : TimeSpan.Zero;
                train.TicketType = TicketType;
                train.CarCount = CarCount;
                train.SeatCount = SeatCount;
                train.AllSeats = CarCount * SeatCount;
                train.AvailableSeats = AllSeats;
                train.AvailableSeatsMonday = AllSeats;
                train.AvailableSeatsTuesday = AllSeats;
                train.AvailableSeatsWednesday = AllSeats;
                train.AvailableSeatsThursday = AllSeats;
                train.AvailableSeatsFriday = AllSeats;
                train.AvailableSeatsSaturday = AllSeats;
                train.AvailableSeatsSunday = AllSeats;
                train.ReservationPrice = ReservationPrice;
                train.SupplementaryPrice = SupplementaryPrice;
                if (train.TicketType == "Helyjegy")
                {
                    train.Price = Price+ReservationPrice;
                }else if(train.TicketType == "Pótjegy")
                {
                    train.Price = Price+SupplementaryPrice;
                }else
                {
                    train.Price = Price;
                }


                await _context.SaveChangesAsync();
            }

            await OnGetAsync(StartDate,EndDate,DepartureLocation,ArrivalLocation);
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

        public async Task<IActionResult> OnPostDeleteUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangeUserRoleAsync(int id, string role)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.Role = role;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}