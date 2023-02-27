using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConnectDatabase.Models;
using System.Text.Json;

namespace ConnectDatabase.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly PRNDBContext _db;

        public ProfileModel(PRNDBContext _db)
        {
            this._db = _db;
        }

        [BindProperty]
        public Account Account { get; set; }

        [BindProperty]
        public Customer Customer { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Customer") == null)
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
                Customer = _db.Customers.FirstOrDefault(c => c.CustomerId == Account.CustomerId);
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                Account = JsonSerializer.Deserialize<Account>(HttpContext.Session.GetString("Customer"));
                var customer = _db.Customers.FirstOrDefault(c => c.CustomerId == Account.CustomerId);

                customer.CompanyName = String.IsNullOrEmpty(Customer.CompanyName) ? "N/A" : Account.Customer.CompanyName;
                customer.ContactName = Account.Customer.ContactName;
                customer.ContactTitle = Account.Customer.ContactTitle;
                customer.Address = Account.Customer.Address;

                Account.Email = Account.Email;

                _db.Accounts.Add(Account);
                _db.Customers.Add(Customer);
                _db.SaveChanges();

                HttpContext.Session.Remove("Customer");
                HttpContext.Session.SetString("Customer", JsonSerializer.Serialize(Account));

                return Page();
            }
            catch (Exception ex)
            {
                return Page();
            }
        }
    }
}
