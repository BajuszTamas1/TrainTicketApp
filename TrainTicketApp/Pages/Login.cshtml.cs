using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TrainTicketApp.Models;
using TrainTicketApp.Data;

namespace TrainTicketApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == Username && u.Password == Password);
            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
				Console.WriteLine($"Logged in user: {user.Username} ÁÁÁÁÁÁÁÁÁÁÁÁÁÁÁÁ");
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
			Console.WriteLine("Invalid login attempt.ÁÁÁÁÁÁÁÁÁÁÁÁÁÁÁÁ");
            return Page();
        }
    }
}