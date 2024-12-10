using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TrainTicketApp.Models;
using TrainTicketApp.Data;

namespace TrainTicketApp.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProfileModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userData = HttpContext.Session.GetString("User");
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(userData);
            User = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userData = HttpContext.Session.GetString("User");
            var userSession = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(userData);

            var userInDb = await _context.Users.FirstOrDefaultAsync(u => u.Username == userSession.Username);
            if (userInDb == null)
            {
                return RedirectToPage("/Login");
            }

            userInDb.Email = User.Email;
            userInDb.PhoneNumber = User.PhoneNumber;
            userInDb.Address = User.Address;

            if (!string.IsNullOrEmpty(User.Password))
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(User.Password, salt);
                userInDb.Password = hashedPassword;
            }
            _context.Users.Update(userInDb);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var userData = HttpContext.Session.GetString("User");
            if (string.IsNullOrEmpty(userData))
            {
                return RedirectToPage("/Login");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == User.Username);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                HttpContext.Session.Clear();
            }

            return RedirectToPage("/Index");
        }
    }
}