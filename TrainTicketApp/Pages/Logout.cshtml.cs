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
            HttpContext.Session.Clear();
            Response.Redirect("/Index");
            await Task.CompletedTask;
        }
    }
}