using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using System.Threading.Tasks;

namespace SupermarketWEB.Pages.Provider
{
    public class DeleteModel : PageModel
    {
        private readonly SupermarketContext _context;

        public DeleteModel(SupermarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Providers Provider { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Provider = await _context.Provider.FirstOrDefaultAsync(m => m.ProviderID == id);

            if (Provider == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Provider = await _context.Provider.FindAsync(id);

            if (Provider != null)
            {
                _context.Provider.Remove(Provider);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}

