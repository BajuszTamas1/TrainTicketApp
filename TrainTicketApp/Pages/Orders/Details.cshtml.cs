using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using TrainTicketApp.Models;
using TrainTicketApp.Data;

namespace TrainTicketApp.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Order Order { get; set; } = new Order();
        public Train Train { get; set; } = new Train();

        public void OnGet(int orderId)
        {
            Order = _context.Orders.FirstOrDefault(o => o.Id == orderId) ?? new Order();
            Train = _context.Trains.FirstOrDefault(t => t.Id == Order.TrainId) ?? new Train();

            if (Order.DepartureTime == TimeSpan.Zero)
            {
                Order.DepartureTime = TimeSpan.FromHours(0);
            }
        }

        public IActionResult OnPostCancel(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                order.Status = "Canceled";
                _context.SaveChanges();
            }
            return RedirectToPage(new { orderId });
        }
    }
}