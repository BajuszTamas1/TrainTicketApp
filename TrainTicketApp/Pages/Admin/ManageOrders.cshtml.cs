using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TrainTicketApp.Pages.Admin
{
    public class ManageOrders : PageModel
    {
        private readonly ILogger<ManageOrders> _logger;

        public ManageOrders(ILogger<ManageOrders> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}