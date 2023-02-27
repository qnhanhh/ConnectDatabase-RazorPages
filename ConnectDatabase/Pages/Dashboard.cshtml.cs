using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConnectDatabase.Models;

namespace ConnectDatabase.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly PRNDBContext _db;

        public DashboardModel(PRNDBContext _db)
        {
            this._db = _db;
        }

        [BindProperty]
        public List<Category>? Categories { get; set; }
        [ViewData]
        public int? totalCat { get; set; }

        [BindProperty]
        public List<Product>? Products { get; set; }
        [ViewData]
        public int? totalProducts { get; set; }
        [ViewData]
        public string? catName { get; set; }

        [BindProperty]
        public List<Customer>? Customers { get; set; }
        [ViewData]
        public int? totalCustomers { get; set; }

        [BindProperty]
        public List<Order>? Orders { get; set; }
        [ViewData]
        public int? totalOrders { get; set; }

        [FromQuery(Name = "id")]
        public string? field { get; set; }

        public void OnGet()
        {
            switch (field)
            {
                case "customers":
                    Customers = _db.Customers.ToList();
                    totalCustomers = Customers.Count();
                    break;
                case "products":
                    Products = _db.Products.ToList();
                    totalProducts = Products.Count();
                    break;
                case "orders":
                    Orders = _db.Orders.ToList();
                    totalOrders = Orders.Count();
                    break;
                default:
                    Categories = _db.Categories.ToList();
                    totalCat = Categories.Count();
                    break;
            }
        }
    }
}
