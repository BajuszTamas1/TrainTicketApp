using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrainTicketApp.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }
        public async Task OnPost()
        {
            HttpContext.Session.Clear();  // Munkamenet törlése
            Response.Redirect("/Index");
            await Task.CompletedTask;
        }
    }
}