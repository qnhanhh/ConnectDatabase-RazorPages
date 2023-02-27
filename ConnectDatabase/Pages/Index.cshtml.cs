using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConnectDatabase.Models;

namespace ConnectDatabase.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PRNDBContext _db;
        public IndexModel(PRNDBContext _db)
        {
            this._db = _db;
        }

        [BindProperty]
        public List<Product> products { get; set; }

        [ViewData]
        public List<Category> categories { get; set; }

        [FromQuery(Name = "id")]
        public string CatId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? searchString { get; set; }

        public string sortOrder { get; set; }

        public IActionResult OnGet()
        {
            categories = _db.Categories.ToList();
            products = _db.Products.ToList();

            if (!string.IsNullOrEmpty(CatId))
            {
                products = products.Where(p => p.CategoryId == int.Parse(CatId)).ToList();
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.ProductName.ToLower().Contains(searchString.ToLower())).ToList();
            }

            switch (sortOrder)
            {
                case "desc":
                    products = products.OrderByDescending(p => p.UnitPrice).ToList();
                    break;
                default:
                    products = products.OrderBy(p => p.UnitPrice).ToList();
                    break;
            }
            return Page();
        }
    }
}
