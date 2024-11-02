using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SupermarketWEB.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly SupermarketContext _context;

        public CreateModel(SupermarketContext context)
        {
            _context = context;
        }
        public class InputModel
        {
            [Required(ErrorMessage = "Name is mandatory.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Price is mandatory.")]
            [Range(0, int.MaxValue, ErrorMessage = "El precio debe ser un número positivo.")]
            public int Price { get; set; }

            [Required(ErrorMessage = "Stock is mandatory.")]
            [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser un número positivo.")]
            public int Stock { get; set; }

            [Required(ErrorMessage = "Category is mandatory")]
            [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría válida.")]
            public int CategoryId { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();
        public IEnumerable<SelectListItem> Categories { get; set; } = default!;
        private async Task LoadCategories()
        {
            Categories = await _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToListAsync();

            if (!Categories.Any())
            {
                ModelState.AddModelError("", "Enter at least one category");
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadCategories();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadCategories();
                return Page();
            }

            var product = new Product
            {
                Name = Input.Name,
                Price = Input.Price,
                Stock = Input.Stock,
                CategoryId = Input.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Product created.";
            return RedirectToPage("./Index");
        }
    }
}
