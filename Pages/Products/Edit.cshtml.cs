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
    public class EditModel : PageModel
    {
        private readonly SupermarketContext _context;

        public EditModel(SupermarketContext context)
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

        // Lista de categor�as para el dropdown
        public IEnumerable<SelectListItem> Categories { get; set; } = default!;

        // M�todo para cargar las categor�as
        private async Task LoadCategories()
        {
            Categories = await _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToListAsync();
        }

        // M�todo para cargar los datos del producto y las categor�as al editar
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            Input = new InputModel
            {
                Name = product.Name,
                Price = (int)product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId
            };

            await LoadCategories();
            return Page();
        }

        // M�todo para procesar la edici�n del producto
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await LoadCategories();
                return Page();
            }

            var productToUpdate = await _context.Products.FindAsync(id);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del producto con los valores del formulario
            productToUpdate.Name = Input.Name;
            productToUpdate.Price = Input.Price;
            productToUpdate.Stock = Input.Stock;
            productToUpdate.CategoryId = Input.CategoryId;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Producto actualizado exitosamente.";
            return RedirectToPage("./Index");
        }
    }
}