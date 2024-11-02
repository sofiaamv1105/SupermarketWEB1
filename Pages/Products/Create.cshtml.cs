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
            [Required(ErrorMessage = "El nombre es obligatorio.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "El precio es obligatorio.")]
            [Range(0, int.MaxValue, ErrorMessage = "El precio debe ser un n�mero positivo.")]
            public int Price { get; set; }

            [Required(ErrorMessage = "El stock es obligatorio.")]
            [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser un n�mero positivo.")]
            public int Stock { get; set; }

            [Required(ErrorMessage = "La categor�a es obligatoria")]
            [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categor�a v�lida.")]
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
                ModelState.AddModelError("", "No hay categor�as disponibles. Debe crear al menos una categor�a primero.");
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

            TempData["SuccessMessage"] = "Producto creado exitosamente.";
            return RedirectToPage("./Index");
        }
    }
}
