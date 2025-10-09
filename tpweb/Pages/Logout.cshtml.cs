using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace tpweb.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            // Limpiar la sesión
            HttpContext.Session.Clear();
        }
    }
}