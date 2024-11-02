using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using System.Data.SqlClient;
using SupermarketWEB.Pages;



namespace SupermarketWEB.Pages.PayMode
{
    public class IndexModel : PageModel
    {
        private readonly SupermarketContext _context;

        public IndexModel(SupermarketContext context)
        {
            _context = context;
        }
        public IList<PayModeModels> PayMode { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PayModes != null)
            {
                PayMode = await _context.PayModes.ToListAsync();
            }
        }
    }
}
