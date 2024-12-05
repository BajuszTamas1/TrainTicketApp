using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrainTicketApp.Pages.Orders
{
    public class OrderConfirmationModel : PageModel
    {
        public int OrderId { get; set; }

        public void OnGet(int orderId)
        {
            OrderId = orderId;
        }
    }
}