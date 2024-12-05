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
        public Order NewOrder { get; set; }
        public IList<Train> AvailableTrains { get; set; } = new List<Train>();

        public async Task<IActionResult>  OnGetAsync()
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

            _context.Orders.Add(NewOrder);
            await _context.SaveChangesAsync();
            return RedirectToPage("./OrderConfirmation", new { id = NewOrder.Id });
        }
    }
}