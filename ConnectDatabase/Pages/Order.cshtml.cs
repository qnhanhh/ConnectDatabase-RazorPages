using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConnectDatabase.Models;
using System.Text.Json;

namespace ConnectDatabase.Pages
{
    public class OrderModel : PageModel
    {
        private readonly PRNDBContext _db;

        public OrderModel(PRNDBContext _db)
        {
            this._db = _db;
        }

        [BindProperty]
        public Account Account { get; set; }

        [BindProperty]
        public List<Order> Orders { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Customer") == null || HttpContext.Session.GetString("Admin") == null)
            {
                return Redirect("./Login");
            }

            Account = JsonSerializer.Deserialize<Account>(HttpContext.Session.GetString("Customer"));
            if (Account == null)
            {
                return Forbid();
            }
            else
            {
                Orders = _db.Orders.Where(o => o.CustomerId == Account.CustomerId).ToList();
            }
            return Page();
        }
    }
}
