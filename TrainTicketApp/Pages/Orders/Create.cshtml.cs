using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainTicketApp.Models;
using TrainTicketApp.Data;

namespace TrainTicketApp.Pages.Orders
{
    public class CreateOrderModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateOrderModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order NewOrder { get; set; } = new Order();
        public IList<Train> AvailableTrains { get; set; } = new List<Train>();

        [BindProperty]
        public string SelectedDay { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            AvailableTrains = await _context.Trains.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                AvailableTrains = await _context.Trains.ToListAsync();
                return Page();
            }

            var train = await _context.Trains.FindAsync(NewOrder.TrainId);
            if (train == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid train selection.");
                AvailableTrains = await _context.Trains.ToListAsync();
                return Page();
            }

            // A nap és az indulási idő beállítása a kiválasztott nap alapján
            switch (SelectedDay)
            {
                case "Hétfő":
                    NewOrder.DayOfWeek = "Monday";
                    NewOrder.DepartureTime = train.Monday;
                    Console.WriteLine("Monday: " + train.Monday);
                    break;
                case "Kedd":
                    NewOrder.DayOfWeek = "Tuesday";
                    NewOrder.DepartureTime = train.Tuesday;
                    Console.WriteLine("Tuesday: " + train.Tuesday);
                    break;
                case "Szerda":
                    NewOrder.DayOfWeek = "Wednesday";
                    NewOrder.DepartureTime = train.Wednesday;
                    break;
                case "Csütörtök":
                    NewOrder.DayOfWeek = "Thursday";
                    NewOrder.DepartureTime = train.Thursday;
                    break;
                case "Péntek":
                    NewOrder.DayOfWeek = "Friday";
                    NewOrder.DepartureTime = train.Friday;
                    break;
                case "Szombat":
                    NewOrder.DayOfWeek = "Saturday";
                    NewOrder.DepartureTime = train.Saturday;
                    break;
                case "Vasárnap":
                    NewOrder.DayOfWeek = "Sunday";
                    NewOrder.DepartureTime = train.Sunday;
                    break;
                default:
                    ModelState.AddModelError(string.Empty, "Invalid day selection.");
                    AvailableTrains = await _context.Trains.ToListAsync();
                    return Page();
            }

            _context.Orders.Add(NewOrder);
            await _context.SaveChangesAsync();

            return RedirectToPage("OrderConfirmation", new { id = NewOrder.Id });
        }
    }
}