using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConnectDatabase.Models;
using System.Text.Json;

namespace ConnectDatabase.Pages
{
    public class DetailModel : PageModel
    {
        private readonly PRNDBContext _db;
        public DetailModel(PRNDBContext _db)
        {
            this._db=_db;
        }

        [BindProperty]
        public Models.Product Product { get; set; }

        [BindProperty]
        public Models.Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _db.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            Category = await _db.Categories.FirstOrDefaultAsync(m => m.CategoryId == Product.CategoryId);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
