using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using BCrypt.Net;

namespace TrainTicketApp.Pages
{
    public class LoginModel : PageModel
    {
        // ...existing code...

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Retrieve the user from the database
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == Username);

            if (user != null && BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
            {
                // Ensure session is accessed correctly
                HttpContext.Session.SetString("UserSession", Username);

                // Retrieve the username from the session
                var username = HttpContext.Session.GetString("UserSession");

                // Write the username to the console
                Console.WriteLine($"Logged in user: {username}");

                // Redirect to the home page or another page
                return RedirectToPage("/Index");
            }

            // If login fails, show an error message
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }

        public IActionResult OnPostLogout()
        {
            // Clear the session
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}