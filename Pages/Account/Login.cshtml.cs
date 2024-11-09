using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Models;
using System.Security.Claims;

namespace SupermarketWEB.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            if (User.Email == "correo@gmail.com" && User.Password == "12345")
            {
                //Se crea los Claim, datos a almacenar en la Cookie
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email,User.Email),
                };
                //se asocian los claims creados a un nombre de un cookie
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                //se agrega la identidad creada al ClaimsPrincipal de la aplicacion
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                // se registra exitosamente la autenticación y se crea la cookie en el navegador 
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                return RedirectToPage("/Index");
            }
            ModelState.AddModelError(string.Empty, "Credenciales incorrectas. Por favor, intente de nuevo.");

            return Page();
        }
    }
}
