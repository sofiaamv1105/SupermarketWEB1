using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using System.Threading.Tasks;

namespace SupermarketWEB.Pages.PayMode
{
    public class DeleteModel : PageModel
    {
        private readonly SupermarketContext _context;

        public DeleteModel(SupermarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PayModeModels PayMode { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            PayMode = await _context.PayModes.FindAsync(id);

            if (PayMode == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            PayMode = await _context.PayModes.FindAsync(id);

            if (PayMode != null)
            {
                _context.PayModes.Remove(PayMode);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}

