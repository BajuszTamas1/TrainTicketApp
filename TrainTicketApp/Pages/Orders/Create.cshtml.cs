using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainTicketApp.Models;
using TrainTicketApp.Data;
using TrainTicketApp.Services;

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

        public int TrainId { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string TicketType { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime OrderDate { get; set; } = DateTime.Now;  
        public string DayOfWeek { get; set; }
        public TimeSpan DepartureTime { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            AvailableTrains = await _context.Trains.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {


            var train = await _context.Trains.FindAsync(NewOrder.TrainId);


            // A nap és az indulási idő beállítása a kiválasztott nap alapján
            switch (SelectedDay)
            {
                case "Hétfő":
                    NewOrder.DayOfWeek = "Hétfő";
                    NewOrder.DepartureTime = train.Monday;
                    break;
                case "Kedd":
                    NewOrder.DayOfWeek = "Kedd";
                    NewOrder.DepartureTime = train.Tuesday;
                    break;
                case "Szerda":
                    NewOrder.DayOfWeek = "Szerda";
                    NewOrder.DepartureTime = train.Wednesday;
                    break;
                case "Csütörtök":
                    NewOrder.DayOfWeek = "Csütörtök";
                    NewOrder.DepartureTime = train.Thursday;
                    break;
                case "Péntek":
                    NewOrder.DayOfWeek = "Péntek";
                    NewOrder.DepartureTime = train.Friday;
                    break;
                case "Szombat":
                    NewOrder.DayOfWeek = "Szombat";
                    NewOrder.DepartureTime = train.Saturday;
                    break;
                case "Vasárnap":
                    NewOrder.DayOfWeek = "Vasárnap";
                    NewOrder.DepartureTime = train.Sunday;
                    break;
            }

            Console.WriteLine("Order: " + NewOrder);
            foreach(var prop in NewOrder.GetType().GetProperties())
            {
                Console.WriteLine(prop.Name + ": " + prop.GetValue(NewOrder));
            }

            if(TicketType == "Helyjegy" && train.AvailableSeats > 0){
                train.AvailableSeats--;
                _context.Orders.Add(NewOrder);
            }else{
                _context.Orders.Add(NewOrder);
            }
            await _context.SaveChangesAsync();


            return RedirectToPage("./OrderConfirmation", new { id = NewOrder.Id });
        }
    }
}