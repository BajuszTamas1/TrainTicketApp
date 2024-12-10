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
            User = Newtonsoft.Json.JsonConvert.DeserializeObject<TrainTicketApp.Models.User>(userData);

            if (User == null)
            {
                return RedirectToPage("/Login");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var userData = HttpContext.Session.GetString("User");
            User = Newtonsoft.Json.JsonConvert.DeserializeObject<TrainTicketApp.Models.User>(userData);
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