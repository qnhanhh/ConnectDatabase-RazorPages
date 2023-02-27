using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConnectDatabase.Models;
using System.Text;

namespace ConnectDatabase.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly PRNDBContext _db;

        public SignUpModel(PRNDBContext _db)
        {
            this._db = _db;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        [BindProperty]
        public Account Account { get; set; }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var acc = _db.Accounts.SingleOrDefault(a => a.Email.Equals(Account.Email));

            if (acc != null)
            {
                ViewData["msg"] = "This email has already existed.";
                return Page();
            }

            var customer = new Customer()
            {
                CustomerId = generateCustomerID(5),
                CompanyName = Customer.CompanyName,
                ContactName = Customer.ContactName,
                ContactTitle = Customer.ContactTitle,
                Address = Customer.Address
            };

            var account = new Account()
            {
                CustomerId = customer.CustomerId,
                Email = Account.Email,
                Password = Account.Password,
                Role = 2
            };

            _db.Accounts.Add(account);
            _db.Customers.Add(customer);
            _db.SaveChanges();

            return RedirectToPage("./Index");
        }

        private string generateCustomerID(int length)
        {
            StringBuilder sB = new StringBuilder();
            Random random = new Random();
            char letter;
            for (int i = 0; i < length; i++)
            {
                double d = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * d));
                letter = Convert.ToChar(shift + 65);
                sB.Append(letter);
            }
            return sB.ToString();
        }
    }
}
