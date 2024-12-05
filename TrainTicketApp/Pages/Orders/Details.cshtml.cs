using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using TrainTicketApp.Models;

namespace TrainTicketApp.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Order Order { get; set; } = new Order(); // Initialize with a default value

        public void OnGet(int orderId)
        {
            Order = _context.Orders.FirstOrDefault(o => o.Id == orderId) ?? new Order(); // Set to a default value if not found
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