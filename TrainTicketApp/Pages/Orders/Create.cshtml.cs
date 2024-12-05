// TrainTicketApp/Pages/Orders/Create.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TrainTicketApp.Data;
using TrainTicketApp.Models;
using System.ComponentModel.DataAnnotations;

namespace TrainTicketApp.Pages.Orders
{
    public class CreateOrderModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateOrderModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty, Required]
        public int TrainId { get; set; }
        [BindProperty, Required]
        public string UserName { get; set; }
        [BindProperty, Required]
        public string UserAddress { get; set; }
        [BindProperty, Required]
        public string UserEmail { get; set; }
        [BindProperty, Required]
        public string UserPhone { get; set; }
        [BindProperty, Required]
        public string TicketType { get; set; }
        public int OrderId { get; set; }
        public IList<Train> Trains { get; set; } = new List<Train>();

        public void OnGet()
        {
            Trains = _context.Trains.ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var order = new Order
            {
                TrainId = TrainId,
                UserName = UserName,
                UserAddress = UserAddress,
                UserEmail = UserEmail,
                UserPhone = UserPhone,
                TicketType = TicketType,
                Status = "Active"
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            OrderId = order.Id;

            return RedirectToPage("./Index");
        }
    }
}