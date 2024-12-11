using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainTicketApp.Data;
using TrainTicketApp.Models;
using System.Threading.Tasks;

namespace TrainTicketApp.Pages
{
	public class RegisterModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public RegisterModel(ApplicationDbContext context)
		{
			_context = context;
			Username = string.Empty;
			Password = string.Empty;
			ConfirmPassword = string.Empty;
			Name = string.Empty;
			Email = string.Empty;
			PhoneNumber = string.Empty;
			Address = string.Empty;
			ErrorMessage = string.Empty;
		}

		[BindProperty]
		public string Username { get; set; }
		[BindProperty]
		public string Password { get; set; }
		[BindProperty]
		public string ConfirmPassword { get; set; }
		[BindProperty]
		public string Name { get; set; }
		[BindProperty]
		public string Email { get; set; }
		[BindProperty]
		public string PhoneNumber { get; set; }
		[BindProperty]
		public string Address { get; set; }
		public string ErrorMessage { get; set; }

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword) ||
				string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Address))
			{
				ErrorMessage = "All fields are required.";
				return Page();
			}

			if (Password != ConfirmPassword)
			{
				ErrorMessage = "Passwords do not match.";
				return Page();
			}

			var existingUser = await _context.Users
				.FirstOrDefaultAsync(u => u.Username == Username);

			if (existingUser != null)
			{
				ErrorMessage = "Username already exists.";
				return Page();
			}

			string salt = BCrypt.Net.BCrypt.GenerateSalt();
			var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password, salt);

			var newUser = new User
			{
				Username = Username,
				Password = hashedPassword,
				Role = "User",
				Name = Name,
				Email = Email,
				PhoneNumber = PhoneNumber,
				Address = Address
			};

			_context.Users.Add(newUser);
			await _context.SaveChangesAsync();

			return RedirectToPage("Login");
		}
	}
}
