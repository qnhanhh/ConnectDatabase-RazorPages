using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConnectDatabase.Models;
using System.Text.Json;

namespace ConnectDatabase.Pages
{
    public class LoginModel : PageModel
    {
        private readonly PRNDBContext _db;

        public LoginModel(PRNDBContext _db)
        {
            this._db = _db;
        }
        [BindProperty]
        public Account account { get; set; }

        public IActionResult OnPost()
        {
            string email = Request.Form["account.Email"].ToString();
            string password = Request.Form["account.Password"].ToString();

            if (ModelState.IsValid)
            {
                account = _db.Accounts.FirstOrDefault(x => x.Email == email && x.Password == password);
                if (account != null)
                {
                    string jsonAccount = JsonSerializer.Serialize<Account>(account);
                    if (account.Role == 1)
                    {
                        HttpContext.Session.SetString("Admin", jsonAccount);
                        return new RedirectToPageResult("/Dashboard");
                    }
                    else
                    {
                        HttpContext.Session.SetString("Customer", jsonAccount);
                        return new RedirectToPageResult("/Index");
                    }
                }
                else
                {
                    ViewData["msg"] = "Account not found";
                    return Page();
                }
            }
            return Page();
        }
    }
}
