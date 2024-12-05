// TrainTicketApp/Pages/Schedule/Index.cshtml.cs
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TrainTicketApp.Data;
using TrainTicketApp.Models;

namespace TrainTicketApp.Pages.Schedule
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Train> Trains { get; set; }

        public void OnGet()
        {
            Trains = _context.Trains.Include(t => t.DepartureTimes).ToList();
        }
    }
}