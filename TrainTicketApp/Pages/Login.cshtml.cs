using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainTicketApp.Data;
using TrainTicketApp.Models;
using System.Threading.Tasks;

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
		public string ErrorMessage { get; set; }

		public void OnGet()
		{
			// A GET request does nothing, just loads the page
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
			{
				ErrorMessage = "Username and Password are required.";
				return Page();
			}

			var user = await _context.Users
				.FirstOrDefaultAsync(u => u.Username == Username);

			if (user == null || !BCrypt.Net.BCrypt.Verify(Password, user.Password))
			{
				ErrorMessage = "Invalid username or password.";
				return Page();
			}

			// Set session variables
			HttpContext.Session.SetString("Username", user.Username);
			HttpContext.Session.SetString("Role", user.Role);

			return RedirectToPage("Index");
		}
	}
}
