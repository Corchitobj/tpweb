using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace tpweb.Pages
{
    public class CerrarSesionModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Limpiar todas las variables de sesi�n
            HttpContext.Session.Clear();

            // Redirigir al login (puedes cambiarlo por Index si prefieres)
            return RedirectToPage("/Login");
        }
    }
}