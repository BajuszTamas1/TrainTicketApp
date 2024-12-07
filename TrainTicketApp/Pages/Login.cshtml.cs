using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TrainTicketApp.Models;
using TrainTicketApp.Data;
using System.Text.Json.Serialization;
using System.Text.Json;

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
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var user = await _context.Users
							.FirstOrDefaultAsync(u => u.Username == Username);

			if (user == null || !BCrypt.Net.BCrypt.Verify(Password, user.Password))
			{
				ErrorMessage = "Helytelen felhasználónév vagy jelszó!";
				return Page();
			}
			if (user != null)
			{
				var userData = JsonSerializer.Serialize(user);
				HttpContext.Session.SetString("User", userData);
				Console.WriteLine($"Logged in user: {user.Username} {user.Role}");
				return RedirectToPage("Index");
			}

			ModelState.AddModelError(string.Empty, "Invalid login attempt.");
			Console.WriteLine("Invalid login attempt.ÁÁÁÁÁÁÁÁÁÁÁÁÁÁÁÁ");
			return Page();
		}
	}
}