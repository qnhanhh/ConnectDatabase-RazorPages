using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConnectDatabase.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Remove("Customer");
            HttpContext.Session.Remove("Admin");

            return RedirectToPage("./Index");
        }
    }
}
