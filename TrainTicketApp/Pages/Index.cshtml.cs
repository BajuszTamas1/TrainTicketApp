// TrainTicketApp/Pages/Index.cshtml.cs
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TrainTicketApp.Models;
using TrainTicketApp.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainTicketApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<Train> Trains { get; set; } = new List<Train>();

        public void OnGet(string departure, string arrival, TimeSpan? departureTime)
        {
            try
            {
                // Example JSON string (replace with actual JSON input)
                string jsonString = "{ \"example\": \"data\" }";

                // Log the JSON string
                _logger.LogInformation("Deserializing JSON: {JsonString}", jsonString);

                // Deserialize the JSON string
                var data = JsonSerializer.Deserialize<object>(jsonString);

                // Your existing code
                Trains = _context.Trains.Include(t => t.DepartureTimes).ToList();
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing JSON");
                throw;
            }
        }
    }
}