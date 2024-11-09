using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupermarketWEB.Pages.Provider
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly SupermarketContext _context;

        public IndexModel(SupermarketContext context)
        {
            _context = context;
        }

        public IList<Providers> Provider { get; set; }

        public async Task OnGetAsync()
        {
            Provider = await _context.Provider.ToListAsync();
        }
    }
}

