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
			// A GET request does nothing, just loads the page
		}

		public async Task<IActionResult> OnPostAsync()
		{
			// Validate required fields
			if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword) ||
				string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Address))
			{
				ErrorMessage = "All fields are required.";
				return Page();
			}

			// Check if the two passwords match
			if (Password != ConfirmPassword)
			{
				ErrorMessage = "Passwords do not match.";
				return Page();
			}

			// Check if the username already exists
			var existingUser = await _context.Users
				.FirstOrDefaultAsync(u => u.Username == Username);

			if (existingUser != null)
			{
				ErrorMessage = "Username already exists.";
				return Page();
			}

			// Hash the password
			string salt = BCrypt.Net.BCrypt.GenerateSalt();
			var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password, salt);

			// Create a new user
			var newUser = new User
			{
				Username = Username,
				Password = hashedPassword,
				Role = "User",  // Default role
				Name = Name,
				Email = Email,
				PhoneNumber = PhoneNumber,
				Address = Address
			};

			// Add the user to the database
			_context.Users.Add(newUser);
			await _context.SaveChangesAsync();

			// Redirect to the login page after successful registration
			return RedirectToPage("Login");
		}
	}
}
