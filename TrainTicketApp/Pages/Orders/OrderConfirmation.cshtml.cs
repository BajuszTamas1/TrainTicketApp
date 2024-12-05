using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TrainTicketApp.Models;
using TrainTicketApp.Data;

namespace TrainTicketApp.Pages.Orders
{
    public class OrderConfirmationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public OrderConfirmationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Order OrderDetails { get; set; }
        public Train TrainDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            OrderDetails = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (OrderDetails == null)
            {
                return RedirectToPage("/Index");
            }

            TrainDetails = await _context.Trains.FirstOrDefaultAsync(t => t.Id == OrderDetails.TrainId);
            if (TrainDetails == null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostCancelAsync(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order != null)
            {
                order.Status = "Canceled";
                await _context.SaveChangesAsync();
            }
            return RedirectToPage(new { id });
        }
    }
}