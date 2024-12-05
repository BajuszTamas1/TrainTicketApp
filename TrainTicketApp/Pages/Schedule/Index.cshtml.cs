using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
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

        public IList<Train> Trains { get; set; } = new List<Train>();

        public void OnGet(string departure, string arrival, TimeSpan? departureTime)
        {
            var query = _context.Trains.AsQueryable();

            if (!string.IsNullOrEmpty(departure))
            {
                query = query.Where(t => t.DepartureLocation.Contains(departure));
            }

            if (!string.IsNullOrEmpty(arrival))
            {
                query = query.Where(t => t.ArrivalLocation.Contains(arrival));
            }

            if (departureTime.HasValue)
            {
                query = query.Where(t => t.Monday == departureTime.Value ||
                                         t.Tuesday == departureTime.Value ||
                                         t.Wednesday == departureTime.Value ||
                                         t.Thursday == departureTime.Value ||
                                         t.Friday == departureTime.Value ||
                                         t.Saturday == departureTime.Value ||
                                         t.Sunday == departureTime.Value);
            }

            Trains = query.ToList();
        }
    }
}